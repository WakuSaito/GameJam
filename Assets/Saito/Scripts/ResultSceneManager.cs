using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class ResultSceneManager : MonoBehaviour
{
    SE se;

    [SerializeField]//名前のテキスト
    Text name_text;

    bool can_change = false;//シーン切り替え可能

    private void Awake()
    {
        se = GameObject.Find("Audio_SE").GetComponent<SE>();
    }

    // Start is called before the first frame update
    void Start()
    {
        name_text.text = StaticData.winner_name;
        can_change = false;
        se.PlayAudio(se.clear_se);//se

        StartCoroutine(DelayCoroutine(2.0f, () =>
        {
            //一定時間後シーン切り替え可能に
            can_change = true;
        }));
    }

    // Update is called once per frame
    void Update()
    {
        // スペースボタンが押されたら
        if (Input.anyKeyDown)
        {
            //切り替え可能
            if (can_change)
                // TitleSceneに切り替え
                SceneManager.LoadScene("kuriya");
        }
    }

    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }


}
