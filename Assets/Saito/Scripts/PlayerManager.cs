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
    int current_hp; //���݂̗̑�

    bool on_ground = false;     //�n�ʂ̏ォ�t���O
    bool on_invincible = false;  //���G�t���O

    private void Awake()
    {
        //�R���|�[�l���g�擾
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        ResetState();//���Z�b�g
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
    }

    //X�����ړ�
    public void MoveX(Direction _dir)
    {
        direction = _dir;//�����ύX

        current_move_speed = max_move_speed;//���x������
    }

    //�W�����v
    public bool Jump()
    {
        if (!on_ground) return false;

        Debug.Log("�W�����v���s");
        rb.AddForce(Vector2.up * jump_pow);//������ɗ͂�������

        on_ground = false;

        return true;
    }

    //�P�̊J��
    public void ChangeUmbrella()
    {
        Debug.Log("�P�؂�ւ�");
    }

    //��_���[�W
    public void Damage(int _damage)
    {
        Debug.Log("��_���[�W");

        //�̗͌v�Z
        int next_hp = current_hp - _damage;

        if(next_hp<=0)
        {
            current_hp = 0;
            //���S����
        }
        else
        {
            //hp�����炷
            current_hp = next_hp;
        }

        //�Q�[�W�X�V
    }

    //���݂̎P�̏�Ԏ擾
    public void GetUmbrellaState()
    {

    }

    //��ԃ��Z�b�g
    public void ResetState()
    {
        on_invincible = false;
        on_ground = false;
        current_hp = max_hp;
        current_move_speed = 0.0f;
    }
}
