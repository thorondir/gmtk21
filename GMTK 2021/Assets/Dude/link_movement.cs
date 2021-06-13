using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_movement : MonoBehaviour
{
    public GameObject parent;
    public float distance;
    public Health hp;
    Rigidbody2D rb;
    Animator anim;
    public float speed = 2.5f;
    public float maxSpeed = 5f;
    public float drag = 0.75f;
    public double animationThreshold = 0.2;

    Vector2 direction;
    Vector2 velocity;

    public GameObject SndManager;
    SoundManager sndmgr;

    public bool dead = false;

    public bool hasWeapon;

    public float distanceThreshold;
    public double cooldown;
    public double timestamp;
    GameObject weapon;
    Animation attackAnim;
    Health enemyHealth;

    public GameObject BloodTrail;

    public GameObject Dagger;
    public float rad;

    public GameObject shardIndicator;
    GameObject ind;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = GetComponent<Health>();
        sndmgr = SndManager.GetComponent<SoundManager>();

        distanceThreshold = 1.5f;
        hasWeapon = false;
        weapon = transform.GetChild(1).gameObject;

        cooldown = 0.5;
        attackAnim = weapon.GetComponent<Animation>();
        timestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        if (velocity.x > animationThreshold) {
            anim.SetInteger("x_movement", 1);
        } else if (velocity.x < -animationThreshold) {
            anim.SetInteger("x_movement", -1);
        } else {
            anim.SetInteger("x_movement", 0);
        }
        if (velocity.y > animationThreshold) {
            anim.SetInteger("y_movement", 1);
        } else if (velocity.y < -animationThreshold) {
            anim.SetInteger("y_movement", -1);
        } else {
            anim.SetInteger("y_movement", 0);
        }
    }

    // move towards some position if at some distance from a reference point
    public void moveTowards(Vector2 position, Vector2 reference, float distance) {
        if ((reference - (Vector2) transform.position).magnitude > distance) {
            direction = position - (Vector2) transform.position;
            direction.Normalize();
            if (rb.velocity.magnitude < maxSpeed)
                rb.velocity += direction * speed;
            else 
                rb.velocity = direction * maxSpeed;
        } else {
            rb.velocity = rb.velocity * drag;
        }
    }

    void GotHit() {
        if (hp.health <= 0)  {
            if (!dead) {
                sndmgr.playSound("manDeath");
                anim.SetBool("is_dead", true);
                Instantiate(BloodTrail, transform);
                dead = true;
                speed *= 0.9f;
                maxSpeed *= 0.9f;
                if (hasWeapon) {
                    Destroy(ind);
                    float ang = Random.Range(-Mathf.PI, 0);
                    Vector2 changePos = new Vector2(rad*Mathf.Cos(ang) + transform.position.x+.7f, rad*Mathf.Sin(ang)/2 + transform.position.y);
                    GameObject newDagger = Instantiate(Dagger, changePos, Quaternion.identity);
                }
            }
        } else if (hp.health > 0){
            sndmgr.playManPain();
        }
            
    }

    public bool CollectWeapon()
    {
        if (hasWeapon || dead) return false;
        else {
            hasWeapon = true;
            ind = Instantiate(shardIndicator, new Vector3(0, 0.8f, 0), Quaternion.identity, transform);

            return true;
        }
    }

    public void AttemptAttack(GameObject target) {
        if (enemyHealth == null)
            enemyHealth = target.GetComponent<Health>();

        if (hasWeapon && Time.time - timestamp > cooldown) {
            timestamp = Time.time;
            Vector2 direction = (target.transform.position + new Vector3(0,0.5f,0)) - transform.position;
            weapon.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            if (direction.magnitude < distanceThreshold) {
                weapon.SetActive(true);
                attackAnim.Play();
                enemyHealth.TakeDamage(1);
            }
        }
    }
}
