using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speed;
    void Update()
    {
        this.transform.RotateAround(this.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
