using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabbing : MonoBehaviour
{
    [SerializeField]
    bool hasWeapon;

    public float distanceThreshold;
    GameObject weapon;

    // Start is called before the first frame update
    public bool CollectWeapon()
    {
        if (hasWeapon) return false;
        else return hasWeapon = true;
    }

    // Update is called once per frame
    void Start()
    {
        hasWeapon = false;
        weapon = transform.GetChild(0).gameObject;
    }

    public void AttemptAttack(GameObject target) {
        Vector2 direction = target.transform.position - transform.position;
        weapon.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x));
        if (direction.magnitude < distanceThreshold) {
            // attack
        }
    }
}
