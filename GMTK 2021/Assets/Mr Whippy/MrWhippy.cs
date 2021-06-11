using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrWhippy : MonoBehaviour
{

    [SerializeField]
    GameObject attack;

    const double ATTACK_TIMER = 300;
    double timer = ATTACK_TIMER;

    int isAttacking = 0;


    // Start is called before the first frame update
    void Start()
    {

        //whip.transform.SetPosition(Vector2.zero);

    }

    // Update is called once per frame
    void Update()
    {
        if (this.timer <= 0)
        {

            this.initiateAttack();

            this.timer = ATTACK_TIMER;

        }
        else
        {
            this.timer -= Time.deltaTime;
        }

        if (this.isAttacking > 0)
        {
            this.updateAttack();
        }
    }

    void initiateAttack()
    {
        this.isAttacking = 40;
        GameObject attackInstance = Instantiate(attack);
        attackInstance.transform.localScale += new Vector3(1, 0, 0);
    }

    void updateAttack()
    {
        
        this.isAttacking -= 1;

        if (this.isAttacking == 0)
        {
            this.endAttack();
        }
    }

    void endAttack()
    {
        return;
    }
}

