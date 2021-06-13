using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioClip explosionSound;
    public AudioClip demonPainSound;
    public AudioClip demonIdleSound;

    public AudioClip[] manPainArray;

    public static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void playSound(string soundKey)
    {

        if (soundKey.Equals("explosion"))
            audioSrc.PlayOneShot(explosionSound);
        else if (soundKey.Equals("demonPain"))
            audioSrc.PlayOneShot(demonPainSound);
        else if (soundKey.Equals("demonIdle"))
            audioSrc.PlayOneShot(demonIdleSound);

    }
    

    public void playExplosion()
    {
        audioSrc.PlayOneShot(explosionSound);
    }

    public void playDemonPain()
    {
        audioSrc.PlayOneShot(demonPainSound);
    }

    public void playManPain()
    {
        Debug.LogWarning(manPainArray.Length);
        int soundKey = Random.Range(0, manPainArray.Length);
        AudioClip soundChoice = manPainArray[soundKey];
        audioSrc.PlayOneShot(soundChoice);
    }
}