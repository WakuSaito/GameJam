using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += 0.00f;    // x座標へ0.00加算
        pos.y -= 0.01f;    // y座標へ-0.01加算
        pos.z += 0.00f;    // z座標へ0.00加算

        transform.position = pos;  // 座標を設定

        //雨が一番下に行った時の処理
        if (-8.5f > transform.position.y)
        //雨がdelete_rainタグのついたオブジェクトに接触したら
        {
            Destroy(gameObject);//消す
        }

    }
}
