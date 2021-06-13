using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAnim : MonoBehaviour
{
    public ParticleSystem boom;
    public ParticleSystem passive;
    public bool spinnen = false;
    public double timeUntilDelete = 0;
    double spinnenTime = 360 / (720 * 2.5f);
    SpriteRenderer r;

    public void Activate()
    {
        r = GetComponent<SpriteRenderer>();
        boom.Play();
        spinnen = true;
    }

    void Update()
    {
        if(r==null)
            r = GetComponent<SpriteRenderer>();
        if (timeUntilDelete > 0)
        {
            Color col = r.color;
            col.a -= 1f * Time.deltaTime;
            r.color = col;
            timeUntilDelete -= Time.deltaTime;

            return;
        }
        if (spinnen)
        {
            spinnenTime -= Time.deltaTime;
            if (spinnenTime <= 0)
            {
                spinnen = false;
                timeUntilDelete = 1;
                Destroy(gameObject, 1f);
                passive.enableEmission = false;
            }
            else
            {
                transform.Rotate(0,0,720*2.5f*Time.deltaTime);
            }
        }
    }

}
