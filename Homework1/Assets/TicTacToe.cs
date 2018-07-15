using UnityEngine;
using System.Collections.Generic;

public class TicTacToe : MonoBehaviour
{
    private enum State
    {
        Defult = 0,
        X = 1,
        O = 2
    };

    struct Coordinate
    {
        public int x;
        public int y;
    };

    private List<string> map = new List<string> { "", "X", "O" };
    private bool playing = true;
    private bool turn = true;
    private State[,] board = new State[3, 3];
    private State winner = State.Defult;

    private int btnHeight = 100;
    private int btnWidth = 100;
    private float height = Screen.height * 0.5f - 150;
    private float width = Screen.width * 0.5f - 150;

    private Stack<Coordinate> stack = new Stack<Coordinate>();

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(width + 4 * btnWidth, height + 40, btnWidth, btnHeight), "Withdraw"))
        {
            Withdraw();
            return;
        }

        if (GUI.Button(new Rect(width + 4 * btnWidth, height + 160, btnWidth, btnHeight), "Reset"))
        {
            Reset();
            return;
        }

        if (winner != State.Defult)
        {
            string msg = map[(int)winner] + " Wins!";
            GUI.Label(new Rect(width - 120, height + 50, 100, 100), msg, new GUIStyle
            {
                fontSize = 25,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.red }
            });
            playing = !playing;
        }

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (GUI.Button(new Rect(width + i * btnWidth, height + j * btnHeight, btnWidth, btnHeight), map[(int)board[i, j]]))
                {
                    if (board[i, j] == State.Defult && playing)
                    {
                        board[i, j] = turn ? State.X : State.O;
                        turn = !turn;
                        winner = Check();
                        stack.Push(new Coordinate() { x = i, y = j });
                        GUI.enabled = true;
                    }
                }
            }
        }
    }

    private State Check()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (board[i, 0] != State.Defult &&
                board[i, 0] == board[i, 1] &&
                board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }
        for (int j = 0; j < 3; ++j)
        {
            if (board[0, j] != State.Defult &&
                board[0, j] == board[1, j] &&
                board[1, j] == board[2, j])
            {
                return board[0, j];
            }
        }
        if (board[1, 1] != State.Defult)
        {
            if (board[1, 1] == board[0, 0] && board[1, 1] == board[2, 2] ||
                board[1, 1] == board[0, 2] && board[1, 1] == board[2, 0])
            {
                return board[1, 1];
            }
        }
        return State.Defult;
    }


    private void Withdraw()
    {
        if (!playing)
            return;
        if (stack.Count > 0)
        {
            Coordinate co = stack.Pop();
            board[co.x, co.y] = State.Defult;
            turn = !turn;
        }
        GUI.enabled = true;
    }

    private void Reset()
    {
        playing = true;
        turn = true;
        winner = State.Defult;
        stack.Clear();
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                board[i, j] = State.Defult;
            }
        }
        GUI.enabled = true;
    }
}