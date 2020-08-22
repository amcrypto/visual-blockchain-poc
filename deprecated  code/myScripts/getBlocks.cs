using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ardor;
using UnityEngine.Networking;
using System;

public class getBlocks : MonoBehaviour
{

    public requestBlocks request;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("_requestBlocks", request);
    }
    private dataBlock info;
    public dataBlocks blocks;

    public List<dataBlock> _Blocks;
    public List<GameObject> blockList;
    //public dataBlock[] Blocks;
    public GameObject ardorBlock;
    public GameObject Center;
    // Update is called once per frame
    public IEnumerator _requestBlocks(requestBlocks data)
    {
       // print("test Request");
        if (data == null)
        {
            print("was null");
            data = new requestBlocks();
        }
        WWWForm form = new WWWForm();
        form.AddField("requestType", "getBlocks");
        //string request = "http://localhost:27876/nxt";
        //request += "requestType ";

        if (data.timestamp != null)
        {
            form.AddField("timestamp", data.timestamp);
        }
        if (data.firstIndex != 0)
        {
            form.AddField("firstIndex", data.firstIndex);
        }
        if (data.lastIndex != 0)
        {
            form.AddField("lastIndex", data.firstIndex+data.lastIndex);
        }

        if (data.includeTransactions != false)
        {
            form.AddField("includeTransactions", "true");
        }
        if (data.includeExecutedPhased != false)
        {
            form.AddField("includeExecutedPhased", "true");
        }
        if (data.requireBlock != null)
        {
            form.AddField("requireBlock", data.requireBlock);
        }
        if (data.requireLastBlock != null)
        {
            form.AddField("requireLastBlock", data.requireLastBlock);
        }
        //print("sent");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
        //print("waiting");
        yield return www.SendWebRequest();
        //print("finished");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //print("no error");
            //Debug.Log(www.downloadHandler.text);
            blocks = JsonUtility.FromJson<dataBlocks>(www.downloadHandler.text);
            //tools.getClassValues(returnData);
            //this.SendMessage("handle", returnData);
            //Blocks = blocks.blocks;
            _Blocks = new List<dataBlock>(blocks.blocks);
            StartCoroutine("BuildChain", _Blocks);
        }
    }

    public IEnumerator BuildChain(List<dataBlock> _blocks)
    {
        int i = 0;
        foreach(dataBlock _block in _blocks)
        {
            GameObject _display = Instantiate(ardorBlock, new Vector3(Center.transform.localPosition.x + i, Center.transform.localPosition.y, Center.transform.localPosition.z), Quaternion.identity);
            _display.transform.parent = Center.transform;
            _display.SendMessage("handle", _block);
            blockList.Add(_display);
            i++;

        }
        refreshList();
        yield return null;

    }


    public void handleAddBlock(string _block)
    {
        StartCoroutine("addBlockID", _block);
       // print("adding");
    }
    public IEnumerator addBlockID(string _block)
    {

        WWWForm form = new WWWForm();
        form.AddField("requestType", "getBlock");
        form.AddField("block", _block);
        form.AddField("includeTransactions", "true");
        form.AddField("includeExecutedPhased", "true");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            info = JsonUtility.FromJson<dataBlock>(www.downloadHandler.text);
            //tools.getClassValues(returnData);

       

        GameObject _display = Instantiate(ardorBlock, new Vector3(Center.transform.localPosition.x, Center.transform.localPosition.y, Center.transform.localPosition.z), Quaternion.identity);
            _display.transform.parent = Center.transform;
            _display.SendMessage("handle", info);
            blockList.Add(_display);
            refreshList();
        }
        yield return null;
    }

    public void refreshList()
    {
        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].transform.localPosition = new Vector3(i, 0, 0);
        }
    }



    


}
