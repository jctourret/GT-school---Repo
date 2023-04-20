using System;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    Camera cam;
    float raycastRange = 10f;
    public static Action<Block> OnBlockHit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = raycastRange;
        mousepos = cam.ScreenToWorldPoint(mousepos);
        Debug.DrawRay(transform.position,mousepos-transform.position,Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 100))
            {
                Debug.Log(hit.collider.name);
                IReadable readable = hit.collider.GetComponent<IReadable>();
                if(readable != null)
                {
                    Block block = hit.collider.GetComponent<Block>();
                    OnBlockHit?.Invoke(block);
                }
            }
        }

    }
}
