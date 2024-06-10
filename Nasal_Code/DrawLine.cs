using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineMarker;
    private LineRenderer lineColumella;
    private LineRenderer lineLeftNostrilX;
    private LineRenderer lineLeftNostrilY;
    private LineRenderer lineRightNostrilX;
    private LineRenderer lineRightNostrilY;

    public Material material_Marker;
    public Material material_Columella;
    public Material material_L_1;
    public Material material_L_2;
    public Material material_R_1;
    public Material material_R_2;

    private float distanceMarker;
    private float distanceColumella;
    private float distanceLeftNostrilX;
    private float distanceLeftNostrilY;
    private float distanceRightNostrilX;
    private float distanceRightNostrilY;

    private float distanceColumellaCal;
    private float distanceLeftNostrilXCal;
    private float distanceLeftNostrilYCal;
    private float distanceRightNostrilXCal;
    private float distanceRightNostrilYCal;

    private Vector3 startPos;
    private Vector3 mousePos;
    public TMP_Text distanceText;
    public float MarkerUnit;
    

    public Toggle SquareMarker;
    public Toggle Columella;
    public Toggle LeftNostrilX;
    public Toggle RightNostrilX;
    public Toggle LeftNostrilY;
    public Toggle RightNostrilY;

    public TMP_Text TextSquareMarker;
    public TMP_Text TextColumella;
    public TMP_Text TextLeftNostrilX;
    public TMP_Text TextLeftNostrilY;
    public TMP_Text TextRightNostrilX;
    public TMP_Text TextRightNostrilY;

    [SerializeField] private GetRectPositon GetRectPositonScript;

    void Start()
    {
        if(StaticData.Marker03)
        {
            MarkerUnit = 0.3f;
            distanceText.text = "Marker 0.3 cm = " + distanceMarker.ToString("F4") + " Units";
        }
        else if(StaticData.Marker05)
        {
            MarkerUnit = 0.5f;
            distanceText.text = "Marker 0.5 cm = " + distanceMarker.ToString("F4") + " Units";
        }

        distanceMarker = 0.0000f;
        distanceColumella = 0.0000f;
        distanceLeftNostrilX = 0.0000f;
        distanceLeftNostrilY = 0.0000f;
        distanceRightNostrilX = 0.0000f;
        distanceRightNostrilY = 0.0000f;

        TextSquareMarker.text = "Not Found";
        TextSquareMarker.color = new Color32(255,94,64,255);
        TextColumella.text = "Not Found";
        TextColumella.color = new Color32(255, 94, 64, 255);
        TextLeftNostrilX.text = "Not Found";
        TextLeftNostrilX.color = new Color32(255, 94, 64, 255);
        TextLeftNostrilY.text = "Not Found";
        TextLeftNostrilY.color = new Color32(255, 94, 64, 255);
        TextRightNostrilX.text = "Not Found";
        TextRightNostrilX.color = new Color32(255, 94, 64, 255);
        TextRightNostrilY.text = "Not Found";
        TextRightNostrilY.color = new Color32(255, 94, 64, 255);
    }

    void Update()
    {
        if (SquareMarker.isOn)
        {
            CheckDrawLine("SquareMarker");
        }
        
        if(distanceMarker == 0.0000f)
        {
            Columella.interactable = false;
            LeftNostrilX.interactable = false;
            LeftNostrilY.interactable = false;
            RightNostrilX.interactable = false;
            RightNostrilY.interactable= false;
        }
        else
        {
            Columella.interactable = true;
            LeftNostrilX.interactable = true;
            LeftNostrilY.interactable = true;
            RightNostrilX.interactable = true;
            RightNostrilY.interactable = true;

            if (Columella.isOn)
            {
                CheckDrawLine("Columella");
            }
            else if (LeftNostrilX.isOn)
            {
                CheckDrawLine("LeftNostrilX");
            }
            else if (LeftNostrilY.isOn)
            {
                CheckDrawLine("LeftNostrilY");
            }
            else if (RightNostrilX.isOn)
            {
                CheckDrawLine("RightNostrilX");
            }
            else if (RightNostrilY.isOn)
            {
                CheckDrawLine("RightNostrilY");
            }
        }

        
    }

    void createLine(string LineType)
    {
        if(LineType == "SquareMarker")
        {
            lineMarker = new GameObject("Line" + "_SquareMarker").AddComponent<LineRenderer>();
            lineMarker.material = material_Marker;
            lineMarker.positionCount = 2;
            lineMarker.startWidth = 0.1f;
            lineMarker.endWidth = 0.1f;
            lineMarker.useWorldSpace = false;
            lineMarker.numCapVertices = 50;
        }

        if (LineType == "Columella")
        {
            lineColumella = new GameObject("Line" + "_Columella").AddComponent<LineRenderer>();
            lineColumella.material = material_Columella;
            lineColumella.positionCount = 2;
            lineColumella.startWidth = 0.1f;
            lineColumella.endWidth = 0.1f;
            lineColumella.useWorldSpace = false;
            lineColumella.numCapVertices = 50;
        }

        if (LineType == "LeftNostrilX")
        {
            lineLeftNostrilX = new GameObject("Line" + "_LeftNostrilX").AddComponent<LineRenderer>();
            lineLeftNostrilX.material = material_L_1;
            lineLeftNostrilX.positionCount = 2;
            lineLeftNostrilX.startWidth = 0.1f;
            lineLeftNostrilX.endWidth = 0.1f;
            lineLeftNostrilX.useWorldSpace = false;
            lineLeftNostrilX.numCapVertices = 50;
        }

        if (LineType == "RightNostrilX")
        {
            lineRightNostrilX = new GameObject("Line" + "_RightNostrilX").AddComponent<LineRenderer>();
            lineRightNostrilX.material = material_R_1;
            lineRightNostrilX.positionCount = 2;
            lineRightNostrilX.startWidth = 0.1f;
            lineRightNostrilX.endWidth = 0.1f;
            lineRightNostrilX.useWorldSpace = false;
            lineRightNostrilX.numCapVertices = 50;
        }

        if (LineType == "LeftNostrilY")
        {
            lineLeftNostrilY = new GameObject("Line" + "_LeftNostrilY").AddComponent<LineRenderer>();
            lineLeftNostrilY.material = material_L_2;
            lineLeftNostrilY.positionCount = 2;
            lineLeftNostrilY.startWidth = 0.1f;
            lineLeftNostrilY.endWidth = 0.1f;
            lineLeftNostrilY.useWorldSpace = false;
            lineLeftNostrilY.numCapVertices = 50;
        }

        if (LineType == "RightNostrilY")
        {
            lineRightNostrilY = new GameObject("Line" + "_RightNostrilY").AddComponent<LineRenderer>();
            lineRightNostrilY.material = material_R_2;
            lineRightNostrilY.positionCount = 2;
            lineRightNostrilY.startWidth = 0.1f;
            lineRightNostrilY.endWidth = 0.1f;
            lineRightNostrilY.useWorldSpace = false;
            lineRightNostrilY.numCapVertices = 50;
        }

    }

    public void CheckDrawLine(string LineType)
    {
        if(FindObjectOfType<FreezeImage>().Freezing == true)
        {
            if (LineType == "SquareMarker")
            {
                if (StaticData.Marker05)
                {
                    distanceText.text = "Marker 0.5 cm = " + distanceMarker.ToString("F4") + " Units";
                }
                else if (StaticData.Marker03)
                {
                    distanceText.text = "Marker 0.3 cm = " + distanceMarker.ToString("F4") + " Units";
                }

                if (GetRectPositonScript.CheckEnter())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (lineMarker == null)
                        {
                            createLine(LineType);
                        }

                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos.z = 0;
                        lineMarker.SetPosition(0, startPos);
                        lineMarker.SetPosition(1, startPos);

                    }
                    else if (Input.GetMouseButton(0) && lineMarker)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        lineMarker.SetPosition(1, mousePos);
                        distanceMarker = (mousePos - startPos).magnitude;
                        if (StaticData.Marker05)
                        {
                            distanceText.text = "Marker 0.5 cm = " + distanceMarker.ToString("F4") + " Units";
                            TextSquareMarker.text = "0.5 cm = " + distanceMarker.ToString("F4") + " Units";
                        }
                        else if (StaticData.Marker03)
                        {
                            distanceText.text = "Marker 0.3 cm = " + distanceMarker.ToString("F4") + " Units";
                            TextSquareMarker.text = "0.3 cm = " + distanceMarker.ToString("F4") + " Units";
                        }
                    }
                    else if (Input.GetMouseButton(1) && lineMarker)
                    {
                        //lineMarker = null;
                        //SquareMarker.isOn = false;
                    }

                    DistanceCal();

                }
            }

            if (LineType == "Columella")
            {
                distanceText.text = "Columella " + distanceColumellaCal.ToString("F4") + " cm";

                if (GetRectPositonScript.CheckEnter())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (lineColumella == null)
                        {
                            createLine(LineType);
                        }

                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos.z = 0;
                        lineColumella.SetPosition(0, startPos);
                        lineColumella.SetPosition(1, startPos);

                    }
                    else if (Input.GetMouseButton(0) && lineColumella)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        lineColumella.SetPosition(1, mousePos);
                        distanceColumella = (mousePos - startPos).magnitude;

                        DistanceCal();
                        distanceText.text = "Columella " + distanceColumellaCal.ToString("F4") + " cm";
                    }
                }
            }

            if (LineType == "LeftNostrilX")
            {
                distanceText.text = "Left Nostril Width " + distanceLeftNostrilXCal.ToString("F4") + " cm";

                if (GetRectPositonScript.CheckEnter())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (lineLeftNostrilX == null)
                        {
                            createLine(LineType);
                        }

                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos.z = 0;
                        lineLeftNostrilX.SetPosition(0, startPos);
                        lineLeftNostrilX.SetPosition(1, startPos);

                    }
                    else if (Input.GetMouseButton(0) && lineLeftNostrilX)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        lineLeftNostrilX.SetPosition(1, mousePos);
                        distanceLeftNostrilX = (mousePos - startPos).magnitude;

                        DistanceCal();
                        distanceText.text = "Left Nostril Width " + distanceLeftNostrilXCal.ToString("F4") + " cm";
                    }
                }
            }

            if (LineType == "LeftNostrilY")
            {
                distanceText.text = "Left Nostril Height " + distanceLeftNostrilYCal.ToString("F4") + " cm";

                if (GetRectPositonScript.CheckEnter())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (lineLeftNostrilY == null)
                        {
                            createLine(LineType);
                        }

                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos.z = 0;
                        lineLeftNostrilY.SetPosition(0, startPos);
                        lineLeftNostrilY.SetPosition(1, startPos);

                    }
                    else if (Input.GetMouseButton(0) && lineLeftNostrilY)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        lineLeftNostrilY.SetPosition(1, mousePos);
                        distanceLeftNostrilY = (mousePos - startPos).magnitude;

                        DistanceCal();
                        distanceText.text = "Left Nostril Height " + distanceLeftNostrilYCal.ToString("F4") + " cm";
                    }
                }
            }


            if (LineType == "RightNostrilX")
            {
                distanceText.text = "Right Nostril Width " + distanceRightNostrilXCal.ToString("F4") + " cm";

                if (GetRectPositonScript.CheckEnter())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (lineRightNostrilX == null)
                        {
                            createLine(LineType);
                        }

                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos.z = 0;
                        lineRightNostrilX.SetPosition(0, startPos);
                        lineRightNostrilX.SetPosition(1, startPos);

                    }
                    else if (Input.GetMouseButton(0) && lineRightNostrilX)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        lineRightNostrilX.SetPosition(1, mousePos);
                        distanceRightNostrilX = (mousePos - startPos).magnitude;

                        DistanceCal();
                        distanceText.text = "Right Nostril Width " + distanceRightNostrilXCal.ToString("F4") + " cm";
                    }
                }
            }

            if (LineType == "RightNostrilY")
            {
                distanceText.text = "Right Nostril Height " + distanceRightNostrilYCal.ToString("F4") + " cm";

                if (GetRectPositonScript.CheckEnter())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (lineRightNostrilY == null)
                        {
                            createLine(LineType);
                        }

                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos.z = 0;
                        lineRightNostrilY.SetPosition(0, startPos);
                        lineRightNostrilY.SetPosition(1, startPos);

                    }
                    else if (Input.GetMouseButton(0) && lineRightNostrilY)
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        lineRightNostrilY.SetPosition(1, mousePos);
                        distanceRightNostrilY = (mousePos - startPos).magnitude;

                        DistanceCal();
                        distanceText.text = "Right Nostril Height " + distanceRightNostrilYCal.ToString("F4") + " cm";
                    }
                }
            }
        }
    }

    void DistanceCal()
    {
        if(lineMarker)
        {
            TextSquareMarker.color = material_Marker.color;
        }
        if (lineColumella)
        {
            distanceColumellaCal = (distanceColumella * MarkerUnit) / distanceMarker;
            TextColumella.color = material_Columella.color;
            TextColumella.text = distanceColumellaCal.ToString("F4") + " cm";
        }
        if(lineLeftNostrilX)
        {
            distanceLeftNostrilXCal = (distanceLeftNostrilX * MarkerUnit) / distanceMarker;
            TextLeftNostrilX.color = material_L_1.color;
            TextLeftNostrilX.text = distanceLeftNostrilXCal.ToString("F4") + " cm";
        }
        if (lineLeftNostrilY)
        {
            distanceLeftNostrilYCal = (distanceLeftNostrilY * MarkerUnit) / distanceMarker;
            TextLeftNostrilY.color = material_L_2.color;
            TextLeftNostrilY.text = distanceLeftNostrilYCal.ToString("F4") + " cm";
        }
        if (lineRightNostrilX)
        {
            distanceRightNostrilXCal = (distanceRightNostrilX * MarkerUnit) / distanceMarker;
            TextRightNostrilX.color = material_R_1.color;
            TextRightNostrilX.text = distanceRightNostrilXCal.ToString("F4") + " cm";
        }
        if (lineRightNostrilY)
        {
            distanceRightNostrilYCal = (distanceRightNostrilY * MarkerUnit) / distanceMarker;
            TextRightNostrilY.color = material_R_2.color;
            TextRightNostrilY.text = distanceRightNostrilYCal.ToString("F4") + " cm";
        }
    }

    public float GetMarker()
    {    
        return distanceMarker;
    }

    public void ResetDrawLine()
    {
        distanceMarker = 0.0000f;
        distanceColumella = 0.0000f;
        distanceLeftNostrilX = 0.0000f;
        distanceLeftNostrilY = 0.0000f;
        distanceRightNostrilX = 0.0000f;
        distanceRightNostrilY = 0.0000f;

        distanceColumellaCal = 0.0000f;
        distanceLeftNostrilXCal = 0.0000f;
        distanceLeftNostrilYCal = 0.0000f;
        distanceRightNostrilXCal = 0.0000f;
        distanceRightNostrilYCal = 0.0000f;

        TextSquareMarker.text = "Not Found";
        TextSquareMarker.color = new Color32(255, 94, 64, 255);
        TextColumella.text = "Not Found";
        TextColumella.color = new Color32(255, 94, 64, 255);
        TextLeftNostrilX.text = "Not Found";
        TextLeftNostrilX.color = new Color32(255, 94, 64, 255);
        TextLeftNostrilY.text = "Not Found";
        TextLeftNostrilY.color = new Color32(255, 94, 64, 255);
        TextRightNostrilX.text = "Not Found";
        TextRightNostrilX.color = new Color32(255, 94, 64, 255);
        TextRightNostrilY.text = "Not Found";
        TextRightNostrilY.color = new Color32(255, 94, 64, 255);

        CheckDrawLine("SquareMarker");
        CheckDrawLine("Columella");
        CheckDrawLine("LeftNostrilX");
        CheckDrawLine("LeftNostrilY");
        CheckDrawLine("RightNostrilX");
        CheckDrawLine("RightNostrilY");

        if(StaticData.Marker05)
        {
            distanceText.text = "Marker 0.5 cm = " + distanceMarker.ToString("F4") + " Units";
        }
        else if (StaticData.Marker03)
        {
            distanceText.text = "Marker 0.3 cm = " + distanceMarker.ToString("F4") + " Units";
        }
        
    }

}
