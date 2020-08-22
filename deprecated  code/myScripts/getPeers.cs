using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ardor;

public class getPeers : MonoBehaviour
{
    public dataPeers info;
    public List<dataPeer> active;
    public List<dataPeer> non_connected;
    public List<dataPeer> connected;

    public void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        StartCoroutine("GetPeers");
    }

    public IEnumerator GetPeers()
    {

        WWWForm form = new WWWForm();
        form.AddField("requestType", "getPeers");
        form.AddField("includePeerInfo", "true");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:27876/nxt", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            info = JsonUtility.FromJson<dataPeers>(www.downloadHandler.text);
            //print(www.downloadHandler.text);
            foreach (dataPeer peer in info.peers)
            {
                if (peer.state == 0)
                {
                    non_connected.Add(peer);
                }
                if (peer.state == 1)
                {
                    connected.Add(peer);
                }
                if (peer.state == 2)
                {
                    active.Add(peer);
                }
            }
        }
        this.SendMessage("addPeers", active);
    }
}
