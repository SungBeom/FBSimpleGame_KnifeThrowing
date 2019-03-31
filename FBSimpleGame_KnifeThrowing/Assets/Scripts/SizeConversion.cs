using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeConversion : MonoBehaviour
{
    public float changeSpeed = 0.5f;
    bool bigger = true;

    void Update()
    {
        if (bigger)
        {
            transform.localScale += Vector3.one * Time.deltaTime * changeSpeed;
            if (transform.localScale.x > 1.25f) bigger = false;
        }
        else
        {
            transform.localScale -= Vector3.one * Time.deltaTime * changeSpeed;
            if (transform.localScale.x < 1f) bigger = true;
        }
    }
}
