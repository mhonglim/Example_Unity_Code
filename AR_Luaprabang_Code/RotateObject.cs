using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject MyObject;
    public float RotateSpeed = 50f;
    bool CanRotate = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CanRotate == true)
        {
            MyObject.transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);
        }
    }

    public void RotateObj()
    {
        if(CanRotate == false)
        {
            CanRotate = true;
        }
        else
        {
            CanRotate = false;
        }
    }

}
