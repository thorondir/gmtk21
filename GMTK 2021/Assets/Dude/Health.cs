using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 2;
    public enum Type
    {
        player,
        pillar,
        boss
    };
    public Type myType;

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
