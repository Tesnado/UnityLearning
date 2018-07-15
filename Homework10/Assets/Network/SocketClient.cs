using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using FSFramework;
namespace AXX
{

    public enum DisType
    {
        Exception,
        Disconnect,
    }

    public class SocketClient
    {

        NetworkManager m_NetMgr;
        
        private TcpClient client = null;
        private uint net_id = 0;         ///netid 和 connecthandle均表示连接的唯一标识,用一个值表示;

        Queue<BinaryReader> mRecvDatas = new Queue<BinaryReader>();
        IAsyncResult reciveHandle = null;


        public uint netId
        {
            get { return net_id; }
        }

        public uint connectHandle
        {
            get { return net_id; }
        }

        private NetworkStream outStream = null;
        private MemoryStream memStream;
        private BinaryReader reader;

        private const int MAX_READ = 8192;
        private byte[] byteBuffer = new byte[MAX_READ];

        // Use this for initialization
        public SocketClient()
        {
            net_id = (uint)GetHashCode();
        }

        /// <summary>
        /// 注册代理
        /// </summary>
        public void OnRegister(NetworkManager netMgr)
        {
            m_NetMgr = netMgr;
            memStream = new MemoryStream();
            reader = new BinaryReader(memStream);
        }

        public void Update()
        {
            int count = mRecvDatas.Count;
            if(count > 0)
            {
                int recvCount = -1;
                for (int i = 0; i < m_NetMgr.keys.Count; i++)
                {
                    if (count < m_NetMgr.keys[i])
                    {
                        recvCount = m_NetMgr.m_recvCfgList[m_NetMgr.keys[i]];
                        break;
                    }
                }

                while ((recvCount == -1 || recvCount-- >= 1) && mRecvDatas.Count > 0)
                {
                    BinaryReader _event = null;
                    lock (mRecvDatas)
                    {
                        _event = mRecvDatas.Dequeue();
                    }
                    if (_event == null)
                    {
                        continue;
                    }
                    ushort cmd = _event.ReadUInt16();
                    _event.ReadUInt16();            ///读掉两个无用字节;
#if UNITY_EDITOR
                    if (cmd != 1464 && cmd != 7052 && cmd != 1104)     ///过滤掉两个心跳包;
                    {
                        //                     Debug.Log(Tools.formatString("RecvMessage cmdId={0}", cmd));
                    }
#endif
                    // UnityEngine.Profiling.Profiler.BeginSample("Reiv pack===" + cmd);
                    m_NetMgr.OnRecv(net_id, cmd, _event);
                    // UnityEngine.Profiling.Profiler.EndSample();
                    //                 break;
                }
            }
        }


        /// <summary>
        /// 连接服务器
        /// </summary>
        public uint ConnectServer(string host, int port, int timeout)
        {
            client = null;
			var addrs = Dns.GetHostAddresses(host);
			if(addrs != null && addrs.Length > 0)
			{
				client = new TcpClient (addrs [0].AddressFamily);
			}
			else{
				client = new TcpClient();
			}
            client.SendTimeout = timeout;
            client.ReceiveTimeout = timeout;
            client.NoDelay = true;
            try
            {
				
				if(addrs != null && addrs.Length > 0)
				{
					//Debug.LogErrorFormat("get addrs,host={0},family={1},add={2}",host,addrs[0].AddressFamily,addrs[0]);
					client.BeginConnect(addrs[0], port, new AsyncCallback(OnConnect), null);
				}
				else
				{
					client.BeginConnect(host, port, new AsyncCallback(OnConnect), null);
				}
                
            }
            catch (Exception e)
            {
                Close(); Debug.LogError(e.Message);
//                 return 0;
            }
            return connectHandle;
        }

        /// <summary>
        /// 连接上服务器
        /// </summary>
        void OnConnect(IAsyncResult asr)
        {
            try
            {
            outStream = client.GetStream();
            reciveHandle = client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
            m_NetMgr.OnConnected(((client != null) && client.Connected), connectHandle, net_id);
        }
            catch (Exception ex)
            {
                Debug.LogError("OnConnect--->>>" + ex.Message);
                Disconnected(DisType.Exception, ex.Message);
            }
        }

