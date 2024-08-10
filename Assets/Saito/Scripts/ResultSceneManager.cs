using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class ResultSceneManager : MonoBehaviour
{
    SE se;

    [SerializeField]//���O�̃e�L�X�g
    Text name_text;

    bool can_change = false;//�V�[���؂�ւ��\

    private void Awake()
    {
        se = GameObject.Find("Audio_SE").GetComponent<SE>();
    }

    // Start is called before the first frame update
    void Start()
    {
        name_text.text = StaticData.winner_name;
        can_change = false;
        se.PlayAudio(se.clear_se);//se

        StartCoroutine(DelayCoroutine(2.0f, () =>
        {
            //��莞�Ԍ�V�[���؂�ւ��\��
            can_change = true;
        }));
    }

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�{�^���������ꂽ��
        if (Input.anyKeyDown)
        {
            //�؂�ւ��\
            if (can_change)
                // TitleScene�ɐ؂�ւ�
                SceneManager.LoadScene("kuriya");
        }
    }

    // ��莞�Ԍ�ɏ������Ăяo���R���[�`��
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }


}
