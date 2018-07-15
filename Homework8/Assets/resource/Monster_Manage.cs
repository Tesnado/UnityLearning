using UnityEngine;
using System.Collections;

public class Monster_Manage : MonoBehaviour {
	public float dist;                 // 怪物与英雄之间的距离
	public int x, z;
	public Vector3 tmppos;
	public Vector3 initpos;
    int time  = 10;
    float delta_Time;
	public GameObject Hero;
	public GameObject WolfMonster;
	public Animator animator;
	public int monster_id;
	bool init = false;
	public CharacterController controller;
	public Monster_Property my_Property;
    public bool stop = false;
	public enum WolfState
	{
		IDLE,
		WALK,
		ATTACK,
		TAKEDAMAGE,
		DEATH
	}
	public WolfState state;          //怪物的五种状态
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		my_Property = transform.GetComponent<Monster_Property>();
		Hero = Character_Controller.character;
		//WolfMonster = GameObject.FindWithTag ("WolfMonster");
		WolfMonster = this.gameObject;
		animator = GetComponent<Animator>();
		if (monster_id == 3) {
			my_Property.Damage = 8; 
			my_Property.DropRate = 0.1;
			my_Property.Monster_Max_Hp = 200;
			my_Property.Monster_Current_Hp = 200;
			my_Property.MonsterName = "WolfBoss";
		}
		else if (monster_id == 2) {
			my_Property.Damage = 4; 
			my_Property.DropRate = 0.2;
			my_Property.Monster_Max_Hp = 100;
			my_Property.Monster_Current_Hp = 100;
			my_Property.MonsterName = "WolfNormal";
		}
		else if (monster_id == 1) {
			my_Property.Damage = 2; 
			my_Property.DropRate = 0.2;
			my_Property.Monster_Max_Hp = 50;
			my_Property.Monster_Current_Hp = 50;
			my_Property.MonsterName = "WolfBaby";
		}
		initpos = transform.position;
		x = Random.Range (-7, 7);
		z = Random.Range (-7, 7);
	}
	
	public void MonsterWalkAround(Vector3 pos) {
		WolfMonster.transform.rotation = Quaternion.Slerp (WolfMonster.transform.rotation,
		                                                   Quaternion.LookRotation(pos  - 
		                        new Vector3(transform.position.x, 0, transform.position.z)), 2*Time.deltaTime);
		state = WolfState.WALK;
		MonsterStateControl ();
		controller.Move (new Vector3 (transform.forward.x, 0, transform.forward.z) * 2*Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (Character_Controller.character == null)
		{
			return;
		}
		else if (!init)
		{
			Init();
		}
		if (state == WolfState.DEATH)
		{
			MonsterStateControl();
			return;
		}
		if (Property_Controller.dead)
		{
			state = WolfState.IDLE;
		}
        if (stop)
        {
            state = WolfState.IDLE;
            MonsterStateControl();
            return;
        }
        else
        {

            dist = Vector3.Distance(Hero.transform.position, WolfMonster.transform.position);
            //mondist = Vector3.Distance(initpos, WolfMonster.transform.position);
            /**
             * 下面是这样的，当两者距离小于3时，则怪物攻击，设置怪物状态为ATTACK
             * 当3<dis<15时，则怪物走向player
             * 大于15，IDLE状态，不处理
             **/
            if (dist < 10)
            {
                //print("进入Boss攻击范围");
                WolfMonster.transform.rotation = Quaternion.Slerp(WolfMonster.transform.rotation,
                                                                  Quaternion.LookRotation(new Vector3(Hero.transform.position.x, 0, Hero.transform.position.z)
                                        - new Vector3(transform.position.x, 0, transform.position.z)), 2 * Time.deltaTime);

                if (dist > 3)
                {
                    state = WolfState.WALK;
                    controller.Move(new Vector3(transform.forward.x, 0, transform.forward.z) * 2 * Time.deltaTime);
                }
                if (dist <= 3)
                {
                    state = WolfState.ATTACK;
                }
            }
            else
            {
                delta_Time -= Time.deltaTime;
                tmppos = new Vector3(initpos.x + x, 0, initpos.z + z);
                if (Vector3.Distance(transform.position, tmppos) > 1)
                {
                    MonsterWalkAround(tmppos);

                }
                else
                {
                    delta_Time = time;
                    x = Random.Range(-7, 7);
                    z = Random.Range(-7, 7);
                    //Debug.Log ("yes");
                }
                if (delta_Time < 0)
                {
                    delta_Time = time;
                    transform.position = initpos;
                }
                //print("脱离Boss攻击范围");
                //state = WolfState.IDLE;
                //MonsterStateControl();
            }
        }
		MonsterStateControl();
		
	}
	
	void Init()
	{
		Hero = Character_Controller.character;
		init = true;
	}
	public void AttackPlayer() {
		//Debug.Log(Time.time);
		Property_Controller.Instance.Change_CurrentHP(-my_Property.Damage);
	}
	
	/// 怪物的各种状态处理，设置了animator的状态机的变量
	public void MonsterStateControl()
	{
		switch (state)
		{
		case WolfState.IDLE:
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
				animator.SetTrigger("isIdle");
			}
			break;
		case WolfState.WALK:
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
			{
				animator.SetTrigger("isWalk");
				
			}
			break;
		case WolfState.ATTACK:
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
			{
				animator.SetTrigger("isAttack");
			}  
			break;
		case WolfState.TAKEDAMAGE:
			
			break;
		case WolfState.DEATH:
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
			{
				animator.SetTrigger("isDeath");
			}
			break;
		}
	}
	
	public void Death()
	{
		TaskManager.Instance.MonsterDie(monster_id);
		Property_Controller.Instance.GetEXP(monster_id * 1000);
		GameObject.Destroy(this.gameObject);
	}
	
	public void Monster_Manage_Change_Hp(int value) {
		my_Property.Monster_Current_Hp += value;
		if (my_Property.Monster_Current_Hp <= 0) {
			my_Property.Monster_Current_Hp = 0;
			state = WolfState.DEATH;
			
		}
        if (value < 0)
        {
            Monster_UI_Controller.setMonsterCurrentProperty(this.gameObject);
        }
		if (my_Property.Monster_Current_Hp > my_Property.Monster_Max_Hp) {
			my_Property.Monster_Current_Hp = my_Property.Monster_Max_Hp;
		}
	}
	
	public void Monster_Manage_Set_CurrentHp(int value) {
		my_Property.Monster_Current_Hp = value;
		if (value <= 0) {
			my_Property.Monster_Current_Hp = 0;
		}
		if (value >= my_Property.Monster_Max_Hp) {
			my_Property.Monster_Current_Hp = my_Property.Monster_Max_Hp;
		}
	}
	
	public int Monster_Manage_Get_CurrentHp() {
		return my_Property.Monster_Current_Hp;
	}
	
	public int Monster_Manage_Get_CurrentDamage() {
		return my_Property.Damage; 
	}
	
	public string Monster_Manage_GetMonsterName() {
		return my_Property.MonsterName;
	}

	public int Monster_Manage_Get_MaxHP() {
		return my_Property.Monster_Max_Hp;
	}
	
}
