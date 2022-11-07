using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraMove : MonoBehaviour
{
    [SerializeField] private bool isDebugMode = false;
    [SerializeField] private float camSpeed;
    void Update()
    {
        if (!isDebugMode) return;
        if(Input.GetKey(KeyCode.Comma))
            Camera.main.transform.position += Vector3.forward * (camSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.O))
            Camera.main.transform.position -= Vector3.forward * (camSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.E))
            Camera.main.transform.position += Vector3.right * (camSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
            Camera.main.transform.position -= Vector3.right * (camSpeed * Time.deltaTime);
    }
}
