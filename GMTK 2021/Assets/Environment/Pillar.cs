using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField]
    Sprite brokenPillar;
    public GameObject Boss;
    SpriteRenderer SpriteR;

    public GameObject soundManager;
    [SerializeField]
    GameObject Dagger;
    [SerializeField]
    float rad;

    ScreenShake shaker;
    [SerializeField]
    GameObject particles;

    void Start()
    {
        SpriteR = GetComponent<SpriteRenderer>();
        shaker = Camera.main.GetComponent<ScreenShake>();
    }
    // Start is called before the first frame update
    void GotHit()
    {
        //Debug.Log("pillar got hit");
        //alert boss that pillar is destroyed
        Boss.GetComponent<MrWhippy>().advanceBattle();

        // destroy this pillar
        SpriteR.sprite = brokenPillar;
        soundManager.GetComponent<SoundManager>().playSound("shatter");
        // To make screenshake, paramaters are intensity and duration
        StartCoroutine(shaker.Shake(0.1f, 0.3f));
        GameObject myParticles = Instantiate(particles, transform.position + new Vector3(0f, .7f, 0f), Quaternion.Euler(new Vector3(-60,0 ,0)));
        Destroy(myParticles, 5.0f);

        //create a shard
        float ang = Random.Range(-Mathf.PI, 0);
        Vector2 changePos = new Vector2(rad*Mathf.Cos(ang) + transform.position.x+.7f, rad*Mathf.Sin(ang)/2 + transform.position.y);
        GameObject newDagger = Instantiate(Dagger, changePos, Quaternion.identity);
        //newDagger.transform.SetPositionAndRotation(changePos, Quaternion.identity);

        // disable further Damage
        Destroy(GetComponent<Health>());
    }
    // Update is called once per frame
}
