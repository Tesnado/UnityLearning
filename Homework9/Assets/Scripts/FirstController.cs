using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestAndDevil;

public class FirstController : MonoBehaviour, ISceneController, IUserController
{
    public static FirstController Instance;
    readonly Vector3 water_pos = new Vector3(0, 0.5F, 0);

    //UserGUI userGUI;
    [HideInInspector]
    public CCActionManager actionManager;
    public CoastController fromCoast;
    public CoastController toCoast;
    public BoatController boat;
    public AIController ai;
    private MyCharacterController[] characters;
    [HideInInspector]
    public string min_plan;

    void Awake()
    {
        Instance = this;
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        //userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new MyCharacterController[6];
        loadResources();
        InitUI();
    }

    void InitUI()
    {
        Instantiate(Resources.Load("UI"));
        //UIController.Instance.SetHintEvent();
    }

    public void loadResources()
    {
        GameObject water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
        water.name = "water";

        fromCoast = new CoastController("from");
        toCoast = new CoastController("to");
        boat = new BoatController();
        ai = new AIController();
        loadCharacter();
    }

    private void loadCharacter()
    {
        for (int i = 0; i < 3; i++)
        {
            MyCharacterController cha = new MyCharacterController("priest");
            cha.setName("priest" + i);
            cha.setPosition(fromCoast.getEmptyPosition());
            cha.getOnCoast(fromCoast);
            fromCoast.getOnCoast(cha);

            characters[i] = cha;
        }

        for (int i = 0; i < 3; i++)
        {
            MyCharacterController cha = new MyCharacterController("devil");
            cha.setName("devil" + i);
            cha.setPosition(fromCoast.getEmptyPosition());
            cha.getOnCoast(fromCoast);
            fromCoast.getOnCoast(cha);

            characters[i + 3] = cha;
        }
    }

    //--------------action----------------
    public void moveBoat()
    {
        if (boat.isEmpty() || check_game_over() != 0)
            return;
        actionManager.moveBoat(boat);
        boat.Move();
        UIController.Instance.Refresh(check_game_over());
        //userGUI.status = check_game_over();
    }

    void resetBoat()
    {
        int boat_state = boat.get_to_or_from();
        if (boat_state == -1)
        {
            actionManager.moveBoat(boat);
            boat.Move();
            boat.resetBoatPassenger();
        }
    }

    public void characterIsClicked(MyCharacterController characterCtrl)
    {
        if (characterCtrl.isOnBoat())
        {
            CoastController whichCoast;
            if (boat.get_to_or_from() == -1)
            { // to->-1; from->1
                whichCoast = toCoast;
            }
            else
            {
                whichCoast = fromCoast;
            }

            boat.GetOffBoat(characterCtrl.getName());
            //characterCtrl.moveToPosition(whichCoast.getEmptyPosition());
            actionManager.moveCharacter(characterCtrl, whichCoast.getEmptyPosition());
            characterCtrl.getOnCoast(whichCoast);
            whichCoast.getOnCoast(characterCtrl);
        }
        else
        {                                   // character on coast
            CoastController whichCoast = characterCtrl.getCoastController();
            if (boat.getEmptyIndex() == -1)
            {       // boat is full
                return;
            }
            if (whichCoast.get_to_or_from() != boat.get_to_or_from())   // boat is not on the side of character
                return;

            whichCoast.getOffCoast(characterCtrl.getName());
            //characterCtrl.moveToPosition(boat.getEmptyPosition());
            actionManager.moveCharacter(characterCtrl, boat.getEmptyPosition());
            characterCtrl.getOnBoat(boat);
            boat.GetOnBoat(characterCtrl);
        }
    }

    private void Update()
    {
        //UIController.Instance.Refresh(check_game_over());
        //userGUI.status = check_game_over();
    }

    public int check_game_over()
    {   // 0->not finish, 1->lose, 2->win
        //if (userGUI.timer < 0) return 1;
        int from_priest = 0;
        int from_devil = 0;
        int to_priest = 0;
        int to_devil = 0;

        int[] fromCount = fromCoast.getCharacterNum();
        from_priest += fromCount[0];
        from_devil += fromCount[1];

        int[] toCount = toCoast.getCharacterNum();
        to_priest += toCount[0];
        to_devil += toCount[1];

        if (to_priest + to_devil == 6)      // win
            return 2;

        int[] boatCount = boat.getCharacterNum();
        if (boat.get_to_or_from() == -1)
        {   // boat at toCoast
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        else
        {   // boat at fromCoast
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        if (from_priest < from_devil && from_priest > 0)
        {       // lose
            return 1;
        }
        if (to_priest < to_devil && to_priest > 0)
        {
            return 1;
        }
        return 0;           // not finish
    }

    public void restart()
    {
        resetBoat();
        fromCoast.reset();
        toCoast.reset();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].reset();
        }
        //userGUI.timer = 120;
    }

