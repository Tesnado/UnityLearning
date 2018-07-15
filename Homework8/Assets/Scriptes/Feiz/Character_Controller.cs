using UnityEngine;
using System.Collections;

public class Character_Controller : MonoBehaviour {
    static Character_Controller instance;
    AudioSource town, fighting;
    GameObject BGM;
    //获取单实例
    public static Character_Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Character_Controller();
            }
            return instance;
        }
    }
    int layerMask;
    public static GameObject character;
    static CharacterController myCController;
    static GameObject mainCamera;
    static Animator animator;
    public static bool fight = false;
    public string joysticks;
    float gravity = 0.058f;
    float fall_speed = 0;
    bool begin = true, quit =false;
	// Use this for initialization
	void Start () {
        layerMask = LayerMask.GetMask("monster");
        BGM = GameObject.Find("BGM");
        town = GameObject.Find("BGM").transform.Find("Town").GetComponent<AudioSource>();
        fighting = GameObject.Find("BGM").transform.Find("Fight").GetComponent<AudioSource>();
        town.Play();
        fighting.Pause();
	}


    public void setCharacter(GameObject c)
    {
        Property_UI_Controller.ui.SetActive(true);
        TaskManager.Instance.UpdateTaskText();
        character = c;
        c.transform.parent = GameObject.Find("Controller").transform;
        myCController = character.GetComponent<CharacterController>();
        animator = character.GetComponent<Animator>();
        mainCamera = character.transform.Find("Camera").gameObject;

    }
    State currentState
    {
        get
        {
            State state = State.Idle;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                state = State.Idle;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                state = State.Run;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                state = State.Walk;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                state = State.Death;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TakeDamage2"))
                state = State.TakeDamage;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TakeDamage1"))
                state = State.TakeDamage;
            else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
                state = State.Attack1;
            else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Skill02"))
                state = State.Skill1;
            else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Skill01"))
                state = State.Skill2;

            return state;
        }
    }

    enum State
    {
        Idle,
        Run,
        Death,
        Attack1,
        TakeDamage,
        Skill1,
        Skill2,
        Walk
    };

    bool CheckMonster()
    {
        Collider[] cols = Physics.OverlapSphere(character.transform.position, 10f,layerMask);
        return cols.Length > 0;
    }

	// Update is called once per frame
	void Update () {

        if (myCController != null)
        {
            fight = CheckMonster();
            if (fight)
            {
                if (!fighting.isPlaying && !quit)
                {
                    fighting.Play();
                    town.Stop();
                    GameObject now = GameObject.FindGameObjectWithTag("Fighting");
                    if (now != null)
                    {
                        Destroy(now);
                    }
                        GameObject temp = Instantiate(Resources.Load("Feiz/Effect/BeginFighting")) as GameObject;
                        Destroy(temp, 2);
                    //Debug.Log("111");
                        quit = true;
                        begin = false;
                        
                }
            }
            else
            {
                if (!town.isPlaying && !begin)
                {
                    town.Play();
                    fighting.Stop();
                    GameObject now = GameObject.FindGameObjectWithTag("Fighting");
                    if (now != null)
                    {
                        Destroy(now);
                    }
                        GameObject temp = Instantiate(Resources.Load("Feiz/Effect/QuitFighting")) as GameObject;
                        Destroy(temp, 2);
                    //Debug.Log("222");
                        begin = true;
                        quit = false;
                }
                
            }
            BGM.transform.position = character.transform.position;
            if (Property_Controller.dead)
            {
                if (currentState != State.Death)
                    animator.SetTrigger("death");
                return;
            }
			if (Input.GetKey(KeyCode.Space) && myCController.isGrounded)
            {
                jumping();
            }
            if (!myCController.isGrounded)
            {
                fall_speed -= gravity;
                myCController.Move(new Vector3(0, fall_speed, 0));
                mainCamera.transform.position -= new Vector3(0, fall_speed, 0);
            }
            else
            {
                fall_speed = 0;
            }
            
            
            //mainCamera.transform.position = new Vector3(character.transform.position.x, 1.5f, character.transform.position.z) - 2 * character.transform.forward;
            mainCamera.transform.position = character.transform.position - 2 * character.transform.forward + 2*Vector3.up;
            mainCamera.transform.localEulerAngles = new Vector3(0, character.transform.localEulerAngles.y, 0);
            mainCamera.transform.LookAt(character.transform.position + Vector3.up);
        }
       
	}

    public void ClickAttack()
    {
        if (Property_Controller.dead)
        {
            return;
        }
        if (currentState != State.Attack1)
            animator.SetTrigger("attack1");

    }


    void jumping()
    {
        fall_speed = 1;
        fall_speed -= gravity;
        myCController.Move(new Vector3(0, fall_speed, 0));
    }

    public void attack()
    {
        
        //if (attackUI.fillAmount != 0 || myGame.pause)
        //{
        //    return;
        //}
        Ray ray1 = new Ray(transform.position - transform.forward + Vector3.up, transform.forward);
        Ray ray2 = new Ray(transform.position - transform.forward, transform.forward);
        RaycastHit hit;
        //myC.attack = true;
        //a_time = attack_cd;
        //if (Physics.Raycast(ray1, out hit, 5.0f))
        //{
        //    Debug.DrawLine(ray1.origin, hit.point);
        //    if (hit.collider.gameObject.tag == "monster")
        //    {
        //        hit.collider.gameObject.GetComponent<Monster_Manage>().Monster_Manage_Change_Hp(-Property_Controller.Instance.Get_AD());
        //    }
        //}
        //else 
        if (Physics.Raycast(ray1, out hit, 5.0f))
        {
            Debug.DrawLine(ray2.origin, hit.point);
            if (hit.collider.gameObject.tag == "monster")
            {
                
                //Debug.Log("next");
                hit.collider.gameObject.GetComponent<Monster_Manage>().Monster_Manage_Change_Hp(-Property_Controller.Instance.Get_AD());
                
                if (hit.collider.gameObject.GetComponent<Monster_Manage>().monster_id == 3)
                {
                    GameObject a = Instantiate(Resources.Load("Feiz/Voice/BossHit")) as GameObject;
                    a.transform.position = character.transform.position;
                    Destroy(a, 1);
                }
                else
                {
                    GameObject a = Instantiate(Resources.Load("Feiz/Voice/Hit")) as GameObject;
                    a.transform.position = character.transform.position;
                    Destroy(a, 1);
                }
            }
        }
    }

   public void SetColorOnSelect (GameObject a){
   }

    void OnEnable()
    {
			
        EasyJoystick.On_JoystickMove += OnJoystickMove;

        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;

    }

    //移动摇杆结束  

    void OnJoystickMoveEnd(MovingJoystick move)
    {

        //停止时，角色恢复idle  
        if (character == null||Property_Controller.dead)
            return;
        if (move.joystickName == joysticks)
        {

            if (currentState != State.Idle)
                animator.SetTrigger("idle");

        }

    }

    //移动摇杆中  

    void OnJoystickMove(MovingJoystick move)
    {
        //if (damage || game.pause || game.finish)
        //{
        //    return;
        //}
        if (character == null||Property_Controller.dead)
            return;
        if (move.joystickName != joysticks)
        {

            return;

        }



        //获取摇杆中心偏移的坐标  

        float joyPositionX = move.joystickAxis.x;

        float joyPositionY = move.joystickAxis.y;



        if (joyPositionY != 0 || joyPositionX != 0)
        {

            //移动玩家的位置（按朝向位置移动）  
            character.transform.Rotate(new Vector3(0, 1, 0), 5 * joyPositionX);
            myCController.Move(character.transform.forward * Time.deltaTime * joyPositionY * Property_Controller.my_Property.speed / 10);

            //播放奔跑动画  
            if (joyPositionY < 0.5 && joyPositionY > -0.5)
            {
                if (currentState != State.Walk)
                {
                    animator.SetTrigger("walk");
                }
            }
            else
            {
                if (currentState != State.Run)
                {
                    animator.SetTrigger("run");
                }
            }


        }

    }


}
