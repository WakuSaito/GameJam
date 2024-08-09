using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    bool is_over = false;

    //ゲーム終了フラグ
    public void OnGameOver(string _name)
    {
        Debug.Log(_name+"の勝利");

        is_over = true;
    }

    public bool IsOver()
    {
        return is_over;
    }

}
