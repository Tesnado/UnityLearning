Shader "Custom/Grain"
{
    CGINCLUDE
        
        #pragma exclude_renderers d3d11_9x
        #pragma target 3.0
        #include "UnityCG.cginc"
        #include "Public.cginc"

		//���ⲿ��ȡ��������û���ﵽ����任��Ч��
		float _Random;

		//�������ֵ���ɷ���
        float Noise(float2 n, float x)
        {
            n += x;
            return frac(sin(dot(n.xy, float2(12.9898, 78.233))) * 43758.5453);
        }

		//��ÿһ�����ؽ��о��������ȡ����Χ�����������ֵ������㣬����һ��Ȩ�����
        float Step1(float2 uv, float n)
        {
			float a = 1.0, b = 2.0, c = -12.0, t = 1.0;   
			return (1.0 / (a * 4.0 + b * 4.0 + abs(c))) * (
			Noise(uv + float2(-1.0,-1.0) * t, n) * a +
			Noise(uv + float2( 0.0,-1.0) * t, n) * b +
			Noise(uv + float2( 1.0,-1.0) * t, n) * a +
			Noise(uv + float2(-1.0, 0.0) * t, n) * b +
			Noise(uv + float2( 0.0, 0.0) * t, n) * c +
			Noise(uv + float2( 1.0, 0.0) * t, n) * b +
			Noise(uv + float2(-1.0, 1.0) * t, n) * a +
			Noise(uv + float2( 0.0, 1.0) * t, n) * b +
			Noise(uv + float2( 1.0, 1.0) * t, n) * a +
			0.0);
        }

		//�ٶ�ÿһ�����ؽ��о��������ȡ��������Step1�Ľ�����������������ֲ����Ӿ���
        float Step2(float2 uv, float n)
        {
            float a = 1.0, b = 2.0, c = 4.0, t = 1.0;   
			return (1.0 / (a * 4.0 + b * 4.0 + abs(c))) * (
			Step1(uv + float2(-1.0,-1.0) * t, n) * a +
			Step1(uv + float2( 0.0,-1.0) * t, n) * b +
			Step1(uv + float2( 1.0,-1.0) * t, n) * a +
			Step1(uv + float2(-1.0, 0.0) * t, n) * b +
			Step1(uv + float2( 0.0, 0.0) * t, n) * c +
			Step1(uv + float2( 1.0, 0.0) * t, n) * b +
			Step1(uv + float2(-1.0, 1.0) * t, n) * a +
			Step1(uv + float2( 0.0, 1.0) * t, n) * b +
			Step1(uv + float2( 1.0, 1.0) * t, n) * a +
			0.0);
        }

		//��������ɫͨ����ֵ��ֵ����Step2������Ὣ�ⲿ��������������Step2�У��û���ﵽ����任��Ч��
        float3 Step3(float2 uv)
        {
            float a = Step2(uv, 0.07 * frac(_Random));
            float b = Step2(uv, 0.11 * frac(_Random));
            float c = Step2(uv, 0.13 * frac(_Random));
            return float3(a, b, c);
        }

        float3 frag(v2f i) : SV_Target
        {
            return Step3(i.uv);
        }

    ENDCG

    SubShader
    {
		//��������Ļ��Ⱦ�������޳�����ȶ����Թ�
        Cull Back ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag

            ENDCG
        }

    }
}
