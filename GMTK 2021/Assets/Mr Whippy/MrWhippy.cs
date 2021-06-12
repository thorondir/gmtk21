using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MrWhippy : MonoBehaviour
{

    public GameObject squareAttack;
    public GameObject roundAttack;

    const double ATTACK_TIMER = 3;
    const int LINE_ATTACK_LENGTH = 5;
    
    double timer = ATTACK_TIMER;

    List<string> attackList = ["line"];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.timer <= 0)
        {
            this.initiateAttack();

            this.timer = ATTACK_TIMER;
        }
        else {
            this.timer -= Time.deltaTime;
        }

    }

    void initiateAttack()
    {
        int randAttack = Random.Range(0, this.attackList.Count);

        string chosenAttack = this.attackList[randAttack];

        if (chosenAttack == "line")
            this.initiateLineAttack();
    }
    
    void initiateLineAttack() {

        //Replace this with code to get two dudes
        Vector3 point1 = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);
        Vector3 point2 = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);

        Vector3 midpoint = point2 - point1;

        Vector3 attackPoint = midpoint + new Vector3(LINE_ATTACK_LENGTH / 2, 0, 0);

        GameObject attackInstance = Instantiate(squareAttack);

        attackInstance.transform.position = attackPoint;

        int vertOrHor = Random.Range(0, 2);
        if (vertOrHor == 0)
            //do vert
            attackInstance.transform.localScale = new Vector3(1, LINE_ATTACK_LENGTH, 1);
        else
            //do hor
            attackInstance.transform.localScale = new Vector3(LINE_ATTACK_LENGTH, 1, 1);
    }

}

