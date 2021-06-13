using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSoundMuting : MonoBehaviour
{
    public jankysettings JankSettings;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        JankSettings = FindObjectOfType<jankysettings>();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        audio.mute = !JankSettings.sound;
    }
}
