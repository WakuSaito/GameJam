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
        PlayerManager win_player = GetOtherPlayer(_loser_name);
        if (win_player == null) return;

        //負けたプレイヤー以外の名前取得
        string win_name = win_player.GetName();

        Debug.Log(win_name + "の勝利");
        is_over = true;
        StaticData.winner_name = win_name;//シーンを跨いで持っていく

        // ResultSceneに切り替え
        SceneManager.LoadScene("syouhai");
    }

    public PlayerManager GetOtherPlayer(string _name)
    {
        //名前が同じでないプレイヤーを取得
        foreach (var pm in playerManagers)
        {
            string name = pm.GetName();//名前取得
            if (name != _name)
            {
                return pm;
            }
        }
        return null;
    }

    public bool IsOver()
    {
        return is_over;
    }

}
