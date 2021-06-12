using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackIndicator : MonoBehaviour
{
    public bool isDamaging = false;
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

    void DefineAttack(double low, double high, int dmg)
    {
        lowTime = low;
        highTime = high;
        damage = dmg;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

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
