using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class StaticData
{
    public static string winner_name;//���������v���C���[�̖��O
}


public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager[] playerManagers;

    bool is_over = false;


    //�Q�[���I���t���O
    public void OnGameOver(string _loser_name)
    {
        PlayerManager win_player = GetOtherPlayer(_loser_name);
        if (win_player == null) return;

        //�������v���C���[�ȊO�̖��O�擾
        string win_name = win_player.GetName();

        Debug.Log(win_name + "�̏���");
        is_over = true;
        StaticData.winner_name = win_name;//�V�[�����ׂ��Ŏ����Ă���

        // ResultScene�ɐ؂�ւ�
        SceneManager.LoadScene("syouhai");
    }

    public PlayerManager GetOtherPlayer(string _name)
    {
        //���O�������łȂ��v���C���[���擾
        foreach (var pm in playerManagers)
        {
            string name = pm.GetName();//���O�擾
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
