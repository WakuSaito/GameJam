using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gage : MonoBehaviour
{
    umbrella umbrella;

    //gageオブジェクト
    [SerializeField] GameObject gage_obj;
    //傘情報取得用
    [SerializeField] GameObject umbrella_obj;
    //最大体力
    [SerializeField] int max_hp;
    //１ダメージ当たりのゲージを減らす量
    float reduce_hp;
    //体力減らす間隔
    [SerializeField] int damege_time;

    float damage_speed;
    bool damage_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        //傘情報
        umbrella = umbrella_obj.GetComponent<umbrella>();
        //１ダメージ当たりのゲージを減らす量
        reduce_hp = gage_obj.GetComponent<RectTransform>().sizeDelta.x / max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (umbrella.state_flag == UMBRELLA_STATE.OPEN && !damage_flag)
        {
            Damege();
        }

        if (damage_flag)
        {
            Debug.Log(damage_speed);
            damage_speed += Time.deltaTime;
            if (damage_speed > damege_time)
            {
                damage_flag = false;
                damage_speed = 0;
            }
        }
    }

    public void Damege()
    {
        if (gage_obj.GetComponent<RectTransform>().sizeDelta.x > 0)
        {
            // 体力ゲージの幅と高さをVector2で取り出す(Width,Height)
            Vector2 nowsafes = gage_obj.GetComponent<RectTransform>().sizeDelta;
            // 体力ゲージの幅からダメージ分の幅を引く
            nowsafes.x -= reduce_hp;
            // 体力ゲージに計算済みのVector2を設定する
            gage_obj.GetComponent<RectTransform>().sizeDelta = nowsafes;
            damage_flag = true;
        }
    }

}
