using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneChange : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�{�^���������ꂽ��
        if (Input.anyKeyDown)
        {
            // TitleScene�ɐ؂�ւ�
            SceneManager.LoadScene("kuriya");
        }
    }
}
