using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�{�^���������ꂽ��
        if (Input.anyKeyDown)
        {
            // PlayScene�ɐ؂�ւ�
            SceneManager.LoadScene("Saito");
        }
    }
}