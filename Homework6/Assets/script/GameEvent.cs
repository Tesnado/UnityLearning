using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public delegate void GameScoreAction();
    public static event GameScoreAction myGameScoreAction;

    public delegate void GameOverAction();
    public static event GameOverAction myGameOverAction;

    private GameController scene;

    void Start()
    {
        scene = GameController.getInstance();
        
    }

    void Update()
    {

    }

    //hero逃离巡逻兵，得分
    public void heroEscapeAndScore()
    {
        if (myGameScoreAction != null)
            myGameScoreAction();
    }

    //巡逻兵捕获hero，游戏结束
    public void patrolHitHeroAndGameover()
    {
        if (myGameOverAction != null)
            myGameOverAction();
    }
}
