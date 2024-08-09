using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP
{
    int max_hp;
    int current_hp;

    public PlayerHP(int _max_hp)
    {
        max_hp = _max_hp;
        current_hp = max_hp;
    }

    //���݂̗͎̑擾
    public int GetCurrentHp()
    {
        return current_hp;
    }
    //�����擾
    public float GetPercentage()
    {
        return (float)current_hp / (float)max_hp;
    }

    //hp�����炷
    public void ReduceHP(int _damage)
    {
        //�̗͌v�Z
        int next_hp = current_hp - _damage;

        if (next_hp <= 0)
        {
            current_hp = 0;
            //���S����
        }
        else
        {
            //hp�����炷
            current_hp = next_hp;
        }
        Debug.Log(current_hp);

    }

    //��
    public void HealHP(int _heal)
    {
        //�̗͌v�Z
        int next_hp = current_hp + _heal;

        if (next_hp >= max_hp)
        {
            current_hp = max_hp;//�ő�܂ŉ�
        }
        else
        {
            //hp����
            current_hp = next_hp;
        }
    }
}