    public void moveAI()
    {
        int[] plan = ai.getMovePlan(this.getCurrentState());
        if (plan == null)
        {
            Debug.LogError("Error!");
            return;
        }
        string hint = "";
        switch (plan[0])
        {
            case (int)AIController.edge.D:
                hint = "1Devial";
                break;
            case (int)AIController.edge.DD:
                hint = "2Devial";
                break;
            case (int)AIController.edge.P:
                hint = "1Priest";
                break;
            case (int)AIController.edge.PD:
                hint = "1Priest and 1Devil";
                break;
            case (int)AIController.edge.PP:
                hint = "2Priest";
                break;
        }
        int boat_state = boat.get_to_or_from();
        hint = boat_state == 1 ? ("<==" + hint) : (hint + "==>");
        hint = hint + "\n";
        UIController.Instance.ShowTxtHint(hint);
    }

    public string getMinPlan()
    {
        return min_plan;
    }

    /*
     * 获取当前状态
     */
    public AIController.point getCurrentState()
    {
        int from_priest = 0;
        int from_devil = 0;

        int[] fromCount = fromCoast.getCharacterNum();
        from_priest += fromCount[0];
        from_devil += fromCount[1];
        int boat_state = boat.get_to_or_from();
        MyCharacterController[] p_in_boat = boat.getPassengers();
        if (boat_state == 1)
        {
            foreach (var p in p_in_boat)
            {
                if (p != null)
                {
                    if (p.characterType == 1)
                    {
                        from_devil++;
                    }
                    else
                    {
                        from_priest++;
                    }
                }
            }

        }
        AIController.point curPoint = AIController.point.UNDONE;              //随便赋值的
        if (from_priest == 3 && from_devil == 3 && boat_state == 1)
        {
            curPoint = AIController.point.P3D3B;
        }
        else if (from_priest == 3 && from_devil == 2 && boat_state == -1)
        {
            curPoint = AIController.point.P3D2;
        }
        else if (from_priest == 3 && from_devil == 1 && boat_state == -1)
        {
            curPoint = AIController.point.P3D1;
        }
        else if (from_priest == 3 && from_devil == 2 && boat_state == 1)
        {
            curPoint = AIController.point.P3D2B;
        }
        else if (from_priest == 2 && from_devil == 2 && boat_state == -1)
        {
            curPoint = AIController.point.P2D2;
        }
        else if (from_priest == 3 && from_devil == 0 && boat_state == -1)
        {
            curPoint = AIController.point.P3D0;
        }
        else if (from_priest == 3 && from_devil == 1 && boat_state == 1)
        {
            curPoint = AIController.point.P3D1B;
        }
        else if (from_priest == 1 && from_devil == 1 && boat_state == -1)
        {
            curPoint = AIController.point.P1D1;
        }
        else if (from_priest == 2 && from_devil == 2 && boat_state == 1)
        {
            curPoint = AIController.point.P2D2B;
        }
        else if (from_priest == 0 && from_devil == 2 && boat_state == -1)
        {
            curPoint = AIController.point.P0D2;
        }
        else if (from_priest == 0 && from_devil == 3 && boat_state == 1)
        {
            curPoint = AIController.point.P0D3B;
        }
        else if (from_priest == 0 && from_devil == 1 && boat_state == -1)
        {
            curPoint = AIController.point.P0D1;
        }
        else if (from_priest == 2 && from_devil == 1 && boat_state == 1)
        {
            curPoint = AIController.point.P2D1B;
        }
        else if (from_priest == 1 && from_devil == 1 && boat_state == 1)
        {
            curPoint = AIController.point.P1D1B;
        }
        else if (from_priest == 0 && from_devil == 2 && boat_state == 1)
        {
            curPoint = AIController.point.P0D2B;
        }
        else if (from_priest == 0 && from_devil == 0 && boat_state == -1)
        {
            curPoint = AIController.point.P0D0;
        }

        return curPoint;
    }
}