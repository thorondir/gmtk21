using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSplosionEffects : MonoBehaviour
{
    public GameObject miniSplosion;
    bool done = false;
    // Start is called before the first frame update
    void Update()
    {
        if (done)
            return;
        Vector3 offset = transform.right;
        done = true;
        for (int i = -4; i < 5; i++)
        {
            Vector3 spawnPos = transform.position + (i / 2f * offset);
            spawnPos.z = -1;
            GameObject instance = Instantiate(miniSplosion, spawnPos, Quaternion.Euler(Vector3.zero));
        }
    }
    void OnDestroy()
    {
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("miniboom"))
        {
            Destroy(t);
        }
    }
}
