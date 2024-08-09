using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager[] playerManager;//�v���C���[

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsOver()) return;//�Q�[���I���Ȃ���͂��󂯕t���Ȃ�

        //�v���C���[�P
        if (playerManager[0] != null)
        {
            //�ړ�
            if (Input.GetKey(KeyCode.D))
            {
                playerManager[0].MoveX(Direction.RIGHT);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerManager[0].MoveX(Direction.LEFT);
            }

            //�W�����v
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerManager[0].Jump();
            }

            //�P�J��
            if(Input.GetKeyDown(KeyCode.S))
            {
                playerManager[0].ChangeUmbrella();
            }
        }

        //�v���C���[�Q
        if(playerManager[1]!=null)
        {
            //�ړ�
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerManager[1].MoveX(Direction.RIGHT);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerManager[1].MoveX(Direction.LEFT);
            }

            //�W�����v
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerManager[1].Jump();
            }

            //�P�J��
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerManager[1].ChangeUmbrella();
            }
        }

    }
}
