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

    public void DefineAttack(double low, double high, int dmg, bool toPlayer, bool toBoss, bool toPillar)
    {
        lowTime = low;
        highTime = high;
        damage = dmg;
        hurtsBoss = toBoss;
        hurtsPillar = toPillar;
        hurtsPlayer = toPlayer;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<link_movement>() != null)
        {

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
                Destroy(gameObject);
            }
        }
    }
}
