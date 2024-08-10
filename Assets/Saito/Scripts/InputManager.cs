using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager[] playerManager;//�v���C���[

    GameManager gameManager;

    public float success_sec = 1.0f;//��������������

    //�������J�E���g
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
                is_count_s = true;//�������J�n
            }
            else if(Input.GetKeyUp(KeyCode.S))
            {
                push_s_count = 0;
                is_count_s = false;//��������~
            }
            if(is_count_s)//����������
            {
                push_s_count += Time.deltaTime;
                if (push_s_count >= success_sec)
                {
                    playerManager[0].ChangeUmbrella();//�P���J��
                    push_s_count = 0;
                    is_count_s = false;//��������~
                }
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
                is_count_down = true;//�������J�n
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                push_down_count = 0;
                is_count_down = false;//��������~
            }
            if (is_count_down)//����������
            {
                push_down_count += Time.deltaTime;
                if (push_down_count >= success_sec)
                {
                    playerManager[1].ChangeUmbrella();//�P���J��
                    push_down_count = 0;
                    is_count_down = false;//��������~
                }
            }

        }

    }
}
