using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 2;
    public int myType;
    public GameObject blood;

    public void TakeDamage(int amount)
    {
        if (myType < 2) {
            Instantiate(blood).transform.position = transform.position;
        }
        health -= amount;
        BroadcastMessage("GotHit", SendMessageOptions.RequireReceiver);
    }
}
