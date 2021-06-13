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

    void Start()
    {
        SpriteR = GetComponent<SpriteRenderer>();
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

        //create a shard
        float ang = Random.Range(-Mathf.PI, 0);
        Vector2 changePos = new Vector2(rad*Mathf.Cos(ang) + transform.position.x, rad*Mathf.Sin(ang)/2 + transform.position.y);
        GameObject newDagger = Instantiate(Dagger, changePos, Quaternion.identity);
        //newDagger.transform.SetPositionAndRotation(changePos, Quaternion.identity);

        // disable further Damage
        Destroy(GetComponent<Health>());
    }
    // Update is called once per frame
}
