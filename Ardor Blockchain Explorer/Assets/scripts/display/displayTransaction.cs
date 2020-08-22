using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ardor;

public class displayTransaction : MonoBehaviour
{
    public GameObject ardor;
    public GameObject ignis;
    public GameObject bitswift;
    public Transform displayHolder;
    public Ardor.Data.Transaction transaction;
    public GameObject display;

    public void Display(Ardor.Data.Transaction t)
    {
        //print(t.senderRS + " : working");
        if (transaction == null){ transaction = t; }
        if(t.chain == 1){ display = Instantiate(ardor, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity); }
        if (t.chain == 2) { display = Instantiate(ignis, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity); }
        if (t.chain == 4) { display = Instantiate(bitswift, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity); }
        print("end");
        display.transform.SetParent(displayHolder);
        display.transform.rotation = displayHolder.rotation;
    }


}
