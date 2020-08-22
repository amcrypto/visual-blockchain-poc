using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class target : MonoBehaviour
{
    public moveToTarget control;

    // Start is called before the first frame update
    void Start()
    {
        control = this.transform.parent.GetComponent<moveToTarget>();
    }

    // Update is called once per frame
   public void OnClick()
    {
        print("clicked");
       control.move(this.transform);
    }
}
