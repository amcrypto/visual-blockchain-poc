using AdvancedDissolve_Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ardor;
public class updateUnconfirmed : MonoBehaviour
{
    public Controller_Mask_XYZ_Axis control;
    public getBlocks _blocks;
    public GameObject displayBlock;
    public float timeRemaining = 0;
    public float offset;
    public Vector3 location;
    public List<string> unconfirmedIDs;
    public GameObject displayUnconfirmed;
    public GameObject ardorUnconfirmedIcon;
    public GameObject ignisUnconfirmedIcon;
    public GameObject bitsUnconfirmedIcon;
    // Start is called before the first frame update
    void Start()
    {
        control.offset = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining < 75)
        {
            timeRemaining += Time.deltaTime;
        }
        control.offset = timeRemaining / 75;
        offset = control.offset;
        location.x = _blocks.blockList.Count;
        displayBlock.transform.localPosition = location;

    }

    public void addUncofirmed(string id)
    {
        bool used = false;
        foreach (string s in unconfirmedIDs)
        {
            if(s == id)
            {
                used = true;
            }
            else
            {
                used = false;
            }
        }
        if (used != true)
        {
            unconfirmedIDs.Add(id);
            addIcon(id);
        }
    }

    public void removeUncofirmed(string id)
    {
        bool used = false;
        foreach (string s in unconfirmedIDs)
        {
            if (s == id)
            {
                used = true;
            }
            else
            {
                used = false;
            }
        }
        if (used == true)
        {
            unconfirmedIDs.Remove(id);
            removeIcon(id);
        }
    }

    public void handleAddBlock()
    {
        //print("mwhaahaahaa");
        StartCoroutine("refreshUnconfirmed");
    }
    public IEnumerator refreshUnconfirmed()
    {
        unconfirmedIDs.Clear();
        foreach (Transform t in displayBlock.transform)
        {
            Destroy(t.gameObject);
            
        }
        for (int i = 1; i < 3; i++)
        {
            WWWForm form = new WWWForm();
            form.AddField("requestType", "getUnconfirmedTransactionIds");
            form.AddField("chain", i);
            UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                getUnconfirmedTransactionIds handler = JsonUtility.FromJson<getUnconfirmedTransactionIds>(www.downloadHandler.text);
                foreach(string id in handler.unconfirmedTransactionIds)
                {
                    unconfirmedIDs.Add(id);
                    addIcon(id);
                }
            }
            yield return null;
        }
    }



    public void addIcon(string id)
    {
        print("addingIcon");
        print(id[0]);
        if (id[0].ToString() == "1") { GameObject icon = Instantiate(ardorUnconfirmedIcon, new Vector3(0, 0, 0), Quaternion.identity); icon.transform.parent = displayBlock.transform; icon.name = id; }
        if (id[0].ToString() == "2") { GameObject icon = Instantiate(ignisUnconfirmedIcon, new Vector3(0, 0, 0), Quaternion.identity); icon.transform.parent = displayBlock.transform; icon.name = id; }
        if (id[0].ToString() == "4") { GameObject icon = Instantiate(bitsUnconfirmedIcon, new Vector3(0, 0, 0), Quaternion.identity); icon.transform.parent = displayBlock.transform; icon.name = id; }
        arrangeIcons();
    }
    public void removeIcon(string id)
    {

        GameObject temp = displayBlock.transform.Find(id).gameObject;
        Destroy(temp);
        arrangeIcons();
    }

    public void arrangeIcons()
    {
        int i = 1;
        foreach(Transform t in displayBlock.transform)
        {
            Vector3 location =  new Vector3(0,i,0);
            t.localPosition = location;
            i++;
        }
    }

}
