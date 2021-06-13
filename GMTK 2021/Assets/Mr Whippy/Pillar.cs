using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField]
    Sprite brokenPillar;
    public GameObject Boss;
    SpriteRenderer SpriteR;

    [SerializeField]
    GameObject Dagger;

    void Start()
    {
        SpriteR = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void GotHit()
    {
        Debug.Log("pillar got hit");
        Boss.GetComponent<MrWhippy>().advanceBattle();
        SpriteR.sprite = brokenPillar;
        Destroy(GetComponent<Health>());
    }
    // Update is called once per frame
}
