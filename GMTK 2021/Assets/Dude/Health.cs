using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 2;
    public enum type
    {
        player,
        pillar,
        boss
    };

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
