using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ardor;
using TMPro;
using System;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;

public class blockHolder : MonoBehaviour
{
    public requestBlock _request;
    public dataBlock _data;
    public TextMeshPro labelBlockID;
    public GameObject transactionHolder;
    public dataTransactions[] transactions;
    public GameObject display;

    public void handle(dataBlock info)
    {
        StartCoroutine(createTransactions(info));
        print("sent");
    }

    public IEnumerator createTransactions(dataBlock info)
    {
        yield return new WaitForEndOfFrame();
        _data = info;
        transactions = info.transactions;
        labelBlockID.text = info.block;

        int i = 0;

        foreach (dataTransactions trans in info.transactions)
        {
            print(trans.block);
             GameObject _display = Instantiate(display, new Vector3(transactionHolder.transform.position.x, transactionHolder.transform.localPosition.y + i+1, transactionHolder.transform.position.z), Quaternion.identity);
            _display.transform.parent = transactionHolder.transform;
            _display.SendMessage("fillData", trans);
             i++;

        }
yield return null;   
}
    public void sendRefresh()
    {
        StartCoroutine(refreshBlock(_request));
        labelBlockID.text = _request.block;
        print("refresh");
    }
    public IEnumerator refreshBlock(requestBlock data)
    {
        if (data == null)
        {
            data = new requestBlock();
        }
        WWWForm form = new WWWForm();
        form.AddField("requestType", "getBlock");
        string request = "http://localhost:27876/nxt";
        request += "requestType ";

        if (data.block != null)
        {
            form.AddField("block", data.block);
        }
        if (data.height != null)
        {
            form.AddField("height", data.height);
        }
        if (data.timestamp != null)
        {
            form.AddField("timestamp", data.timestamp);
        }
//if (data.includeTransactions != false)
       // {
            form.AddField("includeTransactions", "true");
        //}
      //  if (data.includeExecutedPhased != false)
       // {
            form.AddField("includeExecutedPhased", "true");
       // }
        if (data.requireBlock != null)
        {
            form.AddField("requireBlock", data.requireBlock);
        }
        if (data.requireLastBlock != null)
        {
            form.AddField("requireLastBlock", data.requireLastBlock);
        }

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            _data = JsonUtility.FromJson<dataBlock>(www.downloadHandler.text);
            //tools.getClassValues(returnData);
            StartCoroutine(createTransactions(_data));
        }
    }

    #region
    public void setBlockID(string id)
    {
        _request.block = id;
    }
    #endregion
}
