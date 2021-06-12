using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 2;
    public int myType;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("something got hit");
        if (GetComponent<Pillar>() != null)
        {
            Debug.Log("I am a pillar");
        }
        if (GetComponent<MrWhippy>() != null)
        {
            Debug.Log("I am a Boss");
        }
        if (GetComponent<link_movement>() != null)
        {
            Debug.Log("I am a Ying's little monster");
        }
        BroadcastMessage("GotHit", SendMessageOptions.RequireReceiver);
    }
}
