using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather : MonoBehaviour
{
    [SerializeField] GameObject rain_prefab;
    [SerializeField] GameObject wind_prefab;

    GameObject rain_obj;
    GameObject wind_obj;

    [SerializeField] Transform[] rain_pos;
    [SerializeField] Transform[] wind_pos;

    float rain_timer = 0.0f;
    float wind_timer = 0.0f;

    [SerializeField] float rain_spawn_timer;
    [SerializeField] float wind_spawn_timer;
    [SerializeField] float rain_death_spawn_timer;
    [SerializeField] float wind_death_spawn_timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ƒ^ƒCƒ}[‘‰Á
        rain_timer += Time.deltaTime;
        wind_timer += Time.deltaTime;

        //‰J‰_¶¬
        if (rain_timer > rain_spawn_timer && rain_obj == null)  
        {
            //ƒ‰ƒ“ƒ_ƒ€‚ÈˆÊ’u‚É¶¬
            int i = Random.Range(0, rain_pos.Length);
            rain_obj = Instantiate(rain_prefab, rain_pos[i], true);
            rain_obj.transform.position = rain_pos[i].position;
            rain_timer = 0;
        }
        //•—¶¬
        if (wind_timer > wind_spawn_timer && wind_obj == null) 
        {
            //ƒ‰ƒ“ƒ_ƒ€‚ÈˆÊ’u‚É¶¬
            int i = Random.Range(0, wind_pos.Length);
            wind_obj = Instantiate(wind_prefab, wind_pos[i], true);
            wind_obj.transform.position = wind_pos[i].position;
            wind_timer = 0;
        }
        //‰J‰_íœ
        if (rain_timer > rain_death_spawn_timer && rain_obj != null)  
        {
            Destroy(rain_obj);
            rain_timer = 0;
        }
        //•—íœ
        if (wind_timer > wind_death_spawn_timer && wind_obj != null)
        {
            Destroy(wind_obj);
            wind_timer = 0;
        }
    }
}
