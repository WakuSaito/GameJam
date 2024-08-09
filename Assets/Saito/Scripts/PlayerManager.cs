using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum Direction
{
    LEFT,
    RIGHT,
}

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    [SerializeField]
    Slider hpSlider;

    Umbrella umbrella = new Umbrella();//装備している傘
    //プレハブ
    [SerializeField]
    GameObject openUmbrella;
    [SerializeField]
    GameObject closeUmbrellaL;
    [SerializeField]
    GameObject closeUmbrellaR;

    GameObject umbrella_obj;//子の傘オブジェクト

    Direction direction;//向き

    [SerializeField]//地面レイヤー
    LayerMask ground_layer;

    [SerializeField]//移動最大速度
    float max_move_speed = 3.0f;
    float current_move_speed;//現在の速度
    [SerializeField] //ジャンプの力
    float jump_pow = 30.0f;

    [SerializeField]//最大体力
    int max_hp = 200;
    int current_hp; //現在の体力

    [SerializeField]//攻撃力
    int attack_damage = 20;

    [SerializeField]//無敵時間
    float invincible_sec = 0.5f;

    [SerializeField]//環境の効果を受ける間隔
    float take_effect_interval = 0.3f;
    float time_count = 0;//カウント用

    bool on_ground = false;      //地面の上かフラグ
    bool on_invincible = false;  //無敵フラグ
    bool is_under_cloud = false; //雲の下にいるか


    private void Awake()
    {
        //コンポーネント取得
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();


        ResetState();//リセット

        CreateUmbrella();//傘の見た目変更
    }

    private void Update()
    {
        // 地上判定
        on_ground = Physics2D.BoxCast(origin: boxCollider.bounds.center,
                                       size: boxCollider.bounds.size,
                                      angle: 0f,
                                  direction: Vector2.down,
                                   distance: 0.01f,
                                  layerMask: ground_layer
                                  );
        //移動方向更新
        float vec_x;
        if (direction == Direction.LEFT)
            vec_x = -1;
        else
            vec_x = 1;

        //移動
        if (current_move_speed > 0)
        {
            //Xベクトル更新
            rb.velocity = new Vector2(vec_x * current_move_speed, rb.velocity.y);

            current_move_speed -= 0.1f;//減速
        }

        //定期的に環境効果を受ける
        time_count += Time.deltaTime;//時間計測
        if (time_count >= take_effect_interval)
        {
            time_count = 0;//リセット
            //雲の下にいるとき回復
            if (is_under_cloud)
            {
                HealHP(1);
            }
            //傘が開いていなければダメージ
            else if (umbrella.GetState() != UMBRELLA_STATE.OPEN)
            {
                ReduceHP(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //攻撃に接触
        if (collision.gameObject.tag == "Attack")
        {
            TakeDamage(20);
        }
    }

    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    //X方向移動
    public void MoveX(Direction _dir)
    {
        SetDirection(_dir);//向き変更

        current_move_speed = max_move_speed;//速度初期化
    }

    //向き切り替え
    private void SetDirection(Direction _dir)
    {
        if (direction == _dir) return;//同じなら変更しない
        direction = _dir;//向き変更

        if (direction == Direction.LEFT)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;


        CreateUmbrella();
    }

    //ジャンプ
    public bool Jump()
    {
        if (!on_ground) return false;

        Debug.Log("ジャンプ実行");
        rb.AddForce(Vector2.up * jump_pow);//上方向に力を加える

        on_ground = false;

        return true;
    }

    //傘の開閉
    public void ChangeUmbrella()
    {
        Debug.Log("傘切り替え");
        umbrella.ChangeOpen();

        CreateUmbrella();
    }

    public void LostUmbrella()
    {
        Debug.Log("傘ロスト");
        umbrella.Lost();

        CreateUmbrella();
    }
    //傘オブジェクト作成
    private void CreateUmbrella()
    {
        if (umbrella_obj != null)//現在のオブジェクト削除
            Destroy(umbrella_obj);

        //オブジェクト作成
        if (umbrella.GetState() == UMBRELLA_STATE.OPEN)
        {
            umbrella_obj = Instantiate(openUmbrella, gameObject.transform);
        }
        else if(umbrella.GetState() == UMBRELLA_STATE.CLOSE)
        {
            if(direction == Direction.LEFT)
                umbrella_obj = Instantiate(closeUmbrellaL, gameObject.transform);
            else
                umbrella_obj = Instantiate(closeUmbrellaR, gameObject.transform);


        }
    }

    //被ダメージ
    public void TakeDamage(int _damage)
    {
        if (on_invincible) return;//無敵なら受けない
        Debug.Log("被ダメージ"+_damage);

        on_invincible = true;//無敵化
        //一定時間後無敵解除
        StartCoroutine(DelayCoroutine(invincible_sec, () =>
        {
            on_invincible = false;
        }));


        ReduceHP(_damage);
    }

    //hpを減らす
    public void ReduceHP(int _damage)
    {
        //体力計算
        int next_hp = current_hp - _damage;

        if (next_hp <= 0)
        {
            current_hp = 0;
            //死亡処理
        }
        else
        {
            //hpを減らす
            current_hp = next_hp;
        }
        Debug.Log(current_hp);
        //ゲージ更新
        hpSlider.value = (float)current_hp / (float)max_hp;

    }
    //回復
    public void HealHP(int _heal)
    {
        //体力計算
        int next_hp = current_hp + _heal;

        if (next_hp >= max_hp)
        {
            current_hp = max_hp;//最大まで回復
        }
        else
        {
            //hpを回復
            current_hp = next_hp;
        }

        //ゲージ更新
        hpSlider.value = (float)current_hp / (float)max_hp;
    }

    //現在の傘の状態取得
    public UMBRELLA_STATE GetUmbrellaState()
    {
        return umbrella.GetState();
    }

    //状態リセット
    public void ResetState()
    {
        on_invincible = false;
        on_ground = false;
        current_hp = max_hp;
        current_move_speed = 0.0f;
        umbrella.state = UMBRELLA_STATE.OPEN;
    }
}
