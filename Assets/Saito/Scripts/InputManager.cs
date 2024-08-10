using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager[] playerManager;//プレイヤー

    GameManager gameManager;

    public float success_sec = 1.0f;//長押し成功時間

    //長押しカウント
    bool is_count_s = false;
    float push_s_count;
    bool is_count_down = false;
    float push_down_count;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsOver()) return;//ゲーム終了なら入力を受け付けない

        //プレイヤー１
        if (playerManager[0] != null)
        {
            //移動
            if (Input.GetKey(KeyCode.D))
            {
                playerManager[0].MoveX(Direction.RIGHT);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerManager[0].MoveX(Direction.LEFT);
            }

            //ジャンプ
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerManager[0].Jump();
            }

            //傘開閉
            if(Input.GetKeyDown(KeyCode.S))
            {
                is_count_s = true;//長押し開始
            }
            else if(Input.GetKeyUp(KeyCode.S))
            {
                push_s_count = 0;
                is_count_s = false;//長押し停止
            }
            if(is_count_s)//長押し処理
            {
                push_s_count += Time.deltaTime;
                if (push_s_count >= success_sec)
                {
                    playerManager[0].ChangeUmbrella();//傘を開く
                    push_s_count = 0;
                    is_count_s = false;//長押し停止
                }
            }

        }

        //プレイヤー２
        if(playerManager[1]!=null)
        {
            //移動
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerManager[1].MoveX(Direction.RIGHT);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerManager[1].MoveX(Direction.LEFT);
            }

            //ジャンプ
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerManager[1].Jump();
            }

            //傘開閉
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                is_count_down = true;//長押し開始
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                push_down_count = 0;
                is_count_down = false;//長押し停止
            }
            if (is_count_down)//長押し処理
            {
                push_down_count += Time.deltaTime;
                if (push_down_count >= success_sec)
                {
                    playerManager[1].ChangeUmbrella();//傘を開く
                    push_down_count = 0;
                    is_count_down = false;//長押し停止
                }
            }

        }

    }
}
