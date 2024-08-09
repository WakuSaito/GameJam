using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public GameObject rain_prefab;
    public GameObject rainObj;
    public GameObject delete_line;
    float rain_timer = 0.0f;
    public float rain_spawn_timer;
    Transform myTransform;

    bool test = false;
    private void Update()
    {
        //タイマー増加
        rain_timer += Time.deltaTime;
        if (rain_timer >= rain_spawn_timer && rainObj==null)
        {
            rainObj = Instantiate(rain_prefab);//prefabを生成
            rainObj.transform.position = transform.position;
            // transformを取得
            //test = true;
            rain_timer = 0.0f;
        }



        if (rainObj!=null)
        {
            // 座標を取得
            myTransform = rainObj.transform;

            Vector3 pos = myTransform.position;
            pos.x += 0.00f;    // x座標へ0.00加算
            pos.y -= 0.01f;    // y座標へ-0.01加算
            pos.z += 0.00f;    // z座標へ0.00加算

            myTransform.position = pos;  // 座標を設定

            //雨が一番下に行った時の処理
            if (delete_line.transform.position.y > rainObj.transform.position.y)
            //雨がdelete_rainタグのついたオブジェクトに接触したら
            {
                Destroy(rainObj.gameObject);//消す
                rainObj = null;
            }
        }

    }



    //void OnTriggerEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "player")//プレイヤーが雨に当たったら
    //    {
    //        Debug.Log("雨に当たっています");
    //        //ゲージが回復
    //    }


    //}


    //雨とプレイヤーが当たった時の処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")//プレイヤーが雨に当たったら
        {
            Debug.Log("雨に当たっています");
            //ゲージが回復
        }
      
    }

}