        public bool WriteMessage(short msgId, byte[] data, int dataLength)
        {
            MemoryStream ms = null;
            try
            {
            using (ms = new MemoryStream())
            {

                ms.Position = 0;
                BinaryWriter writer = new BinaryWriter(ms);
                int msglen = sizeof(short) + dataLength;
                //Debug.LogError(string.Format("{0} + {1} + {2}", sizeof(int), sizeof(short), dataLength));
                writer.Write(msglen);
                writer.Write(msgId);
                writer.Write(data, 0, dataLength);
                writer.Flush();

                if (client != null && client.Connected)
                {
#if UNITY_EDITOR
                        if (msgId != 1464 && msgId != 7052)     ///过滤掉两个心跳包;
                        {
//                             Debug.Log(Tools.formatString("SendMessage cmdId={0}", msgId));
                        }
#endif
                    byte[] payload = ms.ToArray();


                    outStream.BeginWrite(payload, 0, payload.Length, new AsyncCallback(OnWrite), null);
                    return true;
                }
                else
                {
                    Debug.LogError("client.connected----->>false");
                    return false;
                }
            }
        }
            catch (Exception ex)
            {
                Debug.LogError("WriteMessage--->>>" + ex.Message);
                Disconnected(DisType.Exception, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 读取消息
        /// </summary>
        void OnRead(IAsyncResult asr)
        {
            int bytesRead = 0;
            try
            {
                if ((client == null) || (client.GetStream() == null))
                {
                    return;
                }

                lock (client.GetStream())
                {      
                    //读取字节流到缓冲区;
                    bytesRead = client.GetStream().EndRead(asr);
                }

                if (bytesRead < 1)
                {
                    //包尺寸有问题，断线处理;
                    Disconnected(DisType.Disconnect, "bytesRead < 1");
                    return;
                }

                OnReceive(byteBuffer, bytesRead);   //分析数据包内容，抛给逻辑层;

                lock (client.GetStream())
                {       
                    //分析完，再次监听服务器发过来的新消息;
                    Array.Clear(byteBuffer, 0, byteBuffer.Length);   //清空数组;
                    client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
                }
            }
            catch (Exception ex)
            {
                Disconnected(DisType.Exception, ex.Message);
            }
        }

        /// <summary>
        /// 丢失链接
        /// </summary>
        public void Disconnected(DisType dis, string msg)
        {

            bool holdConnected = (client != null);
            Close();  
                           
            if(holdConnected)
            {
                lock (mRecvDatas){
                    mRecvDatas.Clear();
                }
                m_NetMgr.OnDisConnected(net_id);
            }

        }

        /// <summary>
        /// 打印字节
        /// </summary>
        /// <param name="bytes"></param>
        void PrintBytes()
        {
            string returnStr = string.Empty;
            for (int i = 0; i < byteBuffer.Length; i++)
            {
                returnStr += byteBuffer[i].ToString("X2");
            }
            Debug.LogError(returnStr);
        }

        /// <summary>
        /// 向链接写入数据流
        /// </summary>
        void OnWrite(IAsyncResult r)
        {
            try
            {
                outStream.EndWrite(r);
            }
            catch (Exception ex)
            {
                Debug.LogError("OnWrite--->>>" + ex.Message);
                Disconnected(DisType.Exception, ex.Message);
            }
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        void OnReceive(byte[] bytes, int length)
        {
            memStream.Seek(0, SeekOrigin.End);
            memStream.Write(bytes, 0, length);
            //Reset to beginning
            memStream.Seek(0, SeekOrigin.Begin);
            while (RemainingBytes() >= 4)
            {
                int messageLen = reader.ReadInt32();

                if (RemainingBytes() >= messageLen)
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(ms);
                    writer.Write(reader.ReadBytes(messageLen));
                    ms.Seek(0, SeekOrigin.Begin);
                    OnReceivedMessage(ms);
                }
                else
                {
                    //Back up the position two bytes
                    memStream.Position = memStream.Position - 4;
                    break;
                }
            }
            //Create a new stream with any leftover bytes
            byte[] leftover = reader.ReadBytes((int)RemainingBytes());
            memStream.SetLength(0);     //Clear
            memStream.Write(leftover, 0, leftover.Length);
        }

        /// <summary>
        /// 剩余的字节
        /// </summary>
        private long RemainingBytes()
        {
            return memStream.Length - memStream.Position;
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="ms"></param>
        void OnReceivedMessage(MemoryStream ms)
        {
            BinaryReader r = new BinaryReader(ms);

            lock(mRecvDatas)
            {
                mRecvDatas.Enqueue(r);
            }

        }

        bool connected
        {
            get
            {
                return ((client != null) && client.Connected);
            }
        }

        /// <summary>
        /// 关闭链接
        /// </summary>
        void Close()
        {
            if (client != null)
            {
//                 if(reciveHandle != null)
//                 {
//                     if (client.GetStream() != null && client.Connected)
//                     {
//                         client.GetStream().EndRead(reciveHandle);
//                     }
//                     reciveHandle = null;
//                 }
                if (client.Connected)
                {
                    client.Close();
                }

                client = null;
                
            }
            if(reader != null)
            {
                reader.Close();
            }
            if(memStream != null)
            {
                memStream.Close();
            }
        }

        public void Destroy()
        {
            Close();
            m_NetMgr = null;
        }

    }
}
