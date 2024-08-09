using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    LEFT,
    RIGHT,
}

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;

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

    bool on_ground = false;     //地面の上かフラグ
    bool on_invincible = false;  //無敵フラグ

    private void Awake()
    {
        //コンポーネント取得
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        ResetState();//リセット
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
    }

    //X方向移動
    public void MoveX(Direction _dir)
    {
        direction = _dir;//向き変更

        current_move_speed = max_move_speed;//速度初期化
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
    }

    //被ダメージ
    public void Damage(int _damage)
    {
        Debug.Log("被ダメージ");

        //体力計算
        int next_hp = current_hp - _damage;

        if(next_hp<=0)
        {
            current_hp = 0;
            //死亡処理
        }
        else
        {
            //hpを減らす
            current_hp = next_hp;
        }

        //ゲージ更新
    }

    //現在の傘の状態取得
    public void GetUmbrellaState()
    {

    }

    //状態リセット
    public void ResetState()
    {
        on_invincible = false;
        on_ground = false;
        current_hp = max_hp;
        current_move_speed = 0.0f;
    }
}
