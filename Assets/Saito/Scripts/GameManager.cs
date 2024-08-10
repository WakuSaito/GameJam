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
    [SerializeField]
    PlayerManager[] playerManagers;

    bool is_over = false;


    //ゲーム終了フラグ
    public void OnGameOver(string _loser_name)
    {
        string win_name;

        //負けたプレイヤー以外を取得
        foreach(var pm in playerManagers)
        {
            string name = pm.GetName();//名前取得
            if (name != _loser_name)
            {
                win_name = name;
                Debug.Log(win_name + "の勝利");
                is_over = true;
                StaticData.winner_name = win_name;//シーンを跨いで持っていく

                // ResultSceneに切り替え
                SceneManager.LoadScene("syouhai");
                break;
            }
        }        
    }

    public bool IsOver()
    {
        return is_over;
    }

}
