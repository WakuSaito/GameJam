using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // スペースボタンが押されたら
        if (Input.anyKeyDown)
        {
            // PlaySceneに切り替え
            SceneManager.LoadScene("Saito");
        }
    }
}