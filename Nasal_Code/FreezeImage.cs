using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeImage : MonoBehaviour
{

    [SerializeField] private ScrollRect ScrollImage;
    [SerializeField] private UIZoomImage ZoomImage;
    public GameObject Title_AdjustImage;
    public GameObject Title_DrawImage;
    public bool Freezing;
    public GameObject Text_Distance;
    public GameObject Button_AdjustImage;
    public GameObject Panel_Right;
    public GameObject Button_Next_to_Draw;
    public GameObject Button_Next_to_Reshape;
    public GameObject Button_Back_to_BrowseImage;
    public GameObject Button_Back_to_ZoomImage;

    void Start()
    {
        Freezing = false;
        Text_Distance.SetActive(false);
        Button_AdjustImage.SetActive(true);
        Panel_Right.SetActive(false);

        Button_Next_to_Draw.SetActive(true);
        Button_Next_to_Reshape.SetActive(false);
        Button_Back_to_BrowseImage.SetActive(true);
        Button_Back_to_ZoomImage.SetActive(false);

        ScrollImage = GameObject.Find("ScrollImage").GetComponent<ScrollRect>();
        ZoomImage = GameObject.Find("RawImage").GetComponent<UIZoomImage>();
    }

    public void Freeze()
    {
        ScrollImage.enabled = false;
        ZoomImage.enabled = false;
        Title_AdjustImage.SetActive(false);
        Title_DrawImage.SetActive(true);
        Freezing = true;
        Text_Distance.SetActive(true);
        Button_AdjustImage.SetActive(false);
        Panel_Right.SetActive(true);

        Button_Next_to_Draw.SetActive(false);
        Button_Next_to_Reshape.SetActive(true);

        Button_Back_to_BrowseImage.SetActive(false);
        Button_Back_to_ZoomImage.SetActive(true);
    }

    public void UnFreeze()
    {
        ScrollImage.enabled = true;
        ZoomImage.enabled = true;
        Title_AdjustImage.SetActive(true);
        Title_DrawImage.SetActive(false);
        
        Freezing = false;
        Text_Distance.SetActive(false);
        Button_AdjustImage.SetActive(true);
        Panel_Right.SetActive(false);

        Button_Next_to_Draw.SetActive(true);
        Button_Next_to_Reshape.SetActive(false);
        Button_Back_to_BrowseImage.SetActive(true);
        Button_Back_to_ZoomImage.SetActive(false);

        if(GameObject.Find("Line_SquareMarker"))
        {
            GameObject col = GameObject.Find("Line_SquareMarker");
            Destroy(col);
        }
        if (GameObject.Find("Line_Columella"))
        {
            GameObject col = GameObject.Find("Line_Columella");
            Destroy(col);
        }
        if (GameObject.Find("Line_LeftNostrilX"))
        {
            GameObject col = GameObject.Find("Line_LeftNostrilX");
            Destroy(col);
        }
        if (GameObject.Find("Line_LeftNostrilY"))
        {
            GameObject col = GameObject.Find("Line_LeftNostrilY");
            Destroy(col);
        }
        if (GameObject.Find("Line_RightNostrilX"))
        {
            GameObject col = GameObject.Find("Line_RightNostrilX");
            Destroy(col);
        }
        if (GameObject.Find("Line_RightNostrilY"))
        {
            GameObject col = GameObject.Find("Line_RightNostrilY");
            Destroy(col);
        }

    }
}
