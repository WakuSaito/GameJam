using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weather : MonoBehaviour
{
    SE se;

    //�����������v���C�n�u�I�u�W�F�N�g
    [SerializeField] GameObject rain_prefab;
    [SerializeField] GameObject wind_prefab;
    [SerializeField] GameObject attention_effect;
    GameObject canvas;

    //���������N���[���I�u�W�F�N�g
    GameObject rain_obj;
    GameObject wind_obj;

    //�����������ʒu�i�����j
    [SerializeField] Transform[] rain_pos;
    [SerializeField] Transform[] wind_pos;

    //�e����܂ł̃^�C�}�[
    float rain_timer = 0.0f;
    float wind_timer = 0.0f;

    //�X�|�[���Ԋu
    [SerializeField] float rain_spawn_timer;
    [SerializeField] float wind_spawn_timer;
    //�f�X�|�[���Ԋu
    [SerializeField] float rain_death_spawn_timer;
    [SerializeField] float wind_death_spawn_timer;

    const float ATTENTION_TIME = 0.75f; 

    bool is_wind;//���������Ă��邩

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");//�L�����o�X�擾
        se = GameObject.Find("Audio_SE").GetComponent<SE>();

    }

    // Update is called once per frame
    void Update()
    {
        //�^�C�}�[����
        rain_timer += Time.deltaTime;
        wind_timer += Time.deltaTime;

        //�J�_����
        if (rain_timer > rain_spawn_timer && rain_obj == null)  
        {
            //�����_���Ȉʒu�ɐ���
            int i = UnityEngine.Random.Range(0, rain_pos.Length);
            rain_obj = Instantiate(rain_prefab, rain_pos[i], true);
            rain_obj.transform.position = rain_pos[i].position;
            rain_timer = 0;
            se.PlayAudio(se.cloud_spwan);//se
        }
        //�J�_�폜
        if (rain_timer > rain_death_spawn_timer && rain_obj != null)  
        {
            Destroy(rain_obj);
            rain_timer = 0;
        }
        

        //������
        if (wind_timer > (wind_spawn_timer- ATTENTION_TIME) && wind_obj == null) 
        {
            Debug.Log("Attention");

            wind_timer = 0;//�^�C�}�[���Z�b�g

            if (canvas != null && attention_effect != null)
            {
                //�L�����o�X�����ɒ��ӂ��o��
                Instantiate(attention_effect, canvas.transform);
            }
            //���ӂ�������������ŕ�����
            StartCoroutine(DelayCoroutine(ATTENTION_TIME, () =>
            {
                //�����_���Ȉʒu�ɐ���
                int i = UnityEngine.Random.Range(0, wind_pos.Length);
                wind_obj = Instantiate(wind_prefab, wind_pos[i], true);
                wind_obj.transform.position = wind_pos[i].position;
                is_wind = true;
                se.PlayAudio(se.wind);//se

            }));
        }
        //���폜
        if (wind_timer > wind_death_spawn_timer && wind_obj != null)
        {
            Destroy(wind_obj);
            wind_timer = 0;
            is_wind = false;
        }
    }

    public bool IsWind()
    {
        return is_wind;
    }

    // ��莞�Ԍ�ɏ������Ăяo���R���[�`��
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

}
