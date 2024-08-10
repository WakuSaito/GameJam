using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip wind;
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip cloud_spwan;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip clear_se;

    // Start is called before the first frame update
    void Start()
    {
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAudio(AudioClip _audio)
    {
        audioSource.PlayOneShot(_audio);
    }
}
