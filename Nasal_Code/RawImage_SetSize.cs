using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImage_SetSize : MonoBehaviour
{
    public GameObject RawImageObject;
    RectTransform myRect;
    RawImage myRawImage;
    float myWidth;
    float myHeight;
    float myScale;

    // Start is called before the first frame update
    void Start()
    {
        myRawImage = RawImageObject.GetComponent<RawImage>();
        myRect = RawImageObject.GetComponent<RectTransform>();

        //myWidth = myRawImage.mainTexture.width;
        //myHeight = myRawImage.mainTexture.height;
        myWidth = StaticData.ImageWidthToKeep;
        myHeight = StaticData.ImageHeightToKeep;
        Debug.Log("Width:" + myWidth + "Height:" + myHeight);

        if (myWidth == 0 || myHeight == 0)
        {
            //mockup size for individual scene testing
            myWidth = 1500f;
            myHeight = 2000f;
            Debug.Log("Mockup Size Activated");
        }
        myRawImage.rectTransform.sizeDelta = new Vector2(myWidth, myHeight);
    }

    public void Get_RawImage_Scale()
    {
        myScale = myRawImage.rectTransform.localScale.x;
        StaticData.Scale_RawImage = myScale;
        Debug.Log("myScale:" + StaticData.Scale_RawImage);
    }
}
