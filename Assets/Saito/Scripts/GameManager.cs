using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    bool is_over = false;

    //�Q�[���I���t���O
    public void OnGameOver(string _name)
    {
        Debug.Log(_name+"�̏���");

        is_over = true;
    }

    public bool IsOver()
    {
        return is_over;
    }

}
