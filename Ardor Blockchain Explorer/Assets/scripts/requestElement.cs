using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class requestElement : MonoBehaviour
{
    public Text title;
    public InputField dataInput;
    public int id;
    public scriptedUI scripted;

    public void fillData(string s)
    {
        scripted.formData[id].value = s;
    }
    // Start is called before the first frame update

}
