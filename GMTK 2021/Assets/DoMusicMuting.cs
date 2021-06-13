using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoMusicMuting : MonoBehaviour
{
    public jankysettings JankSettings;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        JankSettings = FindObjectOfType<jankysettings>();
        
    }

    // Update is called once per frame
    void Update()
    {
        audio.mute = !JankSettings.music;
    }
}
