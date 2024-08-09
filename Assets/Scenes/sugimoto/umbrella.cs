using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class umbrella : MonoBehaviour
{
    //状態の最大数
    public const int MAX_STATE = 3;
    //状態フラグ
    public bool[] state_flag;

    public enum STATE
    {
        CLOSE,
        OPEN,
        NOHAVING,
    }

    // Start is called before the first frame update
    void Start()
    {
        state_flag[(int)STATE.OPEN] = false;
        state_flag[(int)STATE.CLOSE] = true;
        state_flag[(int)STATE.NOHAVING] = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void state()
    {
        
    }

}
