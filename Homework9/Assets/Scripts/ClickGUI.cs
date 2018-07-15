using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestAndDevil;

public class ClickGUI : MonoBehaviour
{
    IUserController action;
    MyCharacterController characterController;

    public void setController(MyCharacterController characterCtrl)
    {
        characterController = characterCtrl;
    }

    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserController;
    }


    void OnMouseDown()
    {
        if(FirstController.Instance.check_game_over() != 0)
            return;
        if (gameObject.name == "boat")
        {
            action.moveBoat();
        }
        else
        {
            action.characterIsClicked(characterController);
        }
    }
}
