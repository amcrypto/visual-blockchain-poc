using ardor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayTransaction : MonoBehaviour
{
    public GameObject bitswiftIcon;
    public GameObject ignisIcon;
    public GameObject ardorICon;
    public Transform here;
    public dataTransactions _data;
    // Start is called before the first frame update

    // Update is called once per frame

    public void fillData(dataTransactions info)
    {
        print("is it triggering");
        StartCoroutine("transferInfo", info);
    }
    IEnumerator transferInfo(dataTransactions info)
    {
        yield return new WaitForEndOfFrame();
        _data = info;
        print(here.position);
        if(_data.chain == 1) { GameObject icon = Instantiate(ardorICon, new Vector3(here.localPosition.x, here.localPosition.y, here.localPosition.z), Quaternion.identity); icon.transform.parent = here; icon.transform.position = here.position; }
        if (_data.chain == 2) { GameObject icon = Instantiate(ignisIcon, new Vector3(here.localPosition.x, here.localPosition.y, here.localPosition.z), Quaternion.identity); icon.transform.parent = here; icon.transform.position = here.position; }
        if (_data.chain == 3) { GameObject icon = Instantiate(bitswiftIcon, new Vector3(here.localPosition.x, here.localPosition.y, here.localPosition.z), Quaternion.identity); icon.transform.parent = here; icon.transform.position = here.position; }
        //print("transaction sent");
        yield return null;
    }
}
