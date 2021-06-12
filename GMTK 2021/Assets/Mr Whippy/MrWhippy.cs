using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MrWhippy : MonoBehaviour
{
    public chainmanager chainmanager;
    
    public attackIndicator squareAttack;
    public attackIndicator roundAttack;

    public float TARGETING_DIST_AHEAD_MULTIPLIER = 0.15f;


    public double ATTACK_TIMER = 3;
    public int LINE_ATTACK_LENGTH = 5;

    public int DEFEND_ATTACK_HEIGHT = 4;
    public int DEFEND_ATTACK_WIDTH = 5;

    public int MINI_ATTACK_HEIGHT = 2;
    public int MINI_ATTACK_WIDTH = 2;

    public int SWEEP_ATTACK_HEIGHT = 7;
    public int SWEEP_ATTACK_WIDTH = 8;

    List<string> attackList = new List<string>();
    Queue<string> lockedAttacks = new Queue<string>();
    Queue<string> pendingAttacks = new Queue<string>();

    Vector3 lastMiniAttack = new Vector3(-99, 0, 0);

    double timer;

    // Start is called before the first frame update
    void Start()
    {
        this.timer = this.ATTACK_TIMER;

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
            this.InitiateAttack_and_ResetTimer();

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

    void advanceBattle()
    {
        this.EnqueueAttack();
    }

    void InitiateAttack_and_ResetTimer()
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

    Vector3 locateTarget()
    {
        if (chainmanager.chain[0].GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            return chainmanager.positions[chainmanager.positions.Count - 1];

        else
        {
            Vector3 point1 = chainmanager.positions[chainmanager.positions.Count - 1];
            Vector3 point2 = chainmanager.positions[0];

            Vector3 difference = point1 - point2;

            Vector3 multiplier = new Vector3(TARGETING_DIST_AHEAD_MULTIPLIER, TARGETING_DIST_AHEAD_MULTIPLIER, 1);

            Vector3 attackPoint = point1 + TARGETING_DIST_AHEAD_MULTIPLIER * difference;

            return attackPoint;
        }
    }
    
    void initiateLineAttack() {

        //Replace this with code to get two dudes


        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.transform.position = locateTarget();
        attackInstance.GetComponent<attackIndicator>().DefineAttack(2, 0.3, 0.2, 1, true, false, true);

        Vector3 point1 = chainmanager.positions[chainmanager.positions.Count - 1];
        Vector3 point2 = chainmanager.positions[0];
        Vector3 difference = point1 - point2;

        if (difference.x > difference.y)
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


    void initiateMiniAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.GetComponent<attackIndicator>().DefineAttack(2, 0.2, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(MINI_ATTACK_WIDTH, MINI_ATTACK_HEIGHT, 1);


        Vector3 targetPos = locateTarget();

        attackInstance.transform.position = targetPos;

        if (this.pendingAttacks.Count == 0)
            lastMiniAttack = new Vector3(-99, 0, 0);
        else
            lastMiniAttack = targetPos;
    }

    void initiateSweepAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);

        attackInstance.GetComponent<attackIndicator>().DefineAttack(3, 0.6, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(SWEEP_ATTACK_WIDTH, SWEEP_ATTACK_HEIGHT, 0);
        attackInstance.transform.position = this.transform.position + new Vector3(0, -1, 0);
    }

}

