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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerManager[0].Jump();
            }

            //傘開閉
            if(Input.GetKeyDown(KeyCode.F))
            {
                playerManager[0].ChangeUmbrella();
            }
        }

        //プレイヤー２
        {
            //移動
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerManager[0].MoveX(Direction.RIGHT);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerManager[0].MoveX(Direction.LEFT);
            }

            //ジャンプ
            if (Input.GetMouseButtonDown(0))
            {
                playerManager[0].Jump();
            }

            //傘開閉
            if (Input.GetMouseButtonDown(1))
            {
                playerManager[0].ChangeUmbrella();
            }
        }

    }
}
