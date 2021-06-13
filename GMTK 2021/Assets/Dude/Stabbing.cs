using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabbing : MonoBehaviour
{
    [SerializeField]
    bool hasWeapon;
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
    }
}
