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

    bool is_over = false;

    //�Q�[���I���t���O
    public void OnGameOver(string _name)
    {
        Debug.Log(_name+"�̏���");

        is_over = true;
        StaticData.winner_name = _name;//�V�[�����ׂ��Ŏ����Ă���

        // ResultScene�ɐ؂�ւ�
        SceneManager.LoadScene("syouhai");
    }

    public bool IsOver()
    {
        return is_over;
    }

}
