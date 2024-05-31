using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Use this for initialization

    static int state;

    public enum GameState
    {
        MainMenu = 0,
        Playing = 1,
        Pause = 2,
        Boss = 3
    }

    public static int State
    {
        get { return state; }
        set { state = value; }
    }

}
