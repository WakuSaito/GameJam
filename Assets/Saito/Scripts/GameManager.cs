using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class StaticData
{
    public static string winner_name;//勝利したプレイヤーの名前
}


public class GameManager : MonoBehaviour
{

    bool is_over = false;

    //ゲーム終了フラグ
    public void OnGameOver(string _name)
    {
        Debug.Log(_name+"の勝利");

        is_over = true;
        StaticData.winner_name = _name;//シーンを跨いで持っていく

        // ResultSceneに切り替え
        SceneManager.LoadScene("syouhai");
    }

    public bool IsOver()
    {
        return is_over;
    }

}
