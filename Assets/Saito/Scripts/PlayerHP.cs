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

    //現在の体力取得
    public int GetCurrentHp()
    {
        return current_hp;
    }
    //割合取得
    public float GetPercentage()
    {
        return (float)current_hp / (float)max_hp;
    }

    //hpを減らす
    public void ReduceHP(int _damage)
    {
        //体力計算
        int next_hp = current_hp - _damage;

        if (next_hp <= 0)
        {
            current_hp = 0;
            //死亡処理
        }
        else
        {
            //hpを減らす
            current_hp = next_hp;
        }
        Debug.Log(current_hp);

    }

    //回復
    public void HealHP(int _heal)
    {
        //体力計算
        int next_hp = current_hp + _heal;

        if (next_hp >= max_hp)
        {
            current_hp = max_hp;//最大まで回復
        }
        else
        {
            //hpを回復
            current_hp = next_hp;
        }
    }
}
