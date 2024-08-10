using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] public AudioClip wind;
    [SerializeField] public AudioClip attack;
    [SerializeField] public AudioClip cloud_spwan;
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip clear_se;

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

    public void PlayAudio(AudioClip _audio)
    {
        audioSource.PlayOneShot(_audio);
    }
}
