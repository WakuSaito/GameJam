using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gage : MonoBehaviour
{
    umbrella umbrella;

    //gage�I�u�W�F�N�g
    [SerializeField] GameObject gage_obj;
    //�P���擾�p
    [SerializeField] GameObject umbrella_obj;
    //�ő�̗�
    [SerializeField] int max_hp;
    //�P�_���[�W������̃Q�[�W�����炷��
    float reduce_hp;
    //�̗͌��炷�Ԋu
    [SerializeField] int damege_time;

    float damage_speed;
    bool damage_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        //�P���
        umbrella = umbrella_obj.GetComponent<umbrella>();
        //�P�_���[�W������̃Q�[�W�����炷��
        reduce_hp = gage_obj.GetComponent<RectTransform>().sizeDelta.x / max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (umbrella.state_flag == UMBRELLA_STATE.OPEN && !damage_flag)
        {
            Damege();
        }

        if (damage_flag)
        {
            Debug.Log(damage_speed);
            damage_speed += Time.deltaTime;
            if (damage_speed > damege_time)
            {
                damage_flag = false;
                damage_speed = 0;
            }
        }
    }

    public void Damege()
    {
        if (gage_obj.GetComponent<RectTransform>().sizeDelta.x > 0)
        {
            // �̗̓Q�[�W�̕��ƍ�����Vector2�Ŏ��o��(Width,Height)
            Vector2 nowsafes = gage_obj.GetComponent<RectTransform>().sizeDelta;
            // �̗̓Q�[�W�̕�����_���[�W���̕�������
            nowsafes.x -= reduce_hp;
            // �̗̓Q�[�W�Ɍv�Z�ς݂�Vector2��ݒ肷��
            gage_obj.GetComponent<RectTransform>().sizeDelta = nowsafes;
            damage_flag = true;
        }
    }

}
