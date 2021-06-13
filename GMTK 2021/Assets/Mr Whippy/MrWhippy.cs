using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class MrWhippy : MonoBehaviour
{
    public chainmanager chainmanager;
    public int hp;

    public attackIndicator squareAttack;
    public attackIndicator roundAttack;
    public attackIndicator quickAttack;
    public attackIndicator slowAttack;

    public float TARGETING_DIST_AHEAD_MULTIPLIER;
    public float TARGETING_DIST_BEHIND_MULTIPLIER = 0.5f;

    public double V_SHORT_WINDUP = 0.5;
    public double SHORT_WINDUP = 1;
    public double LONG_WINDUP = 3;

    public double SHORT_STRIKE = 0.3;
    public double LONG_STRIKE = 0.6;

    public double ATTACK_TIMER;
    public double FAST_ATTACK_TIMER = 0.7;

    public int HitsUntilBigCoolDown = 3;
    public double BIG_COOLDOWN_LENGTH = 4;

    public int LINE_ATTACK_LENGTH;

    public int DEFEND_ATTACK_HEIGHT;
    public int DEFEND_ATTACK_WIDTH;

    public int MINI_ATTACK_HEIGHT;
    public int MINI_ATTACK_WIDTH;

    public int SWEEP_ATTACK_HEIGHT;
    public int SWEEP_ATTACK_WIDTH;

    public bool PILLAR_BREAKING_MODE;

    List<string> attackList = new List<string>();
    Queue<string> lockedAttacks = new Queue<string>();
    Queue<string> pendingAttacks = new Queue<string>();

    public GameObject SndManager;

    int phase = 1;

    Vector3 lastMiniAttack = new Vector3(-99, 0, 0);

    double timer;
    enum animationTrigger { AttackStart, AttackEnd, QuickAttack };


    // Start is called before the first frame update
    void Start()
    {
        this.timer = 3;

        //this.attackList.Add("sweep");
        //this.attackList.Add("triple");
        this.lockedAttacks.Enqueue("triple");
        this.lockedAttacks.Enqueue("defend");
        this.lockedAttacks.Enqueue("sweep");

        //Put this line when battle advances
        // this.attackList.Add(this.lockedAttacks.Dequeue());
        // this.attackList.Add(this.lockedAttacks.Dequeue());
        //this.attackList.Add(this.lockedAttacks.Dequeue());


        this.PILLAR_BREAKING_MODE = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timer <= 0)
        {
            this.InitiateAttack_and_ResetTimer();
        }
        else
        {
            this.timer -= Time.deltaTime;
        }

    }

    IEnumerator startAnimation(string trigger, double duration)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(trigger);
        yield return new WaitForSeconds((float)duration);
        animator.SetTrigger("AttackEnd");

    }

    void GotHit()
    {

        if (PILLAR_BREAKING_MODE)
            SndManager.GetComponent<SoundManager>().playSound("hitBlocked");
        else
        {
            GetComponent<Health>().health -= 1;
            SndManager.GetComponent<SoundManager>().playSound("demonPain");
        }

        if (GetComponent<Health>().health <= 0)
        {
            SndManager.GetComponent<SoundManager>().playSound("phaseChange");
            this.PILLAR_BREAKING_MODE = true;
        }

    }

    void EnqueueRandomAttack()
    {
        int randAttack = UnityEngine.Random.Range(0, this.attackList.Count);
        string nextAttack = this.attackList[randAttack];
        this.pendingAttacks.Enqueue(nextAttack);
    }

    //Call this function when pillar falls
    public void advanceBattle()
    {
       if (phase != 4) attackList.Add(this.lockedAttacks.Dequeue());
        PILLAR_BREAKING_MODE = false;
        
        this.phase += 1;
        if (this.phase == 2)
            GetComponent<Health>().health = 3;
        else if (this.phase == 3)
            GetComponent<Health>().health = 6;
        else if (this.phase == 4)
            GetComponent<Health>().health = 9;
        else
            this.transform.localScale = new Vector3(2, (float)0.5, -2);
    }

    bool isPlayerInCenter()
    {
        Vector3 playerPos = chainmanager.chain[0].transform.position;

        if (chainmanager.chain[0].transform.position.x < 3 && chainmanager.chain[0].transform.position.x > -3)
            if (chainmanager.chain[0].transform.position.y < 3 && chainmanager.chain[0].transform.position.y > -3)
                return true;

        return false;
    }
    void InitiateAttack_and_ResetTimer()
    {
        string chosenAttack;

        if (PILLAR_BREAKING_MODE)
            if (this.phase != 1 && UnityEngine.Random.Range(0, 3) == 0)
                chosenAttack = "mini";
            else
                chosenAttack = "line";

        else if (this.HitsUntilBigCoolDown <= 0)
        {
            chosenAttack = "noAttack";
            this.timer = BIG_COOLDOWN_LENGTH;
            this.HitsUntilBigCoolDown = UnityEngine.Random.Range(3, 6);
        }

        else if (this.pendingAttacks.Count == 0)
        {
            if (isPlayerInCenter() && (UnityEngine.Random.Range(0, 2) > 0 && this.phase >= 3))
                chosenAttack = "defend";
            else
            {
                int randAttack = UnityEngine.Random.Range(0, this.attackList.Count);
                chosenAttack = this.attackList[randAttack];
            }
            this.HitsUntilBigCoolDown -= 1;

        }
        else
            chosenAttack = pendingAttacks.Dequeue();

        if (chosenAttack == "line")
        {
            this.initiateLineAttack();
            this.timer = SHORT_WINDUP + SHORT_STRIKE + 1;
        }
        if (chosenAttack == "defend")
        {
            this.initiateDefendAttack();
            this.timer = LONG_WINDUP + LONG_STRIKE + 1;
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
            this.timer = 1;
        }
        if (chosenAttack == "sweep")
        {
            this.initiateSweepAttack();
            this.timer = LONG_WINDUP + LONG_STRIKE + 0.5;
        }

        this.SndManager.GetComponent<SoundManager>().playSound("demonIdle");
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
    Vector3 locateTargetBack()
    {
        if (chainmanager.chain[0].GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            return chainmanager.positions[chainmanager.positions.Count - 1];

        else
        {
            Vector3 point2 = chainmanager.positions[chainmanager.positions.Count - 1];
            Vector3 point1 = chainmanager.positions[0];

            Vector3 difference = point1 - point2;

            Vector3 multiplier = new Vector3(TARGETING_DIST_BEHIND_MULTIPLIER, TARGETING_DIST_BEHIND_MULTIPLIER, 1);

            Vector3 attackPoint = point1 - TARGETING_DIST_BEHIND_MULTIPLIER * difference;

            return attackPoint;
        }
    }

    void initiateLineAttack()
    {

        //Replace this with code to get two dudes

        attackIndicator attackInstance = Instantiate(squareAttack);

        attackInstance.transform.position = locateTargetBack();
        attackInstance.GetComponent<attackIndicator>().DefineAttack(SHORT_WINDUP, SHORT_STRIKE, 0.2, 1, true, false, true);

        Vector3 point1 = chainmanager.positions[chainmanager.positions.Count - 1];
        Vector3 point2 = chainmanager.positions[0];
        Vector3 difference = point1 - point2;


        /*
        if (difference.x > difference.y)
            //do vert
            attackInstance.transform.localScale = new Vector3(1, LINE_ATTACK_LENGTH, 0);
        else
            //do hor
            attackInstance.transform.localScale = new Vector3(LINE_ATTACK_LENGTH, 1, 0);
        */

        attackInstance.transform.localScale = new Vector3(LINE_ATTACK_LENGTH, 0.7f, 3);

        double toa = Math.Atan(attackInstance.transform.position.y / attackInstance.transform.position.x) * (180 / Math.PI);
        attackInstance.transform.Rotate(0, 0, (float)toa);

        StartCoroutine(startAnimation("AttackStart", SHORT_WINDUP));
    }

    void initiateDefendAttack()
    {
        attackIndicator attackInstance = Instantiate(roundAttack);
        attackInstance.transform.position = transform.position - new Vector3(0,0.5f,3);
        attackInstance.GetComponent<attackIndicator>().DefineAttack(LONG_WINDUP, LONG_STRIKE, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(DEFEND_ATTACK_WIDTH, DEFEND_ATTACK_HEIGHT, 0);

        StartCoroutine(startAnimation("AttackStart", LONG_WINDUP));
    }


    void initiateMiniAttack()
    {
        attackIndicator attackInstance = Instantiate(quickAttack);
        Vector3 targetPos = locateTarget();
        attackInstance.transform.position = targetPos;
        attackInstance.GetComponent<attackIndicator>().DefineAttack(V_SHORT_WINDUP, SHORT_STRIKE, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(MINI_ATTACK_WIDTH, MINI_ATTACK_HEIGHT, 1);
        

        if (this.pendingAttacks.Count == 0)
            lastMiniAttack = new Vector3(-99, 0, 0);
        else
            lastMiniAttack = targetPos;

        StartCoroutine(startAnimation("QuickAttack", V_SHORT_WINDUP));
    }

    void initiateSweepAttack()
    {
        attackIndicator attackInstance = Instantiate(slowAttack);
        Vector3 playerpos = chainmanager.chain[0].transform.position;
        if (playerpos.y < 0)
            attackInstance.transform.position = new Vector3(0, -1, 3);
        else
            attackInstance.transform.position = new Vector3(0, 1, 3);
        attackInstance.GetComponent<attackIndicator>().DefineAttack(LONG_WINDUP, LONG_STRIKE, 0.2, 1, true, false, false);
        attackInstance.transform.localScale = new Vector3(SWEEP_ATTACK_WIDTH, SWEEP_ATTACK_HEIGHT, 0);



        StartCoroutine(startAnimation("AttackStart", LONG_WINDUP));
    }

}

