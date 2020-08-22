using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ardor;
using UnityEngine.Networking;
using System;
using UnityEngine.XR;

public class eventListener : MonoBehaviour
{
    [Serializable]
    public class eventRegister
    {
        public int token;
    }
    [Serializable]
    public class events
    {
        public string name;
        public string[] ids;
    }
    [Serializable]
    public class eventData
    {
        public events[] events;
        public string test;
    }

    public eventData handler;
    public eventRegister eventdata;
    public GameObject controller;
    public events infoTest;
    public updateUnconfirmed unconfirmedHolder;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("removeListeners");
        //_wait();
    }
    void _wait()
    {
        //print("restart");
        StartCoroutine("waitEvent");

    }

    // Update is called once per frame


    public IEnumerator removeListeners()
    {
        for (int i = 0; i < 130; i++) {
            WWWForm form = new WWWForm();
            form.AddField("requestType", "eventRegister");
            form.AddField("token", i);
            form.AddField("remove", "true");
            UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                //eventdata = JsonUtility.FromJson<eventRegister>(www.downloadHandler.text);
                //tools.getClassValues(returnData);
                //this.SendMessage("handle", returnData);
            }
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine("registerEvent");
    }


    public IEnumerator registerEvent()
    {

        WWWForm form = new WWWForm();
        form.AddField("requestType", "eventRegister");
        //form.AddField("token", eventdata.token);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            eventdata = JsonUtility.FromJson<eventRegister>(www.downloadHandler.text);
            //tools.getClassValues(returnData);
            //this.SendMessage("handle", returnData);
        }
        yield return new WaitForEndOfFrame();
        _wait();


    }
        public IEnumerator waitEvent()
        {

            WWWForm form = new WWWForm();
            form.AddField("requestType", "eventWait");
            form.AddField("token", eventdata.token);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            handler = JsonUtility.FromJson<eventData>(www.downloadHandler.text);
            for (int i = 0; i < handler.events.Length; i++)
            {
                if (handler.events[i].name.Contains("Block.BLOCK_PUSHED"))
                {
                    print("block pushed");
                    controller.SendMessage("handleAddBlock", handler.events[i].ids[0]);
                    unconfirmedHolder.timeRemaining = 0;
                }
                if (handler.events[i].name.Contains("Peer.CHANGE_ACTIVE_PEER"))
                {
                    print("Change Peer");
                    //controller.SendMessage("handleAddBlock", handler.events[i].ids[0]);
                    //unconfirmedHolder.timeRemaining = 0;
                }
                if (handler.events[i].name.Contains("Transaction.ADDED_UNCONFIRMED_TRANSACTIONS"))
                {
                    print("add uncomfirmed");
                    controller.SendMessage("addUncofirmed", handler.events[i].ids[0]);
                    //unconfirmedHolder.timeRemaining = 0;
                }
                if (handler.events[i].name.Contains("REMOVE_UNCONFIRMED_TRANSACTIONS"))
                {
                    print("remove uncomfirmed");
                    
                    controller.SendMessage("removeUncofirmed", handler.events[i].ids[0]);
                    //unconfirmedHolder.timeRemaining = 0;
                }
            }
            //tools.getClassValues(returnData);
            //this.SendMessage("handle", returnData);

            }
        yield return new WaitForEndOfFrame();
        _wait();

        }
    void handlerBlockAdd(string info)
    {
        print("restart");
        //controller.SendMessage("", info);

    }

}
