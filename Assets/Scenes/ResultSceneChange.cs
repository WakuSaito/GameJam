using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneChange : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // スペースボタンが押されたら
        if (Input.anyKeyDown)
        {
            // TitleSceneに切り替え
            SceneManager.LoadScene("kuriya");
        }
    }
}
