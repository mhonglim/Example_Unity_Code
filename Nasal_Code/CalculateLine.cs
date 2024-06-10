using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
//using UnityEditor.Formats.Fbx.Exporter;
using Parabox.Stl;
using UnityEditor.Experimental.GraphView;
//using UnityEditor.Build.Content;

public class CalculateLine : MonoBehaviour
{
    //Calculate
    private float pixSquare;
    private float pixValue;

    public static float resultCol;
    public static float resultLeftRed;
    public static float resultLeftBlue;
    public static float resultRightRed;
    public static float resultRightBlue;

    private bool scaleSqu3;


    public void CalSquare2()
    {
        if(StaticData.Marker03)
        {
            scaleSqu3 = true;
        }
        else if(StaticData.Marker05)
        {
            scaleSqu3 = false;
        }


        GameObject square = GameObject.Find("Line_SquareMarker");
        LineRenderer comLineSquare = square.GetComponent<LineRenderer>();

        Vector3 startPos = comLineSquare.GetPosition(0);
        Vector3 endPos = comLineSquare.GetPosition(1);

        float xValue = Get_TopLeft_X(startPos.x) - Get_TopLeft_X(endPos.x);
        float yValue = Get_TopLeft_Y(startPos.y) - Get_TopLeft_Y(endPos.y);

        //Manhattan distance
        xValue = Mathf.Abs(xValue);
        yValue = Mathf.Abs(yValue);
        pixSquare = xValue + yValue;

        if (scaleSqu3 == true)
        {
            pixValue = 0.3f / pixSquare;
        }
        else
        {
            pixValue = 0.5f / pixSquare;
        }
        Debug.Log("X:" + xValue + " " + "Y:" + yValue);
        Debug.Log(pixSquare);
        Debug.Log(pixValue);
    }

    public void CalColumella2()
    {
        GameObject col = GameObject.Find("Line_Columella");
        LineRenderer comLineCol = col.GetComponent<LineRenderer>();

        Vector3 startPosCol = comLineCol.GetPosition(0);
        Vector3 endPosCol = comLineCol.GetPosition(1);

        float colXValue = Get_TopLeft_X(startPosCol.x) - Get_TopLeft_X(endPosCol.x);
        float colYValue = Get_TopLeft_Y(startPosCol.y) - Get_TopLeft_Y(endPosCol.y);


        Debug.Log("colXValue: " + colXValue);
        Debug.Log("colYValue: " + colYValue);


        //Manhattan distance
        colXValue = Mathf.Abs(colXValue);
        colYValue = Mathf.Abs(colYValue);
        float sumColValue = colXValue + colYValue;
        Debug.Log("sumColValue: " + sumColValue);
        resultCol = sumColValue * pixValue;
        Debug.Log("resultCol: " + resultCol);
    }

    public void CalNostrilLeft()
    {
        GameObject lineX = GameObject.Find("Line_LeftNostrilX");
        GameObject lineY = GameObject.Find("Line_LeftNostrilY");

        LineRenderer comLineX = lineX.GetComponent<LineRenderer>();
        LineRenderer comLineY = lineY.GetComponent<LineRenderer>();

        Vector3 startPosX = comLineX.GetPosition(0);
        Vector3 endPosX = comLineX.GetPosition(1);

        Vector3 startPosY = comLineY.GetPosition(0);
        Vector3 endPosY = comLineY.GetPosition(1);

        float xValue =  Get_TopLeft_X(startPosX.x) - Get_TopLeft_X(endPosX.x);
        float xValue2 = Get_TopLeft_Y(startPosX.y) - Get_TopLeft_Y(endPosX.y);

        float yValue = Get_TopLeft_X(startPosY.x) - Get_TopLeft_X(endPosY.x);
        float yValue2 = Get_TopLeft_Y(startPosY.y) - Get_TopLeft_Y(endPosY.y);

        //Manhattan distance
        xValue = Mathf.Abs(xValue);
        xValue2 = Mathf.Abs(xValue2);
        yValue = Mathf.Abs(yValue);
        yValue2 = Mathf.Abs(yValue2);
        float sumXValue = xValue + xValue2;
        float sumYValue = yValue + yValue2;

        //Old axis
        //resultLeftRed = sumXValue * pixValue;
        //resultLeftBlue = sumYValue * pixValue;

        //SwapXY axis for Manual Reshape
        resultLeftBlue = sumXValue * pixValue;
        resultLeftRed = sumYValue * pixValue;

    }

    public void CalNostrilRight()
    {
        GameObject lineX = GameObject.Find("Line_RightNostrilX");
        GameObject lineY = GameObject.Find("Line_RightNostrilY");

        LineRenderer comLineX = lineX.GetComponent<LineRenderer>();
        LineRenderer comLineY = lineY.GetComponent<LineRenderer>();

        Vector3 startPosX = comLineX.GetPosition(0);
        Vector3 endPosX = comLineX.GetPosition(1);

        Vector3 startPosY = comLineY.GetPosition(0);
        Vector3 endPosY = comLineY.GetPosition(1);

        float xValue = Get_TopLeft_X(startPosX.x) - Get_TopLeft_X(endPosX.x);
        float xValue2 = Get_TopLeft_Y(startPosX.y) - Get_TopLeft_Y(endPosX.y);

        float yValue = Get_TopLeft_X(startPosY.x) - Get_TopLeft_X(endPosY.x);
        float yValue2 = Get_TopLeft_Y(startPosY.y) - Get_TopLeft_Y(endPosY.y);

        //Manhattan distance
        xValue = Mathf.Abs(xValue);
        xValue2 = Mathf.Abs(xValue2);
        yValue = Mathf.Abs(yValue);
        yValue2 = Mathf.Abs(yValue2);
        float sumXValue = xValue + xValue2;
        float sumYValue = yValue + yValue2;

        //SwapXY for Manual Reshape
        //resultRightRed = sumXValue * pixValue;
        //resultRightBlue = sumYValue * pixValue;

        resultRightBlue = sumXValue * pixValue;
        resultRightRed = sumYValue * pixValue;
    }


    //Convert Line Point Unit to Pixel Unit and then Convert Cartesian coordinate to Top_Left coordinate
    private float Get_TopLeft_X(float Line_Point)
    {

        float X_Cartesian; 
        X_Cartesian = Line_Point * (36 * (3.026816f / StaticData.Scale_RawImage));

        float X_TopLeft;
        X_TopLeft = (StaticData.ImageWidthToKeep / 2) + X_Cartesian;
        Debug.Log("X_TopLeft:" + X_TopLeft);
        return X_TopLeft;

    }

    private float Get_TopLeft_Y(float Line_Point)
    {

        float Y_Cartesian;
        Y_Cartesian = Line_Point * (36 * (3.026816f / StaticData.Scale_RawImage));

        float Y_TopLeft;
        Y_TopLeft = (StaticData.ImageHeightToKeep / 2) - Y_Cartesian;
        Debug.Log("Y_TopLeft:" + Y_TopLeft);
        return Y_TopLeft;
    }

}
