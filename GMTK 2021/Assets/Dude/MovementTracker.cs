using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracker : MonoBehaviour
{
    public List<Vector2> past;
    public float recencyWeighting = .8f;
    public int noPolls = 5;
    public double pollSpacing = 0.2;
    double timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0;
    }

    // calculates a recency weighted sum
    // of recent velocities and scales by the significance of each measure
    public Vector2 GetVelocity()
    {
        Vector2 prev = past[0];
        Vector2 avg = new Vector2(0, 0);
        for (int i = 1; i < past.Count; i++)
        {
            avg += recencyWeighting*past[i] - (1f-recencyWeighting)*prev;
        }
        return avg * (float)(noPolls / pollSpacing);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pollSpacing)
        {
            timer -= pollSpacing;
            past.Add((Vector2)transform.position);
            if (past.Count > noPolls) past.RemoveAt(0);
        }
    }
}
