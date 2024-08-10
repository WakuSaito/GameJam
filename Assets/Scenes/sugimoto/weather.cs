using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weather : MonoBehaviour
{
    SE se;

    //生成したいプレイハブオブジェクト
    [SerializeField] GameObject rain_prefab;
    [SerializeField] GameObject wind_prefab;
    [SerializeField] GameObject attention_effect;
    GameObject canvas;

    //生成したクローンオブジェクト
    GameObject rain_obj;
    GameObject wind_obj;

    //生成したい位置（複数可）
    [SerializeField] Transform[] rain_pos;
    [SerializeField] Transform[] wind_pos;

    //各動作までのタイマー
    float rain_timer = 0.0f;
    float wind_timer = 0.0f;

    //スポーン間隔
    [SerializeField] float rain_spawn_timer;
    [SerializeField] float wind_spawn_timer;
    //デスポーン間隔
    [SerializeField] float rain_death_spawn_timer;
    [SerializeField] float wind_death_spawn_timer;

    const float ATTENTION_TIME = 0.75f; 

    bool is_wind;//風が吹いているか

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");//キャンバス取得
        se = GameObject.Find("Audio_SE").GetComponent<SE>();

    }

    // Update is called once per frame
    void Update()
    {
        //タイマー増加
        rain_timer += Time.deltaTime;
        wind_timer += Time.deltaTime;

        //雨雲生成
        if (rain_timer > rain_spawn_timer && rain_obj == null)  
        {
            //ランダムな位置に生成
            int i = UnityEngine.Random.Range(0, rain_pos.Length);
            rain_obj = Instantiate(rain_prefab, rain_pos[i], true);
            rain_obj.transform.position = rain_pos[i].position;
            rain_timer = 0;
            se.PlayAudio(se.cloud_spwan);//se
        }
        //雨雲削除
        if (rain_timer > rain_death_spawn_timer && rain_obj != null)  
        {
            Destroy(rain_obj);
            rain_timer = 0;
        }
        

        //風生成
        if (wind_timer > (wind_spawn_timer- ATTENTION_TIME) && wind_obj == null) 
        {
            Debug.Log("Attention");

            wind_timer = 0;//タイマーリセット

            if (canvas != null && attention_effect != null)
            {
                //キャンバス直下に注意を出す
                Instantiate(attention_effect, canvas.transform);
            }
            //注意が消えたあたりで風生成
            StartCoroutine(DelayCoroutine(ATTENTION_TIME, () =>
            {
                //ランダムな位置に生成
                int i = UnityEngine.Random.Range(0, wind_pos.Length);
                wind_obj = Instantiate(wind_prefab, wind_pos[i], true);
                wind_obj.transform.position = wind_pos[i].position;
                is_wind = true;
                se.PlayAudio(se.wind);//se

            }));
        }
        //風削除
        if (wind_timer > wind_death_spawn_timer && wind_obj != null)
        {
            Destroy(wind_obj);
            wind_timer = 0;
            is_wind = false;
        }
    }

    public bool IsWind()
    {
        return is_wind;
    }

    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

}
