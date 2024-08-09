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

    //Œ»İ‚Ì‘Ì—Íæ“¾
    public int GetCurrentHp()
    {
        return current_hp;
    }
    //Š„‡æ“¾
    public float GetPercentage()
    {
        return (float)current_hp / (float)max_hp;
    }

    //hp‚ğŒ¸‚ç‚·
    public void ReduceHP(int _damage)
    {
        //‘Ì—ÍŒvZ
        int next_hp = current_hp - _damage;

        if (next_hp <= 0)
        {
            current_hp = 0;
            //€–Sˆ—
        }
        else
        {
            //hp‚ğŒ¸‚ç‚·
            current_hp = next_hp;
        }
        Debug.Log(current_hp);

    }

    //‰ñ•œ
    public void HealHP(int _heal)
    {
        //‘Ì—ÍŒvZ
        int next_hp = current_hp + _heal;

        if (next_hp >= max_hp)
        {
            current_hp = max_hp;//Å‘å‚Ü‚Å‰ñ•œ
        }
        else
        {
            //hp‚ğ‰ñ•œ
            current_hp = next_hp;
        }
    }
}
