using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaMove : MonoBehaviour
{
    [SerializeField]
    Vector2 base_pos;

    public void SetPos(Vector2 _ppos)
    {
        transform.position = _ppos + base_pos;
    }
}
