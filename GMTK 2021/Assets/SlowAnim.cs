using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAnim : MonoBehaviour
{
    public ParticleSystem boom;
    public ParticleSystem schwoop;
    public ParticleSystem passive;
    SpriteRenderer r;

    public double timeUntilDelete=0;
    // Start is called before the first frame update
    public void Activate()
    {
        boom.Play();
        schwoop.Play();
        timeUntilDelete = 1;
    }

    void Update()
    {
        if (r == null)
            r = GetComponent<SpriteRenderer>();
        if (timeUntilDelete > 0)
        {
            Color col = r.color;
            col.a -= 1f * Time.deltaTime;
            r.color = col;
            timeUntilDelete -= Time.deltaTime;

            return;
        }
        if(timeUntilDelete < 0)
        {
            Destroy(gameObject);
        }
    }
}
