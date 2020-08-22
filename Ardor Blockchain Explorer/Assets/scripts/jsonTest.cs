using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ardor;


public class jsonTest : MonoBehaviour
{

    public string JSON;
    public Ardor.Data.Attachment t;
    public Ardor.Data.testA tester;

    public class testing
    {
        public Ardor.Data.Attachment tester;
    }
    // Start is called before the first frame update
    void Start()
    {
        print("test");
        JSON = JSON.Replace("version.", "");

        tester = JsonUtility.FromJson<Ardor.Data.testA>(JSON);
    }


}
