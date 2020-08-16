using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ardor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/createSimpleScriptable", order = 1)]

public class createSimpleScriptable : ScriptableObject
{

    public List<geoPeer> peers;

}