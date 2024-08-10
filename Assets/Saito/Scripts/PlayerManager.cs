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
    //���R���|�[�l���g
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    PlayerHP playerHP;//�̗̓N���X
    Umbrella umbrella;//�������Ă���P

    GameManager gameManager;//�Q�[���}�l�[�W���N���X
    Weather weather;
    SE se;

    [SerializeField]
    string player_name = "player";

    [SerializeField]//HP�o�[
    Slider hpSlider;
    [SerializeField]//�_���[�W�G�t�F�N�g
    GameObject hitEffect;

    //�v���n�u
    [SerializeField]
    GameObject openUmbrella;
    [SerializeField]
    GameObject closeUmbrellaL;
    [SerializeField]
    GameObject closeUmbrellaR;

    GameObject umbrella_obj;//�q�̎P�I�u�W�F�N�g

    Direction direction;//����

    [SerializeField]//�n�ʃ��C���[
    LayerMask ground_layer;

    [SerializeField]//�ړ��ő呬�x
    float max_move_speed = 3.0f;
    float current_move_speed;//���݂̑��x
    [SerializeField] //�W�����v�̗�
    float jump_pow = 30.0f;

    [SerializeField]//�ő�̗�
    int max_hp = 200;

    [SerializeField]//�U����
    int attack_damage = 20;

    [SerializeField]//���G����
    float invincible_sec = 0.5f;
    [SerializeField]//�P���A���Ă���܂ł̎���
    float lost_sec = 5.0f;
    [SerializeField]//���̌��ʂ��󂯂�Ԋu
    float take_effect_interval = 0.3f;
    float time_count = 0;//�J�E���g�p

    bool on_ground = false;      //�n�ʂ̏ォ�t���O
    bool on_invincible = false;  //���G�t���O
    bool is_under_cloud = false; //�_�̉��ɂ��邩
    bool is_hit_attack = false;  //�U�����������Ă���


    private void Awake()
    {
        //�R���|�[�l���g�擾
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        //�N���X�쐬
        playerHP = new PlayerHP(max_hp);
        umbrella = new Umbrella();

        //�N���X�擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        weather = GameObject.Find("Weather").GetComponent<Weather>();
        se = GameObject.Find("Audio_SE").GetComponent<SE>();

        Text name_text = hpSlider.GetComponentInChildren<Text>();//���O�𔽉f
        if (name_text != null) name_text.text = player_name;

        ResetState();//���Z�b�g

        CreateUmbrella();//�P�̌����ڕύX
    }

    private void Update()
    {
        // �n�㔻��
        on_ground = Physics2D.BoxCast(origin: boxCollider.bounds.center,
                                       size: boxCollider.bounds.size,
                                      angle: 0f,
                                  direction: Vector2.down,
                                   distance: 0.01f,
                                  layerMask: ground_layer
                                  );
        //�ړ������X�V
        float vec_x;
        if (direction == Direction.LEFT)
            vec_x = -1;
        else
            vec_x = 1;

        //�ړ�
        if (current_move_speed > 0)
        {
            //X�x�N�g���X�V
            rb.velocity = new Vector2(vec_x * current_move_speed, rb.velocity.y);

            current_move_speed -= 0.1f;//����
        }

        if (gameManager.IsOver()) return;//�Q�[���I���Ȃ���ʂ��󂯂Ȃ�

        //����I�Ɋ����ʂ��󂯂�
        time_count += Time.deltaTime;//���Ԍv��
        if (time_count >= take_effect_interval)
        {
            time_count = 0;//���Z�b�g
            if (umbrella.GetState() != UMBRELLA_STATE.OPEN)
            {

                //�_�̉��ɂ���Ƃ���
                if (is_under_cloud)
                {
                    playerHP.HealHP(1);
                }
                //�P���J���Ă��Ȃ���΃_���[�W
                else
                {
                    playerHP.ReduceHP(2);
                }

                UpdateHPBar();
            }
        }
        if (weather == null) return;
        //�����󂯂����̏����i������ς���\������j
        if(weather.IsWind() && umbrella.GetState() == UMBRELLA_STATE.OPEN)
        {
            LostUmbrella();//�P���Ȃ���

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //�U���ɐڐG
        if (collision.gameObject.tag == "Attack")
        {
            if (TakeDamage(attack_damage))//�_���[�W���󂯂鏈��
            {
                Instantiate(hitEffect,transform.position,Quaternion.identity);//�G�t�F�N�g�\��
                se.PlayAudio(se.attack);//se
            }
            is_hit_attack = true;
        }       
        else
        {
            is_hit_attack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�J�ɓ������Ƃ�
        if (collision.gameObject.tag == "rain")
        {
            is_under_cloud = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�J����o���Ƃ�
        if (collision.gameObject.tag == "rain")
        {
            is_under_cloud = false;
        }
    }

    // ��莞�Ԍ�ɏ������Ăяo���R���[�`��
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    //X�����ړ�
    public void MoveX(Direction _dir)
    {
        SetDirection(_dir);//�����ύX

        current_move_speed = max_move_speed;//���x������
    }

    //�����؂�ւ�
    private void SetDirection(Direction _dir)
    {
        if (direction == _dir) return;//�����Ȃ�ύX���Ȃ�
        direction = _dir;//�����ύX

        if (direction == Direction.LEFT)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;


        CreateUmbrella();
    }

    //�W�����v
    public bool Jump()
    {
        if (!on_ground) return false;

        Debug.Log("�W�����v���s");
        rb.AddForce(Vector2.up * jump_pow);//������ɗ͂�������

        on_ground = false;

        se.PlayAudio(se.jump);//se

        return true;
    }

    //�P�̊J��
    public void ChangeUmbrella()
    {
        Debug.Log("�P�؂�ւ�");
        umbrella.ChangeOpen();
        CreateUmbrella();

        if(umbrella.GetState() == UMBRELLA_STATE.OPEN)//�J�����Ƃ�
        {
            se.PlayAudio(se.umbrella_open);//se
            if (is_hit_attack) //�U����������Ă�����
            {
                gameManager.GetOtherPlayer(player_name).LostUmbrella();//����̎P����
                se.PlayAudio(se.umbrella_brock);//se
            }

        }
        else//�����Ƃ�
        {
            se.PlayAudio(se.umbrella_close);//se
        }

    }
    //�P���X�g
    public void LostUmbrella()
    {
        Debug.Log("�P���X�g");
        umbrella.Lost();//�P������
        CreateUmbrella();

        //��莞�Ԍ�A���Ă���
        StartCoroutine(DelayCoroutine(lost_sec, () =>
        {
            umbrella.PickUp();
            CreateUmbrella();
        }));
    }

    //�P�I�u�W�F�N�g�쐬
    private void CreateUmbrella()
    {
        if (umbrella_obj != null)//���݂̃I�u�W�F�N�g�폜
            Destroy(umbrella_obj);

        //�I�u�W�F�N�g�쐬
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

    //��_���[�W
    public bool TakeDamage(int _damage)
    {
        if (on_invincible) return false;//���G�Ȃ�󂯂Ȃ�
        Debug.Log("��_���[�W"+_damage);

        on_invincible = true;//���G��
        //��莞�Ԍ㖳�G����
        StartCoroutine(DelayCoroutine(invincible_sec, () =>
        {
            on_invincible = false;
        }));


        playerHP.ReduceHP(_damage);//hp�����炷
        UpdateHPBar();

        return true;
    }

    //HP�o�[�X�V
    private void UpdateHPBar()
    {
        float per = playerHP.GetPercentage();//�����擾

        if (per <= 0) gameManager.OnGameOver(player_name);//�Q�[���I�[�o�[����

        hpSlider.value = per;//�Q�[�W�X�V
    }

    public string GetName()
    {
        return player_name;
    }


    //���݂̎P�̏�Ԏ擾
    public UMBRELLA_STATE GetUmbrellaState()
    {
        return umbrella.GetState();
    }


    //��ԃ��Z�b�g
    public void ResetState()
    {
        on_invincible = false;
        on_ground = false;
        playerHP.HealHP(10000);
        current_move_speed = 0.0f;
        umbrella.state = UMBRELLA_STATE.OPEN;
    }
}
