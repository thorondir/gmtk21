using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health

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

    // Start is called before the first frame update
    void Awake()
    {
        timer = lowTime;
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void DefineAttack(double low, double high, double end, int dmg, bool toPlayer, bool toBoss, bool toPillar)
    {
        lowTime = low;
        highTime = high;
        endTime = end;
        damage = dmg;
        hurtsBoss = toBoss;
        hurtsPillar = toPillar;
        hurtsPlayer = toPlayer;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null)
        {
            bool isHit = false;
            switch(other.GetComponent<Health>().myType)
            {
                case Type.player:
                    if (hurtsPlayer) isHit = true;
                    break;
                case Type.boss:
                    if (hurtsBoss) isHit = true;
                    break;
                case Type.pillar:
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
                Debug.Log("switching to dmg");
                isDamaging = true;
                col.enabled = true;
                timer = highTime;
            }
        } else
        {
            if (timer <= 0.0)
            {
                Debug.Log("Destroying self");
                col.enabled = false;
                Destroy(gameObject,endTime);
            }
        }
    }
}
