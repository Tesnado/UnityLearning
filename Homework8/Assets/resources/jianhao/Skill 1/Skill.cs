using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Skill : MonoBehaviour
{
    public static Skill Instance;

    public static float[] cd = new float[10];
    private static int sword_skill_num = 4;
    private static int magic_skill_num = 4;

    //是否使用过技能,get一次后置为false
    static public bool used_skill;

    /*{
		set {
			used_skill = value;
		}
		get {
			bool temp = used_skill;
			used_skill = false;
			return temp;
		}
	}*/

    public bool UsedSkill()
    {
        bool temp = used_skill;
        used_skill = false;
        return temp;
    }

    //粒子
    private GameObject blink_particle;

    private GameObject skyfire_particle;
    private GameObject slash_particle;
    private GameObject flame_particle;
    private GameObject bomb_particle;
    private GameObject fen_shen;
    private GameObject protect_orb;
    private GameObject shackle_particle;
    private GameObject lightning_particle;

    private GameObject orb_sound;
    private GameObject lightning_sound;
    private GameObject strike_sound;

    private float flame_timer = 0.1f;
    private XmlDocument xmldoc;
    private Animator animator;
    private bool init = false;
    private GameObject character;
    private Collider[] cols;

    void Awake()
    {
        StartCoroutine(GetXML());
    }

    void Start()
    {
    }

    void Update()
    {
        if (Character_Controller.character == null)
            return;
        else if (!init)
            Init();

        if (Property_Controller.my_Property == null)
            return;
        if (Property_Controller.my_Property.career == Career.Swordman)
        {
            for (int i = 0; i < sword_skill_num; i++)
            {
                if (cd[i] >= 0)
                    cd[i] -= Time.deltaTime;
                else
                    cd[i] = 0;
            }
        }
        else if (Property_Controller.my_Property.career == Career.Magician)
        {
            for (int i = 0; i < magic_skill_num; i++)
            {
                if (cd[i] >= 0)
                    cd[i] -= Time.deltaTime;
                else
                    cd[i] = 0;
            }
        }

        //开启烈焰模式

        if (flame_timer >= 0)
            flame_timer -= Time.deltaTime;
        else
            flame_timer = 0;

        if (flame_mode)
            if (flame_timer <= 0)
            {
                Instantiate(flame_particle, character.transform.position + character.transform.up, character.transform.rotation);
                flame_timer = 0.1f;
            }
    }

    void Init()
    {
        character = Character_Controller.character;
        animator = character.GetComponent<Animator>();
        blink_particle = Resources.Load("jianhao/BlinkParticle") as GameObject;
        skyfire_particle = Resources.Load("jianhao/NewSkyFireParticle") as GameObject;
        slash_particle = Resources.Load("jianhao/SkyFireParticle") as GameObject;
        flame_particle = Resources.Load("jianhao/FlameParticle") as GameObject;
        bomb_particle = Resources.Load("jianhao/BombParticle") as GameObject;
        fen_shen = Resources.Load("jianhao/FenShen") as GameObject;
        protect_orb = Resources.Load("jianhao/ProtectOrb") as GameObject;
        shackle_particle = Resources.Load("jianhao/ShackleParticle") as GameObject;
        lightning_particle = Resources.Load("jianhao/LightningParticle") as GameObject;

        orb_sound = Resources.Load("jianhao/OrbSound") as GameObject;
        lightning_sound = Resources.Load("jianhao/LightningSound") as GameObject;
        strike_sound = Resources.Load("jianhao/StrikeSound") as GameObject;
        InvokeRepeating("FlameMode", 0, 0.5f);
        SkillInit();
        used_skill = false;
        init = true;
    }

    IEnumerator GetXML()
    {
        string localPath;
        if (Application.platform == RuntimePlatform.Android)
        {
            localPath = Application.streamingAssetsPath + "/SkillInfo.xml";
            //Debug.Log(localPath);
        }
        else
        {
            localPath = "file://" + Application.streamingAssetsPath + "/SkillInfo.xml";
            //Debug.Log(localPath);
        }
        WWW www = new WWW(localPath);
        while (!www.isDone)
        {
            Debug.Log("Getting GetXML");
            yield return www;
            ReadSkillInfo(www);
        }
    }

    void ReadSkillInfo(WWW www)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(www.text);
        XmlNode root = xmlDoc.SelectSingleNode("Skill");

        if (root != null)
        {
            XmlNodeList skill_node_list = root.ChildNodes;
            foreach (XmlNode node in skill_node_list)
            {
                Skill_Attribution temp = new Skill_Attribution();
                XmlNodeList cont_node_list = node.ChildNodes;
                foreach (XmlNode _node in cont_node_list)
                {
                    XmlElement xmlelement = (XmlElement)_node;
                    switch (xmlelement.GetAttribute("name"))
                    {
                        case "Rname":
                            temp.name = xmlelement.InnerText;
                            break;

                        case "cd":
                            temp.cd = float.Parse(xmlelement.InnerText);
                            break;

                        case "mp":
                            temp.mp_use = int.Parse(xmlelement.InnerText);
                            break;

                        case "damage":
                            temp.damage_des = xmlelement.InnerText;
                            break;

                        case "description":
                            temp.des = xmlelement.InnerText;
                            break;
                    }
                }
                temp.skill_level = 0;
                temp.damage = 0;
                Skill_Register.AddSkill(temp.name, temp);
            }
        }
    }

    void SkillInit()
    {
        Skill_Register.GetSkill("SwordSkill1").effect = Blink;
        Skill_Register.GetSkill("SwordSkill2").effect = Strike;
        Skill_Register.GetSkill("SwordSkill3").effect = FlameOpen;
        Skill_Register.GetSkill("SwordSkill4").effect = Ultimate;

        Skill_Register.GetSkill("MagicSkill1").effect = Bomb;
        Skill_Register.GetSkill("MagicSkill2").effect = Lightning;
        Skill_Register.GetSkill("MagicSkill3").effect = Orb;
        Skill_Register.GetSkill("MagicSkill4").effect = Shackles;

        for (int i = 0; i < sword_skill_num; i++)
        {
            cd[i] = 0;
            cd[i] = 0;
        }
    }

    bool TargetExist()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "monster")
            {
                return true;
            }
        }
        return false;
    }

    //Blink                  SwordSkill1
    //Shadow Strike          SwordSkill2
    //Flame Mode             SwordSkill3
    //Ultimate Sky Fire      SwordSkill4
    //Bomb                   MagicSkill1
    //Lightning              MagicSkill2
    //Protactive Orb         MagicSkill3
    //Magic Shackles         MagicSkill4

    public void Blink()
    {
        int _mp = Skill_Register.GetSkill("SwordSkill1").mp_use;
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            cd[0] <= 0)
        {
            used_skill = true;
            cd[0] = Skill_Register.GetSkill("SwordSkill1").cd;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            //mp-=20;
            character.GetComponent<CharacterController>().Move(character.transform.forward * 10f);
            animator.SetTrigger("blink_trigger");
            Instantiate(blink_particle, character.transform.position, character.transform.rotation);
            cols = Physics.OverlapSphere(character.transform.position, 5f);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] != null && cols[i].tag == "monster")
                {
                    Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                    monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("SwordSkill1"));
                }
            }
        }
    }

    public void Strike()
    {
        int _mp = Skill_Register.GetSkill("SwordSkill2").mp_use;
        cols = Physics.OverlapSphere(character.transform.position + character.transform.forward * 3, 3f);
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            TargetExist() &&
            cd[1] <= 0)
        {
            used_skill = true;
            cd[1] = Skill_Register.GetSkill("SwordSkill2").cd;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            int i;
            character.transform.Find("Swordman_Body001").SendMessage("hide");
            character.transform.Find("Swordman_Sword").SendMessage("hide");
            animator.SetTrigger("strike_trigger");
            //GameObject child = character.transform.FindChild("Swordman_Body001")as GameObject;
            //GetComponentsInChildren<renderer>
            //if(child.renderer)
            //	child.renderer.material.color = new Color(renderer.material.color.r,renderer.material.color.g,renderer.material.color.b,0);
            for (i = 0; i < cols.Length; i++)
            {
                Debug.Log(cols[i].name);
                if (cols[i] != null && cols[i].tag == "monster")
                {
                    Instantiate(fen_shen, cols[i].transform.position - cols[i].transform.forward, cols[i].transform.rotation);
                    Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                    monster.stop = true;
                    //Instantiate (slash_particle, cols[i].transform.position, cols[i].transform.rotation);
                    //Instantiate (slash_particle, cols[i].transform.position, cols[i].transform.rotation);
                }
            }
            Invoke("StrikeDone", 1.2f);
            Collider[] temp_cols = cols;
            StartCoroutine(StrikeDone2(temp_cols));
        }
    }

    void StrikeDone()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "monster")
            {
                Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("SwordSkill2"));
                Instantiate(slash_particle, cols[i].transform.position, cols[i].transform.rotation);
                Instantiate(strike_sound, character.transform.position, character.transform.rotation);
            }
        }
        character.transform.Find("Swordman_Body001").SendMessage("show");
        character.transform.Find("Swordman_Sword").SendMessage("show");
    }

    IEnumerator StrikeDone2(Collider[] _cols)
    {
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (_cols[i] != null && cols[i].tag == "monster")
            {
                Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                monster.stop = false;
            }
        }
    }

    public void Ultimate()
    {
        int _mp = Skill_Register.GetSkill("SwordSkill4").mp_use;
        cols = Physics.OverlapSphere(character.transform.position + character.transform.forward * 3, 3f);
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            TargetExist() &&
            cd[2] <= 0)
        {
            used_skill = true;
            cd[2] = Skill_Register.GetSkill("SwordSkill4").cd;
            Property_Controller.Instance.Change_CurrentMP(-_mp);

            int i;
            animator.SetTrigger("ultimate_trigger");

            Invoke("CallSkyfire", 0.1f);
            Invoke("UltimateDamage", 0.1f);
            Invoke("CallSkyfire", 0.2f);
            for (i = 0; i < 5; i++)
                Invoke("CallSkyfire", 0.5f);
            Invoke("UltimateDamage", 0.5f);
            Invoke("CallSkyfire", 0.6f);
            Invoke("CallSkyfire", 0.75f);
            Invoke("CallSkyfire", 0.8f);
            for (i = 0; i < 5; i++)
                Invoke("CallSkyfire", 1f);
            Invoke("UltimateDamage", 1f);
            for (i = 0; i < 10; i++)
                Invoke("CallSkyfire", 1.6f);
            Invoke("UltimateDamage", 1.6f);
        }
    }

    void CallSkyfire()
    {
        Vector3 pos = character.transform.position + character.transform.forward * Random.Range(1, 6);
        Vector3 p = pos + character.transform.right * Random.Range(-2, 2);
        Instantiate(skyfire_particle, p, character.transform.rotation);
    }

    private bool flame_mode = false;
    private int ad_add = 0;

    public void FlameOpen()
    {
        int _mp = Skill_Register.GetSkill("SwordSkill3").mp_use;
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp && cd[3] <= 0)
        {
            used_skill = true;
            cd[3] = Skill_Register.GetSkill("SwordSkill3").cd;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            flame_mode = true;
            Skill_Register.GetSkill("SwordSkill3").effect = FlameClose;
        }
    }

    public void FlameClose()
    {
        if (cd[3] <= 0)
        {
            cd[3] = Skill_Register.GetSkill("SwordSkill3").cd;
            flame_mode = false;
            Skill_Register.GetSkill("SwordSkill3").effect = FlameOpen;
            Property_Controller.Instance.Change_AD(-ad_add);
            ad_add = 0;
        }
    }

    void FlameMode()
    {
        if (Property_Controller.Instance.Get_CurrentMP() < 1)
            flame_mode = false;
        if (flame_mode && Property_Controller.Instance.Get_CurrentMP() >= 1)
        {
            Property_Controller.Instance.Change_CurrentMP(-1);
            cols = Physics.OverlapSphere(character.transform.position, 5f);
            Property_Controller.Instance.Change_AD(-ad_add);
            ad_add = (int)(Property_Controller.Instance.Get_AD() * 0.3);
            Property_Controller.Instance.Change_AD(ad_add);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] != null && cols[i].tag == "monster")
                {
                    Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                    monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("SwordSkill4"));
                }
            }
        }
    }

    void UltimateDamage()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i] != null && cols[i].tag == "monster")
            {
                Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("SwordSkill3"));
            }
        }
    }

    public void Bomb()
    {
        int _mp = Skill_Register.GetSkill("MagicSkill1").mp_use;
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            cd[0] <= 0)
        {
            used_skill = true;
            cd[0] = Skill_Register.GetSkill("MagicSkill1").cd;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            //mp-=20;
            animator.SetTrigger("bomb_trigger");
            cols = Physics.OverlapSphere(character.transform.position, 5f);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] != null && cols[i].tag == "monster")
                {
                    Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                    monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("MagicSkill1"));
                }
            }
            Instantiate(bomb_particle, character.transform.position, character.transform.rotation);
            character.transform.position -= character.transform.forward * 8f;
        }
    }

    public void Lightning()
    {
        int _mp = Skill_Register.GetSkill("MagicSkill2").mp_use;
        cols = Physics.OverlapSphere(character.transform.position, 5f);
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            TargetExist() &&
            cd[1] <= 0)
        {
            used_skill = true;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            cd[1] = Skill_Register.GetSkill("MagicSkill2").cd;
            animator.SetTrigger("lightning_trigger");
            Invoke("CallLightning", 0.5f);
            Instantiate(lightning_sound, character.transform.position, character.transform.rotation);
        }
    }

    void CallLightning()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i] != null && cols[i].tag == "monster")
            {
                if (UnityEngine.Random.Range(0, 100) > 30)
                {
                    Instantiate(lightning_particle, cols[i].transform.position, cols[i].transform.rotation);
                    Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                    monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("MagicSkill2"));
                }
            }
        }
    }

    public void Orb()
    {
        int _mp = Skill_Register.GetSkill("MagicSkill3").mp_use;
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            cd[2] <= 0)
        {
            used_skill = true;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            cd[2] = Skill_Register.GetSkill("MagicSkill3").cd;
            animator.SetTrigger("orb_trigger");
            Invoke("CreateOrb", 0.36f);
        }
    }

    void CreateOrb()
    {
        Instantiate(orb_sound, character.transform.position, character.transform.rotation);
        Instantiate(protect_orb, character.transform.position + character.transform.forward, character.transform.rotation);
    }

    public void Missile()
    {
        int _mp = Skill_Register.GetSkill("MagicSkill4").mp_use;
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            cd[3] <= 0)
        {
            used_skill = true;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            cd[3] = Skill_Register.GetSkill("MagicSkill4").cd;
            Ray ray = new Ray(character.transform.position - character.transform.forward + Vector3.up, character.transform.forward);
            RaycastHit hit;

            for (int i = 0; i < 6; i++)
            {
                ray = new Ray(character.transform.position - character.transform.forward + Vector3.up, character.transform.forward + i * 0.1f * character.transform.right);
                if (Physics.Raycast(ray, out hit, 20f))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 10);
                    if (hit.collider.gameObject.tag == "monster")
                    {
                        hit.collider.gameObject.GetComponent<Monster_Manage>().Monster_Manage_Change_Hp(-Property_Controller.Instance.Get_AD());
                        break;
                    }
                }
                ray = new Ray(character.transform.position - character.transform.forward + Vector3.up, character.transform.forward - i * 0.1f * character.transform.right);
                if (Physics.Raycast(ray, out hit, 20f))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 10);
                    if (hit.collider.gameObject.tag == "monster")
                    {
                        hit.collider.gameObject.GetComponent<Monster_Manage>().Monster_Manage_Change_Hp(-Property_Controller.Instance.Get_AD());
                        break;
                    }
                }
            }
        }
    }

    void ShootMissile()
    {
        Debug.Log("1");
    }

    void Shackles()
    {
        cols = Physics.OverlapSphere(character.transform.position, 8f);
        int _mp = Skill_Register.GetSkill("MagicSkill4").mp_use;
        if (Property_Controller.Instance.Get_CurrentMP() >= _mp &&
            TargetExist() &&
            cd[3] <= 0)
        {
            used_skill = true;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            cd[3] = Skill_Register.GetSkill("MagicSkill4").cd;
            Property_Controller.Instance.Change_CurrentMP(-_mp);
            DestroyForTime_CSP destroy = shackle_particle.GetComponent<DestroyForTime_CSP>();
            animator.SetTrigger("shackle_trigger");
            float timer = Skill_Register.GetSkill("MagicSkill4").skill_level * 0.5f + 2f;
            destroy.timer = timer;
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] != null && cols[i].tag == "monster")
                {
                    Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                    monster.Monster_Manage_Change_Hp(-Skill_Damage.Instance.GetDamage("MagicSkill4"));
                    Instantiate(shackle_particle, cols[i].transform.position, cols[i].transform.rotation);
                    monster.stop = true;
                }
            }
            Collider[] temp_cols = cols;

            StartCoroutine(UnShackles(temp_cols, timer));
        }
    }

    IEnumerator UnShackles(Collider[] _cols, float timer)
    {
        yield return new WaitForSeconds(timer);
        //Debug.Log("!!!");
        for (int i = 0; i < cols.Length; i++)
        {
            if (_cols[i] != null && cols[i].tag == "monster")
            {
                Monster_Manage monster = cols[i].GetComponent<Monster_Manage>();
                monster.stop = false;
            }
        }
    }
}