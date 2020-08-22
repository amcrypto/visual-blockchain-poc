using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToTarget : MonoBehaviour
{
    //public moveToTarget(Transform t) { target = t; }
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float speed;
    public float scrollSpeed;
    public Transform target;
    public Camera _camera;
    public Vector3 location;
    // Start is called before the first frame update


    public IEnumerator move(Transform t)
    {
        target = t;
        location = new Vector3(t.position.x + offsetX, t.position.y + offsetY, t.position.z + offsetZ);
        print("triggered");
        while (Vector3.Distance(_camera.transform.position, location) > 0.01f)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, location, speed);
            _camera.transform.LookAt(t);
            print(_camera.transform.position);
            yield return null;

        }
        yield return null;
    }

      void Update()
        {
        if (Input.GetMouseButton(0))
            {
            print("clicking");
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                print(hit.transform.name);
                StartCoroutine(move(hit.transform));
            }
            }
        if(Input.mouseScrollDelta.y != 0)
        {
            print(Input.mouseScrollDelta.y);
            offsetZ += Input.mouseScrollDelta.y  * scrollSpeed;
            //offsetZ = Mathf.Clamp(offsetZ, -1f, -4f);
            if (target != null)
            {
                _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, target.position.z + offsetZ);
            }
        }

        }
         public void setOffset(float f)
        {
        offsetZ = f;
        }

}
