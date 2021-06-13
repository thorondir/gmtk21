using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public IEnumerator Shake(float magnitude, float duration)
    {
        Vector2 oldPos = transform.localPosition;

        float timer = 0f;
        while (timer < duration)
        {
            // Camera displacement
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            // Adjust displacement
            transform.localPosition = new Vector2(x, y);

            // Increment timer
            timer += Time.deltaTime;
            // Wait until next frame before looping
            yield return null;
        }
        // reset to starting position
        transform.localPosition = oldPos;
    }
}
