using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public GameObject MyObject;
    public float ScaleSpeed = 0.01f;

    private bool ZoomIn;
    private bool ZoomOut;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ZoomIn)
        {
            MyObject.transform.localScale += new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);
        }

        if(ZoomOut)
        {
            MyObject.transform.localScale -= new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);
        }

    }

    public void OnPressZoomIn()
    {
        ZoomIn = true;
    }

    public void OnReleaseZoomIn()
    {
        ZoomIn = false;
    }
    public void OnPressZoomOut()
    {
        ZoomOut = true;
    }

    public void OnReleaseZoomOut()
    {
        ZoomOut = false;
    }

}
