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
    //自コンポーネント
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    PlayerHP playerHP;//体力クラス
    Umbrella umbrella;//装備している傘

    GameManager gameManager;//ゲームマネージャクラス

    [SerializeField]
    string player_name = "player";

    [SerializeField]
    Slider hpSlider;

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

        //クラス作成
        playerHP = new PlayerHP(max_hp);
        umbrella = new Umbrella();

        //クラス取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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

        if (gameManager.IsOver()) return;//ゲーム終了なら効果を受けない

        //定期的に環境効果を受ける
        time_count += Time.deltaTime;//時間計測
        if (time_count >= take_effect_interval)
        {
            time_count = 0;//リセット
            if (umbrella.GetState() != UMBRELLA_STATE.OPEN)
            {

                //雲の下にいるとき回復
                if (is_under_cloud)
                {
                    playerHP.HealHP(1);
                }
                //傘が開いていなければダメージ
                else
                {
                    playerHP.ReduceHP(1);
                }

                UpdateHPBar();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //攻撃に接触
        if (collision.gameObject.tag == "Attack")
        {
            TakeDamage(attack_damage);
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //雨に入ったとき
        if (collision.gameObject.tag == "rain")
        {
            is_under_cloud = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //雨から出たとき
        if (collision.gameObject.tag == "rain")
        {
            is_under_cloud = false;
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


        playerHP.ReduceHP(_damage);//hpを減らす
        UpdateHPBar();
    }

    //HPバー更新
    private void UpdateHPBar()
    {
        float per = playerHP.GetPercentage();//割合取得

        if (per <= 0) gameManager.OnGameOver(player_name);//ゲームオーバー処理

        hpSlider.value = per;//ゲージ更新
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
        playerHP.HealHP(10000);
        current_move_speed = 0.0f;
        umbrella.state = UMBRELLA_STATE.OPEN;
    }
}
