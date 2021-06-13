using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackIndicator : MonoBehaviour
{
    public bool isDamaging = false;
    bool hurtsPlayer = true;
    bool hurtsBoss = true;
    bool hurtsPillar = true;
    double lowTime = 1.0;
    double highTime = 1.0;
    double endTime = 0.2;
    int damage = 1;
    double timer;
    Collider2D col;
    SpriteRenderer mySprite;

    public GameObject effect;

    GameObject currentEffect;

    public GameObject SndManager;

    // Start is called before the first frame update
    void Awake()
    {
        timer = lowTime;
        col = GetComponent<Collider2D>();
        col.enabled = false;
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.color = new Color(220, 75, 220, 200);

    }

    public void DefineAttack(double low, double high, double end, int dmg, bool toPlayer, bool toBoss, bool toPillar)
    {
        lowTime = low;
        timer = low;
        highTime = high;
        endTime = end;
        damage = dmg;
        hurtsBoss = toBoss;
        hurtsPillar = toPillar;
        hurtsPlayer = toPlayer;

        if (effect != null)
            currentEffect = Instantiate(effect, transform.position, Quaternion.Euler(Vector3.zero));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null)
        {
            bool isHit = false;
            switch(other.GetComponent<Health>().myType)
            {
                case 0: // is a player
                    if (hurtsPlayer) isHit = true;
                    break;
                case 1: // is a boss
                    if (hurtsBoss) isHit = true;
                    break;
                case 2: // is a pillar
                    if (hurtsPillar) isHit = true;
                    break;
            }
            if (isHit)
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log(timer.ToString());
        if (!isDamaging)
        {
            if (timer <= 0.0)
            {
                // Debug.Log("switching to dmg");
                isDamaging = true;
                col.enabled = true;
                timer = highTime;
                mySprite.color = Color.white;
                currentEffect?.GetComponent<SpinAnim>()?.Activate();
                currentEffect?.GetComponent<SlowAnim>()?.Activate();


                foreach (GameObject t in GameObject.FindGameObjectsWithTag("miniboom"))
                {
                    t.GetComponent<Animator>()?.SetTrigger("Boom");
                }

                SndManager.GetComponent<SoundManager>().playSound("explosion");

            }
        } else
        {
            if (timer <= 0.0)
            {
                // Debug.Log("Destroying self");
                col.enabled = false;
                Destroy(gameObject,(float)endTime);
            }
        }
    }
}
