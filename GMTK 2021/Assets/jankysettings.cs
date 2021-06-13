using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jankysettings : MonoBehaviour
{
    public bool sound;
    public bool music;

    // Start is called before the first frame update
    void Start()
    {
        sound = true;
        music = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
