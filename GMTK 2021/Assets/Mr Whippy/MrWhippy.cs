using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MrWhippy : MonoBehaviour
{

    public attackIndicator squareAttack;
    public attackIndicator roundAttack;

    const double ATTACK_TIMER = 3;
    const int LINE_ATTACK_LENGTH = 5;

    const int SWEEP_ATTACK_HEIGHT = 3;
    const int SWEEP_ATTACK_WIDTH = 4;

    const int MINI_ATTACK_HEIGHT = 2;
    const int MINI_ATTACK_WIDTH = 2;

    double timer = ATTACK_TIMER;

    List<string> attackList = new List<string>();
    Queue<string> lockedAttacks = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        //this.attackList.Add("sweep");
        this.attackList.Add("line");
        this.lockedAttacks.Enqueue("sweep");

        //Put this line when battle advances
        this.attackList.Add(this.lockedAttacks.Dequeue());
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
        if (chosenAttack == "sweep")
            this.initiateSweepAttack();
    }
    
    void initiateLineAttack() {

        //Replace this with code to get two dudes
        Vector3 point1 = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);
        Vector3 point2 = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);

        Vector3 midpoint = point2 - point1;

        Vector3 attackPoint = midpoint;

        attackIndicator attackInstance = Instantiate(squareAttack);

        attackInstance.transform.position = attackPoint;
        attackInstance.GetComponent<attackIndicator>().DefineAttack(2, 1, 1);


        int vertOrHor = Random.Range(0, 2);
        if (vertOrHor == 0)
            //do vert
            attackInstance.transform.localScale = new Vector3(1, LINE_ATTACK_LENGTH, 0);
        else
            //do hor
            attackInstance.transform.localScale = new Vector3(LINE_ATTACK_LENGTH, 1, 0);

    }

    void initiateSweepAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.GetComponent<attackIndicator>().DefineAttack(3, 2, 1);
        attackInstance.transform.localScale = new Vector3(SWEEP_ATTACK_WIDTH, SWEEP_ATTACK_HEIGHT, 0);
        attackInstance.transform.position = this.transform.position;

    }

    void initiateTripleAttack()
    {
        //replace this with player movement predicting logic
        Vector3 attackCentrePoint = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);

        Vector3 attack1 = attackCentrePoint += new Vector3(Random.Range(-3, 4), Random.Range(-3, 4), 0);
        Vector3 attack2 = attackCentrePoint += new Vector3(Random.Range(-3, 4), Random.Range(-3, 4), 0);
        Vector3 attack3 = attackCentrePoint += new Vector3(Random.Range(-3, 4), Random.Range(-3, 4), 0);

        List<Vector3> attackList = new List<Vector3>();
        attackList.Add(attack1);
        attackList.Add(attack2);
        attackList.Add(attack3);

        // Prevent overlapping attacks
        /*
        foreach (Vector3 attack in attackList)
        {
            foreach (Vector3 other in attackList)
                if (attack == other)
                    attack
        }
        */

        foreach (Vector3 attack in attackList)
        {
            int timer = 2;
            
            attackIndicator attackInstance = Instantiate(roundAttack);

            attackInstance.GetComponent<attackIndicator>().DefineAttack(2, 2, 1);
            attackInstance.transform.localScale = new Vector3(SWEEP_ATTACK_WIDTH, SWEEP_ATTACK_HEIGHT, 1);
            attackInstance.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0);
        }


    }



}

