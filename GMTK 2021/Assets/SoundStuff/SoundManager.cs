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

    public Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();

    bool SOUNDS_LOADED = false;

    // Start is called before the first frame update
    void Start()
    {

        audioSrc = GetComponent<AudioSource>();

        Debug.LogWarning(soundDict.Keys);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void playSound(string soundKey)
    {
        if (!SOUNDS_LOADED)
        {
            soundDict.Add("explosion", explosionSound);
            soundDict.Add("demonIdle", demonIdleSound);
            soundDict.Add("demonPain", demonPainSound);
            SOUNDS_LOADED = true;
        }


        Debug.LogWarning(soundKey);
        AudioClip sound = soundDict[soundKey];
        
        audioSrc.PlayOneShot(sound);
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