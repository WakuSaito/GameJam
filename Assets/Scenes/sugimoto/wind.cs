using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    [SerializeField] Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
        {
            vec.x = -vec.x;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(vec.x * Time.deltaTime, -vec.y * Time.deltaTime);
    }
}
