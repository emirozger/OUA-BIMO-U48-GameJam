using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateFunction : MonoBehaviour
{
    public GameObject obje;
    public float xRotationSpeed = 10f;

    void Update()
    {
        obje.transform.Rotate(xRotationSpeed * Time.deltaTime, 0, 0);
    }
}
