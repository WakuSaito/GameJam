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
        string win_name;

        //�������v���C���[�ȊO���擾
        foreach(var pm in playerManagers)
        {
            string name = pm.GetName();//���O�擾
            if (name != _loser_name)
            {
                win_name = name;
                Debug.Log(win_name + "�̏���");
                is_over = true;
                StaticData.winner_name = win_name;//�V�[�����ׂ��Ŏ����Ă���

                // ResultScene�ɐ؂�ւ�
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
