using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += 0.00f;    // x���W��0.00���Z
        pos.y -= 0.01f;    // y���W��-0.01���Z
        pos.z += 0.00f;    // z���W��0.00���Z

        transform.position = pos;  // ���W��ݒ�

        //�J����ԉ��ɍs�������̏���
        if (-8.5f > transform.position.y)
        //�J��delete_rain�^�O�̂����I�u�W�F�N�g�ɐڐG������
        {
            Destroy(gameObject);//����
        }

    }
}
