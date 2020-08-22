using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ardor;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RequestObject", order = 1)]
public class createRequestObject : ScriptableObject
{
    public List<Request> field;
}

