using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public float MUSIC_VOLUME = 0.05f;
    public float SOUND_VOLUME = 1f;
    
    public AudioClip backgroundMusic;
    
    public AudioClip explosionSound;
    public AudioClip demonPainSound;
    public AudioClip blockSound;
    public AudioClip bigAngrySound;
    public AudioClip shatterSound;

    public AudioClip[] demonIdleArray;

    public AudioClip[] manPainArray;

    public AudioClip[] manDeathArray;

    public static AudioSource soundSrc;
    public static AudioSource musicSrc;

    // Start is called before the first frame update
    void Start()
    {
        soundSrc = GetComponent<AudioSource>();
        musicSrc = GetComponent<AudioSource>();

        soundSrc.volume = SOUND_VOLUME;
        musicSrc.volume = MUSIC_VOLUME;

        //this.playMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void playSound(string soundKey)
    {

        if (soundKey.Equals("explosion"))
            soundSrc.PlayOneShot(explosionSound);
        else if (soundKey.Equals("demonPain"))
            soundSrc.PlayOneShot(demonPainSound);
        else if (soundKey.Equals("hitBlocked"))
            soundSrc.PlayOneShot(blockSound);
        else if (soundKey.Equals("phaseChange"))
            soundSrc.PlayOneShot(bigAngrySound);
        else if (soundKey.Equals("demonIdle"))
        {
            int soundNo = Random.Range(0, demonIdleArray.Length);
            AudioClip soundChoice = demonIdleArray[soundNo];
            soundSrc.PlayOneShot(soundChoice);
        }
        else if (soundKey.Equals("shatter"))
            soundSrc.PlayOneShot(shatterSound);

        else if (soundKey.Equals("manDeath"))
        {
            int soundNo = Random.Range(0, manDeathArray.Length);
            AudioClip soundChoice = manDeathArray[soundNo];
            soundSrc.PlayOneShot(soundChoice);

        }
    }

    public void playManPain()
    {
        int soundKey = Random.Range(0, manPainArray.Length);
        AudioClip soundChoice = manPainArray[soundKey];
        soundSrc.PlayOneShot(soundChoice);
    }

    public void playMusic()
    {
        musicSrc.PlayOneShot(backgroundMusic);
    }
}
