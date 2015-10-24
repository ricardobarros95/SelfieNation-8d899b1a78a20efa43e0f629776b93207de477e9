using UnityEngine;
using System.Collections;

public class setLeaderBoard : MonoBehaviour {



    public void setLeaderBoardZ(int i)
    {
        GameManager.me.leaderBoard = i;
    }
}
