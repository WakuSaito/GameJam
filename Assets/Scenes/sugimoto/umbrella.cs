using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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
