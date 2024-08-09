using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public GameObject rain_prefab;
    public GameObject rainObj;
    public GameObject delete_line;
    float rain_timer = 0.0f;
    public float rain_spawn_timer;
    Transform myTransform;

    bool test = false;
    private void Update()
    {
        //�^�C�}�[����
        rain_timer += Time.deltaTime;
        if (rain_timer >= rain_spawn_timer && rainObj==null)
        {
            rainObj = Instantiate(rain_prefab);//prefab�𐶐�
            rainObj.transform.position = transform.position;
            // transform���擾
            //test = true;
            rain_timer = 0.0f;
        }



        if (rainObj!=null)
        {
            // ���W���擾
            myTransform = rainObj.transform;

            Vector3 pos = myTransform.position;
            pos.x += 0.00f;    // x���W��0.00���Z
            pos.y -= 0.01f;    // y���W��-0.01���Z
            pos.z += 0.00f;    // z���W��0.00���Z

            myTransform.position = pos;  // ���W��ݒ�

            //�J����ԉ��ɍs�������̏���
            if (delete_line.transform.position.y > rainObj.transform.position.y)
            //�J��delete_rain�^�O�̂����I�u�W�F�N�g�ɐڐG������
            {
                Destroy(rainObj.gameObject);//����
                rainObj = null;
            }
        }

    }



    //void OnTriggerEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "player")//�v���C���[���J�ɓ���������
    //    {
    //        Debug.Log("�J�ɓ������Ă��܂�");
    //        //�Q�[�W����
    //    }


    //}


    //�J�ƃv���C���[�������������̏���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")//�v���C���[���J�ɓ���������
        {
            Debug.Log("�J�ɓ������Ă��܂�");
            //�Q�[�W����
        }
      
    }

}
