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
    }
}
