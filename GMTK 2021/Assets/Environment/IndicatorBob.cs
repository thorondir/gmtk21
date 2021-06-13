using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBob : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    double time;
    [SerializeField]
    double duration;
    [SerializeField]
    double amp;
    double freq;

    void Awake()
    {
        pos = transform.position;
        freq = 2*Mathf.PI / duration;
    }

    // Update is called once per frame
    void Update()
    {
        freq = 2 * Mathf.PI / duration;
        time += Time.deltaTime;
        Vector3 newPos = new Vector3(pos.x, (float)amp*Mathf.Sin((float)(time * freq)) + pos.y, pos.z);
        transform.localPosition = newPos;
        if (time >= duration) time -= duration;
    }
}
