using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager[] playerManager;//�v���C���[

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�P
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerManager[0].Jump();
            }

            //�P�J��
            if(Input.GetKeyDown(KeyCode.F))
            {
                playerManager[0].ChangeUmbrella();
            }
        }

        //�v���C���[�Q
        {
            //�ړ�
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerManager[0].MoveX(Direction.RIGHT);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerManager[0].MoveX(Direction.LEFT);
            }

            //�W�����v
            if (Input.GetMouseButtonDown(0))
            {
                playerManager[0].Jump();
            }

            //�P�J��
            if (Input.GetMouseButtonDown(1))
            {
                playerManager[0].ChangeUmbrella();
            }
        }

    }
}
