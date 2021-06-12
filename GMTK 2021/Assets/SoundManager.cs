using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip explosionSound;
    public static AudioSource audioSrc;

    Dictionary<string, AudioClip> soundDict;

    // Start is called before the first frame update
    void Start()
    {
        //explosionSound = Resources.Load<AudioClip>("explosionSound");
        audioSrc = GetComponent<AudioSource>();
            //soundDict.Add("explosion", explosionSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        //AudioClip sound = soundDict[soundKey];
        
        audioSrc.PlayOneShot(explosionSound);
    }
}
