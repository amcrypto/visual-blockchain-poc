using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class moveToCenter : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform display;
    public float zPos;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(-display.localPosition.x/2, -display.localPosition.y, zPos);
    }
}
