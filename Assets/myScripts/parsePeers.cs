using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using ardor;


public class parsePeers : MonoBehaviour
{
    public createSimpleScriptable geoPeers;
    public List<dataPeer> activePeers;
    public List<dataPeer> newPeers;
    public List<dataPeer> oldPeers;
    public geoDatalist parsedPeers;
    public string ips;
    public List<string> ipa;
    public void addPeers(List<dataPeer> parser)
    {
        activePeers.Clear();
        newPeers.Clear();
        oldPeers.Clear();

        foreach (dataPeer x in parser)
        {
            bool inside = false;
            foreach (geoPeer p in geoPeers.peers)
            {
                if (p.ip == x.address)
                {
                    inside = true;
                    //print(p.ip + " true");
                    oldPeers.Add(x);
                }

            }
            if (inside == false)
            {
                newPeers.Add(x);
            }

        }
        activePeers = parser;
        if(newPeers.Count >= 1){
            StartCoroutine("getGeoLocations", newPeers);
        }
    }


    public IEnumerator getGeoLocations(List<dataPeer> peers)
    {
        int i = 0;
        ips = "[";
        ipa.Clear();
        foreach (dataPeer x in peers)
        {
            if (i < 99)
            {
                print(peers.Count);
                if (i != peers.Count-1)
                {
                    ips += "\"" + x.address + "\",";
                }
                else
                {
                    ips += "\"" + x.address + "\"";
                }
                ipa.Add(x.address);
            }
            if(i == 100)
            {
                ips += "\"" + x.address + "\"";
            }
           
            i++;
        }
        ips += "]";
        //print(ips);
        var request = new UnityWebRequest("http://ip-api.com/batch?fields=8384", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(ips);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        //print(request.downloadHandler.text);

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            parsedPeers = JsonUtility.FromJson<geoDatalist>("{\"data\":" + request.downloadHandler.text + "}");
            print(request.downloadHandler.text);
        }
        //print(parsedPeers.data[0].query);
        bool Inside = false;
        foreach (geoData data in parsedPeers.data)
        {
            foreach (geoPeer p in geoPeers.peers)
            {
                if (data.query == p.ip)
                {
                    Inside = true;
                }
            }
            if (Inside != true)
            {
                geoPeer temp = new geoPeer();
                temp.ip = data.query;
                temp.location = data.lat + "," + data.lon;
                geoPeers.peers.Add(temp);
            }
            
        }

    }

}



/*




        yield return null;
    }

}
    */

