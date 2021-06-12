using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MrWhippy : MonoBehaviour
{

    public attackIndicator squareAttack;
    public attackIndicator roundAttack;

    const double ATTACK_TIMER = 3;
    const int LINE_ATTACK_LENGTH = 5;

    const int DEFEND_ATTACK_HEIGHT = 4;
    const int DEFEND_ATTACK_WIDTH = 5;

    const int MINI_ATTACK_HEIGHT = 2;
    const int MINI_ATTACK_WIDTH = 2;

    double timer = ATTACK_TIMER;

    List<string> attackList = new List<string>();
    Queue<string> lockedAttacks = new Queue<string>();
    Queue<string> pendingAttacks = new Queue<string>();

    Vector3 lastMiniAttack = new Vector3(-99, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //this.attackList.Add("sweep");
        this.attackList.Add("line");
        this.lockedAttacks.Enqueue("defend");
        this.lockedAttacks.Enqueue("triple");
        this.lockedAttacks.Enqueue("sweep");

        //Put this line when battle advances
        this.attackList.Add(this.lockedAttacks.Dequeue());
        this.attackList.Add(this.lockedAttacks.Dequeue());
        this.attackList.Add(this.lockedAttacks.Dequeue());

        this.EnqueueAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timer <= 0)
        {
            this.InitiateAttack_and_ResetTimer_and_QueueNewAttack();

            if (pendingAttacks.Count == 0)
                EnqueueAttack();
        }
        else {
            this.timer -= Time.deltaTime;
        }

    }

    void EnqueueAttack()
    {
        int randAttack = Random.Range(0, this.attackList.Count);
        string nextAttack = this.attackList[randAttack];
        this.pendingAttacks.Enqueue(nextAttack);
    }


    void InitiateAttack_and_ResetTimer_and_QueueNewAttack()
    {
        string chosenAttack = pendingAttacks.Dequeue();

        if (chosenAttack == "line")
        {
            this.initiateLineAttack();
            this.timer = ATTACK_TIMER;
        }
        if (chosenAttack == "defend")
        {
            this.initiateDefendAttack();
            this.timer = ATTACK_TIMER;
        }

        if (chosenAttack == "triple")
        {
            this.pendingAttacks.Enqueue("mini");
            this.pendingAttacks.Enqueue("mini");
            this.initiateMiniAttack();
            this.timer = 1;

        }

        if (chosenAttack == "mini")
        {
            this.initiateMiniAttack();
            if (this.pendingAttacks.Count == 0)
                this.timer = ATTACK_TIMER;
            else
            {
                this.timer = 1;
            }

        }

        if (chosenAttack == "sweep")
        {
            this.initiateSweepAttack();
            this.timer = ATTACK_TIMER;
        }
    }
    
    void initiateLineAttack() {

        //Replace this with code to get two dudes
        Vector3 point1 = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
        Vector3 point2 = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);

        Vector3 midpoint = point2 - point1;

        Vector3 attackPoint = midpoint;

        attackIndicator attackInstance = Instantiate(squareAttack);

        attackInstance.transform.position = attackPoint;
        attackInstance.GetComponent<attackIndicator>().DefineAttack(2, 0.3, 0.2, 1, true, false, true);


        int vertOrHor = Random.Range(0, 2);
        if (vertOrHor == 0)
            //do vert
            attackInstance.transform.localScale = new Vector3(1, LINE_ATTACK_LENGTH, 0);
        else
            //do hor
            attackInstance.transform.localScale = new Vector3(LINE_ATTACK_LENGTH, 1, 0);

    }

    void initiateDefendAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.GetComponent<attackIndicator>().DefineAttack(3, 0.6, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(DEFEND_ATTACK_WIDTH, DEFEND_ATTACK_HEIGHT, 0);
        attackInstance.transform.position = this.transform.position;

    }

    void initiateTripleAttack()
    {
        //replace this with player movement predicting logic
        Vector3 attackPoint = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);

        Vector3 attack1 = attackPoint += new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);
        Vector3 attack2 = attackPoint += new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);
        Vector3 attack3 = attackPoint += new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);

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
            

        }


    }
    void initiateMiniAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.GetComponent<attackIndicator>().DefineAttack(2, 0.2, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(MINI_ATTACK_WIDTH, MINI_ATTACK_HEIGHT, 1);

        Vector3 randomPos;
        if (lastMiniAttack == new Vector3(-99, 0, 0))
            randomPos = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
        else
        {
            randomPos = lastMiniAttack + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0);
        }
        attackInstance.transform.position = randomPos;

        if (this.pendingAttacks.Count == 0)
            lastMiniAttack = new Vector3(-99, 0, 0);
        else
            lastMiniAttack = randomPos;
    }

    void initiateSweepAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.GetComponent<attackIndicator>().DefineAttack(3, 0.6, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(DEFEND_ATTACK_WIDTH, DEFEND_ATTACK_HEIGHT, 0);
        attackInstance.transform.position = this.transform.position;

    }

}

