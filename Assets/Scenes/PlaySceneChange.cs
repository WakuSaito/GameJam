using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�{�^���������ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ResultScene�ɐ؂�ւ�
            SceneManager.LoadScene("ResultScene");
        }
    }
}
