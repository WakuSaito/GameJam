using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UMBRELLA_STATE
{
    CLOSE,
    OPEN,
    NOHAVING,
}

public class umbrella : MonoBehaviour
{
    //状態の最大数
    public const int MAX_STATE = 3;
    //状態フラグ
    public UMBRELLA_STATE state_flag;


    // Start is called before the first frame update
    void Start()
    {
        state_flag = UMBRELLA_STATE.OPEN;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void state()
    {
        
    }

}
