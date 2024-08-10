using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject rain_prefab;
    float rain_timer = 0.0f;
    public float rain_spawn_timer;
    [SerializeField] float spawn_max_x;
    [SerializeField] float spawn_min_x;
    
    private void FixedUpdate()
    {
        //�^�C�}�[����
        rain_timer += Time.deltaTime;

    }

    private void Update()
    {
        if (rain_timer >= rain_spawn_timer)
        {
            float x = Random.Range(spawn_min_x, spawn_max_x);
            Vector3 spawn = new Vector3(x, 0.0f);

            Instantiate(rain_prefab, transform.position+spawn,Quaternion.identity,gameObject.transform);//prefab�𐶐�
            // transform���擾
            rain_timer = 0.0f;
        }

    }

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
