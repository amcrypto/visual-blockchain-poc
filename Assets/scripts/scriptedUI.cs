using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ardor;

public class scriptedUI : MonoBehaviour
{

    public createRequestObject[] requests;
    public List<Request> formData;
    public GameObject UIElement;
    public Text rawData;
    public Ardor.Data.Block test;
    public string random;
    public GameObject displayHolder;
    public GameObject blockDisplay;
    public GameObject transactionDisplay;
    public string newline;
    public List<string> apiUrl;
    public void Start()
    {
        setAPI(0);
        buildRequestMenu(0);
    }

    public void setAPI(int i)
    {
        Ardor.Tools.apiUrl = apiUrl[i];
    }

    public void buildRequestMenu(int I)
    {
        print("building");
        // Clear old UI fields
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        formData.Clear();
        // Load request data into form instance
        foreach (Ardor.Request r in requests[I].field) 
        {
            Ardor.Request R = new Ardor.Request( r.key, r.value );
            formData.Add(R);          
        }


        //Create request UI Form
        int i = 0;
        foreach (Ardor.Request r in formData)
        {
            
           GameObject temp = Instantiate(UIElement,  new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(this.transform);
            requestElement _request = temp.GetComponent<requestElement>();
           _request.title.text = r.key;
           _request.id = i;
           _request.scripted = this;
           _request.dataInput.text = r.value;
            if (i == 0)

            {
                _request.dataInput.readOnly = true;
            }
            i++;
        }
    }

    // send api request and return data as string
    public void getData()
    {
        string requestType = formData[0].value;
        print(requestType);
        StartCoroutine(Ardor.Tools.getData(formData, (returnValue) => { print(returnValue); this.SendMessage(requestType, returnValue); }));

    }
    #region Block Actions
    public void getBlockId(string s)
    {
        clearDisplay();
        s = s.Replace(",", newline);
        rawData.text = s;

    }
    public void getBlockchainStatus(string s)
    {
        clearDisplay();
        s = s.Replace(",", newline);
        rawData.text = s;
    }
    public void getECBlock(string s)
    {
        clearDisplay();
        s = s.Replace(",", newline);
        rawData.text = s;
    }
    public void getBlock(string s)
    {
        clearDisplay();
        print("block");
        Ardor.Data.Block Block = JsonUtility.FromJson<Ardor.Data.Block>(s);

        GameObject temp = Instantiate(blockDisplay, new Vector3(displayHolder.transform.position.x, displayHolder.transform.position.y, displayHolder.transform.position.z), Quaternion.identity);
        temp.transform.SetParent(displayHolder.transform);
        temp.transform.rotation = displayHolder.transform.rotation;
        temp.SendMessage("display", Block);

        // Display block transactions not including phasedTransactions
        if(Block.transactions.Length > 0)
        {
            int i = 0;
            foreach(Ardor.Data.Transaction t in Block.transactions)
            {
                print(i);
                distributeTransactions(temp, t, i);
                i++;
            }
        }
        //s = s.Replace(",", newline);
        rawData.text = "";
        //distributeTransactions(temp);
    }
    public void getBlocks(string s)
    {
        clearDisplay();

        Ardor.Data.Blocks blocks = JsonUtility.FromJson<Ardor.Data.Blocks>(s);
        int z = 0;
        foreach (Ardor.Data.Block b in blocks.blocks)
        {
            GameObject temp = Instantiate(blockDisplay, new Vector3(displayHolder.transform.position.x + (z*1.1f), displayHolder.transform.position.y, displayHolder.transform.position.z), Quaternion.identity);
            temp.transform.SetParent(displayHolder.transform);
            temp.transform.rotation = displayHolder.transform.rotation;
            temp.SendMessage("display", b);

            // Display block transactions not including phasedTransactions
            if (b.transactions.Length > 0)
            {
                 int i = 0;
                foreach (Ardor.Data.Transaction t in b.transactions)
                {
                    print(t.chain+" : chain");
                    distributeTransactions(temp, t, i);
                    i++;
                }
            }
            z++;
        }


        print("blocks");
    }
    #endregion

    public void distributeTransactions(GameObject displayHolder, Ardor.Data.Transaction t, int i)
    {
        GameObject temp = Instantiate(transactionDisplay, new Vector3(displayHolder.transform.position.x, displayHolder.transform.position.y+i+1, displayHolder.transform.position.z), Quaternion.identity);
        temp.transform.SetParent(displayHolder.transform);
        temp.name = t.transaction;
        displayTransaction Temp = (displayTransaction)temp.GetComponent("displayTransaction");

        Temp.transaction = t;
        Temp.displayHolder = displayHolder.transform;
        Temp.Display(t);

    }

    public void clearDisplay()
    {
        foreach (Transform child in displayHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
