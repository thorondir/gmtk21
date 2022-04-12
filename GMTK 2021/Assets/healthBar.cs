using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setFill(float current, float max)
    {
        print("Set fill to");
        float fillPercent = current / max;
        print(fillPercent);
        bar.fillAmount = fillPercent;
    }

}

