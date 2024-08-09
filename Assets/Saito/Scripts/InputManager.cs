using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager[] playerManager;//プレイヤー

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                playerManager[0].ChangeUmbrella();
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
                playerManager[1].ChangeUmbrella();
            }
        }

    }
}
