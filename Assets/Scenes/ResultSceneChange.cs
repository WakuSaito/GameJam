using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // スペースボタンが押されたら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TitleSceneに切り替え
            SceneManager.LoadScene("TitleScene");
        }
    }
}
