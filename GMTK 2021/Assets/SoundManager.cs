using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip explosionSound;
    public static AudioSource audioSrc;

    Dictionary<int, string> soundDict = new Dictionary<int, string>();

    // Start is called before the first frame update
    void Start()
    {
        //explosionSound = Resources.Load<AudioClip>("explosionSound");
        audioSrc = GetComponent<AudioSource>();
        //this.soundDict.Add(1, "explosion");

        //Debug.LogWarning(this.soundDict[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void playSound(int soundKey)
    {
        string sound = this.soundDict[soundKey];

        Debug.LogWarning(sound);
        //audioSrc.PlayOneShot(sound);
    }
    */

    public void playExplosion()
    {
        audioSrc.PlayOneShot(explosionSound);
    }
}
