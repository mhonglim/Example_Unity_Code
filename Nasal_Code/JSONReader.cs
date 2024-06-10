using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
//using UnityEditor.Formats.Fbx.Exporter;
using Parabox.Stl;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;
using UnityEngine.UI;
//using UnityEditor.Build.Content;

public class JSONReader : MonoBehaviour
{
    //JSON
    public TextAsset JSONText;

    //Calculate
    private NasalSplintList myNasalSplintList = new NasalSplintList();
    public NasalSplint nal = new NasalSplint();

    private float pixSquare;
    private float pixValue;
    public float resultCol;
    public float resultLeftRed;
    public float resultLeftBlue;
    public float resultRightRed;
    public float resultRightBlue;
    private bool scaleSqu3 = false;

    //Reshape
    public GameObject LModel;
    public GameObject LModel2;
    public GameObject SModel;
    public GameObject SModel2;

    public GameObject MModel;

    private float unityUnit = 1.0f;
    private bool checkSelectBoth = false; //Default = false
    private float yForCol1 = 0;
    private float yForCol2 = 0;
    public string NostrilReshapedStatus;
    public string ColumellaReshapedStatus;
    private float YScaleFactor = 1.1f;
    private float CScaleFactor = 3.0f;

    //Export
    public string ExportStatus;

    //Tag
    public bool Cal_Tag_Start = false;
    public bool Reshape_Tag_Start = false;
    public bool Reshape_Tag_End = false;
    public bool Export_Tag_Start = false;
    public bool Export_Tag_End = false;
    public bool Fix_Tag_Start = false;


    //JSON
    [System.Serializable]
    public class NasalSplint
    {
        public bool FoundMarker;
        public float MarkerX1;
        public float MarkerY1;
        public float MarkerX2;
        public float MarkerY2;

        public bool FoundColumella;
        public float ColumellaX1;
        public float ColumellaY1;
        public float ColumellaX2;
        public float ColumellaY2;

        public bool FoundNostrilRight;
        public float NostrilRightRedX1;
        public float NostrilRightRedY1;
        public float NostrilRightRedX2;
        public float NostrilRightRedY2;
        public float NostrilRightBlueX1;
        public float NostrilRightBlueY1;
        public float NostrilRightBlueX2;
        public float NostrilRightBlueY2;

        public bool FoundNostrilLeft;
        public float NostrilLeftRedX1;
        public float NostrilLeftRedY1;
        public float NostrilLeftRedX2;
        public float NostrilLeftRedY2;
        public float NostrilLeftBlueX1;
        public float NostrilLeftBlueY1;
        public float NostrilLeftBlueX2;
        public float NostrilLeftBlueY2;
    }

    [System.Serializable]
    public class NasalSplintList
    {
        public NasalSplint[] NS;
    }

    public void JSON_Start()
    {
        //Start
        //Check marker size from user
        scaleSqu3 = StaticData.Marker03;

        RefreshJSON();

        myNasalSplintList = JsonUtility.FromJson<NasalSplintList>(JSONText.text);
        for (int i = 0; i < myNasalSplintList.NS.Length; i++)
        {
            nal = myNasalSplintList.NS[i];
        }

        StartCoroutine(LateStart1(1));
    }

    IEnumerator LateStart1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CalSquare2();
        CalColumella2();
        CalNostrilLeft();
        CalNostrilRight();
        StartCoroutine(LateStart2(1));
    }

    IEnumerator LateStart2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ReShapeColumella();
        StartCoroutine(LateStart3(5));
    }

    IEnumerator LateStart3(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ReShapeNostril();
        StartCoroutine(LateStart4(10));
    }
    IEnumerator LateStart4(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FixBugModel2();
        StartCoroutine(LateStart5(2));
    }

    IEnumerator LateStart5(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Export3DModel();
    }

    //Calculate
    public void CalSquare2()
    {
        Cal_Tag_Start = true;

        float xValue = nal.MarkerX1 - nal.MarkerX2;
        float yValue = nal.MarkerY1 - nal.MarkerY2;

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
        float colXValue = nal.ColumellaX1 - nal.ColumellaX2;
        float colYValue = nal.ColumellaY1 - nal.ColumellaY2;
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
        float xValue = nal.NostrilLeftRedX1 - nal.NostrilLeftRedX2;
        float xValue2 = nal.NostrilLeftRedY1 - nal.NostrilLeftRedY2;

        float yValue = nal.NostrilLeftBlueX1 - nal.NostrilLeftBlueX2;
        float yValue2 = nal.NostrilLeftBlueY1 - nal.NostrilLeftBlueY2;

        //Manhattan distance
        xValue = Mathf.Abs(xValue);
        xValue2 = Mathf.Abs(xValue2);
        yValue = Mathf.Abs(yValue);
        yValue2 = Mathf.Abs(yValue2);
        float sumXValue = xValue + xValue2;
        float sumYValue = yValue + yValue2;

        resultLeftRed = sumXValue * pixValue;
        resultLeftBlue = sumYValue * pixValue;

    }

    public void CalNostrilRight()
    {
        float xValue = nal.NostrilRightRedX1 - nal.NostrilRightRedX2;
        float xValue2 = nal.NostrilRightRedY1 - nal.NostrilRightRedY2;

        float yValue = nal.NostrilRightBlueX1 - nal.NostrilRightBlueX2;
        float yValue2 = nal.NostrilRightBlueY1 - nal.NostrilRightBlueY2;

        //Manhattan distance
        xValue = Mathf.Abs(xValue);
        xValue2 = Mathf.Abs(xValue2);
        yValue = Mathf.Abs(yValue);
        yValue2 = Mathf.Abs(yValue2);
        float sumXValue = xValue + xValue2;
        float sumYValue = yValue + yValue2;

        resultRightRed = sumXValue * pixValue;
        resultRightBlue = sumYValue * pixValue;

    }

    //Reshape Right = Model.transform.GetChild(1) / Left = Model.transform.GetChild(0)
    public void ReShapeNostril()
    {
        if(StaticData.Marker05)
        {
            //L model Original
            if (LModel.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure_R = resultRightRed;
                //Doctor said Y = Y * 1.2
                float scaleX_R = (unityUnit / 1.7f) * xMeasure_R * YScaleFactor;
                //float scaleX_R = (unityUnit / 1.0f) * xMeasure;
                float yMeasure_R = resultRightBlue;
                if (checkSelectBoth == true)
                    yForCol2 = yMeasure_R;
                else
                    yForCol1 = yMeasure_R;
                float scaleY_R = (unityUnit / 1.1f) * yMeasure_R;
                //Doctor said X = (Y + X)/2
                scaleY_R = (scaleY_R + scaleX_R) / 2;
                //float scaleY = (unityUnit / 1.0f) * yMeasure;
                Transform Rside = LModel.transform.GetChild(1);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped";
            }

            if (LModel.transform.GetChild(0).gameObject.activeSelf)
            {
                float xMeasure_L = resultLeftRed;
                //Doctor said Y = Y * 1.2
                float scaleX_L = (unityUnit / 1.7f) * xMeasure_L * YScaleFactor;
                //float scaleX_L = (unityUnit / 1.0f) * xMeasure;
                float yMeasure_L = resultLeftBlue;
                yForCol2 = yMeasure_L;
                float scaleY_L = (unityUnit / 1.1f) * yMeasure_L;
                //Doctor said X = (Y + X)/2
                scaleY_L = (scaleY_L + scaleX_L) / 2;
                //float scaleY = (unityUnit / 1.0f) * yMeasure;

                Transform Lside = LModel.transform.GetChild(0);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped";
            }

            //Both as Right
            //if (LModel.transform.GetChild(0).gameObject.activeSelf && LModel.transform.GetChild(1).gameObject.activeSelf)
            //{
            //    float xMeasure = resultRightRed;
            //    //Doctor said Y = Y * 1.2
            //    float scaleX = (unityUnit / 1.7f) * xMeasure * YScaleFactor;
            //    //float scaleX = (unityUnit / 1.0f) * xMeasure;
            //    float yMeasure = resultRightBlue;
            //    yForCol1 = yMeasure;
            //    checkSelectBoth = true;
            //    float scaleY = (unityUnit / 1.1f) * yMeasure;
            //    //Doctor said X = (Y + X)/2
            //    scaleY = (scaleY + scaleX) / 2;
            //    //float scaleY = (unityUnit / 1.0f) * yMeasure;

            //    Transform Rside = LModel.transform.GetChild(0);
            //    Transform Lside = LModel.transform.GetChild(1);
            //    Rside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);
            //    Lside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);

            //    NostrilReshapedStatus = "Both sides are reshaped";
            //}
            //--------------------------------------------------------------
            //LModel2 = L Model Carved
            if (LModel2.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure_R = resultRightRed;
                //Doctor said Y = Y * 1.2
                float scaleX_R = (unityUnit / 1.7f) * xMeasure_R * YScaleFactor;
                //float scaleX_R = (unityUnit / 1.0f) * xMeasure;
                float yMeasure_R = resultRightBlue;
                if (checkSelectBoth == true)
                    yForCol2 = yMeasure_R;
                else
                    yForCol1 = yMeasure_R;
                float scaleY_R = (unityUnit / 1.1f) * yMeasure_R;
                //Doctor said X = (Y + X)/2
                scaleY_R = (scaleY_R + scaleX_R) / 2;
                //float scaleY = (unityUnit / 1.0f) * yMeasure;
                Transform Rside = LModel2.transform.GetChild(1);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped 2";
            }

            if (LModel2.transform.GetChild(0).gameObject.activeSelf)
            {
                float xMeasure_L = resultLeftRed;
                //Doctor said Y = Y * 1.2
                float scaleX_L = (unityUnit / 1.7f) * xMeasure_L * YScaleFactor;
                //float scaleX_L = (unityUnit / 1.0f) * xMeasure;
                float yMeasure_L = resultLeftBlue;
                yForCol2 = yMeasure_L;
                float scaleY_L = (unityUnit / 1.1f) * yMeasure_L;
                //Doctor said X = (Y + X)/2
                scaleY_L = (scaleY_L + scaleX_L) / 2;
                //float scaleY = (unityUnit / 1.0f) * yMeasure;

                Transform Lside = LModel2.transform.GetChild(0);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped 2";
            }

            /*        if (LModel2.transform.GetChild(0).gameObject.activeSelf && LModel2.transform.GetChild(1).gameObject.activeSelf)
                    {
                        float xMeasure = resultRightRed;
                        //Doctor said Y = Y * 1.2
                        float scaleX = (unityUnit / 1.7f) * xMeasure * YScaleFactor;
                        //float scaleX = (unityUnit / 1.0f) * xMeasure;
                        float yMeasure = resultRightBlue;
                        yForCol1 = yMeasure;
                        checkSelectBoth = true;
                        float scaleY = (unityUnit / 1.1f) * yMeasure;
                        //Doctor said X = (Y + X)/2
                        scaleY = (scaleY + scaleX) / 2;
                        //float scaleY = (unityUnit / 1.0f) * yMeasure;

                        Transform Rside = LModel2.transform.GetChild(0);
                        Transform Lside = LModel2.transform.GetChild(1);
                        Rside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);
                        Lside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);

                        NostrilReshapedStatus = "Both sides are reshaped 2";
                    }*/
        }

        ///////////////////////////////////////////////////////////////////////////////////////////// M Model /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*if (MModel.transform.GetChild(0).gameObject.activeSelf)
        {
            float xMeasure = resultRightRed;
            //Doctor said Y = Y * 1.2
            float scaleX = (unityUnit / 1.0f) * xMeasure * YScaleFactor;

            float yMeasure = resultRightBlue;
            if (checkSelectBoth == true)
                yForCol2 = yMeasure;
            else
                yForCol1 = yMeasure;
            float scaleY = (unityUnit / 0.7f) * yMeasure;
            //Doctor said X = (Y + X)/2
            scaleY = (scaleY + scaleX) / 2;

            Transform Rside = MModel.transform.GetChild(0);
            Rside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

            NostrilReshapedStatus = "Right side is reshaped";
        }

        if (MModel.transform.GetChild(1).gameObject.activeSelf)
        {
            float xMeasure = resultLeftRed;
            //Doctor said Y = Y * 1.2
            float scaleX = (unityUnit / 1.0f) * xMeasure * YScaleFactor;
            float yMeasure = resultLeftBlue;
            yForCol2 = yMeasure;
            float scaleY = (unityUnit / 0.7f) * yMeasure;
            //Doctor said X = (Y + X)/2
            scaleY = (scaleY + scaleX) / 2;

            Transform Lside = MModel.transform.GetChild(1);
            Lside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);

            NostrilReshapedStatus = "Left side is reshaped";
        }

        if (MModel.transform.GetChild(0).gameObject.activeSelf && MModel.transform.GetChild(1).gameObject.activeSelf)
        {
            float xMeasure = resultRightRed;
            //Doctor said Y = Y * 1.2
            float scaleX = (unityUnit / 1.0f) * xMeasure * YScaleFactor;
            float yMeasure = resultRightBlue;
            yForCol1 = yMeasure;
            checkSelectBoth = true;
            float scaleY = (unityUnit / 0.7f) * yMeasure;
            //Doctor said X = (Y + X)/2
            scaleY = (scaleY + scaleX) / 2;

            Transform Rside = MModel.transform.GetChild(0);
            Transform Lside = MModel.transform.GetChild(1);
            Rside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);
            Lside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);

            NostrilReshapedStatus = "Both sides are reshaped";
        }*/

        else if(StaticData.Marker03)
        {
            //S model Original
            if (SModel.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure_R = resultRightRed;
                //Doctor said Y = Y * 1.2
                float scaleX_R = (unityUnit / 0.85f) * xMeasure_R * YScaleFactor;
                float yMeasure_R = resultRightBlue;
                if (checkSelectBoth == true)
                    yForCol2 = yMeasure_R;
                else
                    yForCol1 = yMeasure_R;
                float scaleY_R = (unityUnit / 0.67f) * yMeasure_R;
                //Doctor said X = (Y + X)/2
                scaleY_R = (scaleY_R + scaleX_R) / 2;

                Transform Rside = SModel.transform.GetChild(1);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped";
            }

            if (SModel.transform.GetChild(0).gameObject.activeSelf)
            {
                float xMeasure_L = resultLeftRed;
                //Doctor said Y = Y * 1.2
                float scaleX_L = (unityUnit / 0.85f) * xMeasure_L * YScaleFactor;
                float yMeasure_L = resultLeftBlue;
                yForCol2 = yMeasure_L;
                float scaleY_L = (unityUnit / 0.67f) * yMeasure_L;
                //Doctor said X = (Y + X)/2
                scaleY_L = (scaleY_L + scaleX_L) / 2;

                Transform Lside = SModel.transform.GetChild(0);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped";
            }

            /*if (SModel.transform.GetChild(0).gameObject.activeSelf && SModel.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure = resultRightRed;
                //Doctor said Y = Y * 1.2
                float scaleX = (unityUnit / 0.85f) * xMeasure * YScaleFactor;
                float yMeasure = resultRightBlue;
                yForCol1 = yMeasure;
                checkSelectBoth = true;
                float scaleY = (unityUnit / 0.67f) * yMeasure;
                //Doctor said X = (Y + X)/2
                scaleY = (scaleY + scaleX) / 2;

                Transform Rside = SModel.transform.GetChild(0);
                Transform Lside = SModel.transform.GetChild(1);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX, 1, scaleY);

                NostrilReshapedStatus = "Both sides are reshaped";
            }*/

            //S model Carved
            if (SModel2.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure_R = resultRightRed;
                //Doctor said Y = Y * 1.2
                float scaleX_R = (unityUnit / 0.85f) * xMeasure_R * YScaleFactor;
                float yMeasure_R = resultRightBlue;
                if (checkSelectBoth == true)
                    yForCol2 = yMeasure_R;
                else
                    yForCol1 = yMeasure_R;
                float scaleY_R = (unityUnit / 0.67f) * yMeasure_R;
                //Doctor said X = (Y + X)/2
                scaleY_R = (scaleY_R + scaleX_R) / 2;

                Transform Rside = SModel2.transform.GetChild(1);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped 2";
            }
            if (SModel2.transform.GetChild(0).gameObject.activeSelf)
            {
                float xMeasure_L = resultLeftRed;
                //Doctor said Y = Y * 1.2
                float scaleX_L = (unityUnit / 0.85f) * xMeasure_L * YScaleFactor;
                float yMeasure_L = resultLeftBlue;
                yForCol2 = yMeasure_L;
                float scaleY_L = (unityUnit / 0.67f) * yMeasure_L;
                //Doctor said X = (Y + X)/2
                scaleY_L = (scaleY_L + scaleX_L) / 2;

                Transform Lside = SModel2.transform.GetChild(0);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped 2";
            }

        }
        Selection.activeGameObject = GameObject.Find("Main Camera");
    }


    public void ReShapeColumella()
    {
        Reshape_Tag_Start = true;

        if (StaticData.Marker05)
        {
            if (LModel.activeSelf)
            {
                // always new run time
                LModel.transform.GetChild(0).gameObject.SetActive(true);
                LModel.transform.GetChild(1).gameObject.SetActive(true);
                LModel.transform.GetChild(2).gameObject.SetActive(true);

                float colMeasure = resultCol;
                // Doctor said C = C * 1.3
                float scaleCol = (unityUnit / 0.55f) * colMeasure * CScaleFactor;
                Debug.Log("scaleCol: " + scaleCol);
                float RsideMove = 0.0f;
                float LsideMove = 0.0f;
                scaleCol = scaleCol / 2;
                if (checkSelectBoth == true)
                {
                    if (yForCol1 < 0.75f || (yForCol1 + yForCol2) / 2 < 0.75f)
                    {
                        RsideMove = 7 + scaleCol; // 7
                        LsideMove = (-7) - scaleCol; // -7
                    }
                }
                else
                {
                    if ((yForCol1 + yForCol2) / 2 < 0.75f)
                    {
                        RsideMove = 7 + scaleCol; // 7
                        LsideMove = (-7) - scaleCol; // -7
                    }
                    else
                    {
                        RsideMove = 8 + scaleCol; // 8
                        LsideMove = (-8) - scaleCol; // -8
                    }
                }
                Transform Rside = LModel.transform.GetChild(0);
                Transform Lside = LModel.transform.GetChild(1);
                //Debug.Log(RsideMove);
                //Debug.Log(LsideMove);
                Rside.transform.localPosition = new UnityEngine.Vector3(0, -2, RsideMove);
                Lside.transform.localPosition = new UnityEngine.Vector3(0, -2, LsideMove);

                //using UnityEditor
                //Selection.activeGameObject = GameObject.Find("Noseplug0.05");
                Selection.activeGameObject = GameObject.Find(LModel.name);

                Debug.Log("Large Model Name " + LModel.name);
                ColumellaReshapedStatus = "Columella is reshaped";
            }

            //LModel2
            if (LModel2.activeSelf)
            {
                // always new run time
                LModel2.transform.GetChild(0).gameObject.SetActive(true);
                LModel2.transform.GetChild(1).gameObject.SetActive(true);
                LModel2.transform.GetChild(2).gameObject.SetActive(true);

                float colMeasure = resultCol;
                // Doctor said C = C * 1.3
                float scaleCol = (unityUnit / 0.55f) * colMeasure * CScaleFactor;
                Debug.Log("scaleCol: " + scaleCol);
                float RsideMove = 0.0f;
                float LsideMove = 0.0f;
                scaleCol = scaleCol / 2;
                if (checkSelectBoth == true)
                {
                    if (yForCol1 < 0.75f || (yForCol1 + yForCol2) / 2 < 0.75f)
                    {
                        RsideMove = 7 + scaleCol; // 7
                        LsideMove = (-7) - scaleCol; // -7
                    }
                }
                else
                {
                    if ((yForCol1 + yForCol2) / 2 < 0.75f)
                    {
                        RsideMove = 7 + scaleCol; // 7
                        LsideMove = (-7) - scaleCol; // -7
                    }
                    else
                    {
                        RsideMove = 8 + scaleCol; // 8
                        LsideMove = (-8) - scaleCol; // -8
                    }
                }
                Transform Rside = LModel2.transform.GetChild(0);
                Transform Lside = LModel2.transform.GetChild(1);
                //Debug.Log(RsideMove);
                //Debug.Log(LsideMove);
                Rside.transform.localPosition = new UnityEngine.Vector3(0, -2, RsideMove);
                Lside.transform.localPosition = new UnityEngine.Vector3(0, -2, LsideMove);

                //using UnityEditor
                //Selection.activeGameObject = GameObject.Find("Noseplug0.05");
                Selection.activeGameObject = GameObject.Find(LModel2.name);

                Debug.Log("Large Model Name " + LModel2.name);
                ColumellaReshapedStatus = "Columella is reshaped 2";
            }
        }




        /*if (MModel.activeSelf)
        {
            // always new run time
            MModel.transform.GetChild(0).gameObject.SetActive(true);
            MModel.transform.GetChild(1).gameObject.SetActive(true);
            MModel.transform.GetChild(2).gameObject.SetActive(true);

            float colMeasure = resultCol;
            float scaleCol = (unityUnit / 0.47f) * colMeasure * CScaleFactor;
            float RsideMove = 0.0f;
            float LsideMove = 0.0f;
            scaleCol = scaleCol / 2;
            if (checkSelectBoth == true)
            {
                if (yForCol1 < 0.35f || (yForCol1 + yForCol2) / 2 < 0.35f)
                {
                    RsideMove = 5 + scaleCol;
                    LsideMove = (-5) - scaleCol;
                }
            }
            else
            {
                if ((yForCol1 + yForCol2) / 2 < 0.35f)
                {
                    RsideMove = 5 + scaleCol;
                    LsideMove = (-5) - scaleCol;
                }
                else
                {
                    RsideMove = 6 + scaleCol;
                    LsideMove = (-6) - scaleCol;
                }
            }
            Transform Rside = MModel.transform.GetChild(0);
            Transform Lside = MModel.transform.GetChild(1);
            //Debug.Log(RsideMove);
            //Debug.Log(LsideMove);
            Rside.transform.localPosition = new UnityEngine.Vector3(0, -2, RsideMove);
            Lside.transform.localPosition = new UnityEngine.Vector3(0, -2, LsideMove);

            //using UnityEditor
            //Selection.activeGameObject = GameObject.Find("Noseplug_Edited_M");
            Selection.activeGameObject = GameObject.Find("Nose_V3_Mirror_M");

            ColumellaReshapedStatus = "Columella is reshaped";
        }*/

        else if (StaticData.Marker03)
        {
            if (SModel.activeSelf)
            {
                // always new run time
                SModel.transform.GetChild(0).gameObject.SetActive(true);
                SModel.transform.GetChild(1).gameObject.SetActive(true);
                SModel.transform.GetChild(2).gameObject.SetActive(true);

                float colMeasure = resultCol;
                float scaleCol = (unityUnit / 0.4f) * colMeasure * CScaleFactor;
                float RsideMove = 0.0f;
                float LsideMove = 0.0f;
                scaleCol = scaleCol / 2;
                if (checkSelectBoth == true)
                {
                    if (yForCol1 < 0.32f || (yForCol1 + yForCol2) / 2 < 0.32f)
                    {
                        RsideMove = 4.5f + scaleCol;
                        LsideMove = (-4.5f) - scaleCol;
                    }
                }
                else
                {
                    if ((yForCol1 + yForCol2) / 2 < 0.32f)
                    {
                        RsideMove = 4.5f + scaleCol;
                        LsideMove = (-4.5f) - scaleCol;
                    }
                    else
                    {
                        RsideMove = 5 + scaleCol;
                        LsideMove = (-5) - scaleCol;
                    }
                }
                Transform Rside = SModel.transform.GetChild(0);
                Transform Lside = SModel.transform.GetChild(1);
                //Debug.Log(RsideMove);
                //Debug.Log(LsideMove);
                Rside.transform.localPosition = new UnityEngine.Vector3(0, -2, RsideMove);
                Lside.transform.localPosition = new UnityEngine.Vector3(0, -2, LsideMove);

                //using UnityEditor
                //Selection.activeGameObject = GameObject.Find("Noseplug_Edited_S");
                Selection.activeGameObject = GameObject.Find(SModel.name);
                Debug.Log("Small Model Name " + SModel.name);
                ColumellaReshapedStatus = "Columella is reshaped";
            }

            //S Model Carved
            if (SModel2.activeSelf)
            {
                // always new run time
                SModel2.transform.GetChild(0).gameObject.SetActive(true);
                SModel2.transform.GetChild(1).gameObject.SetActive(true);
                SModel2.transform.GetChild(2).gameObject.SetActive(true);

                float colMeasure = resultCol;
                float scaleCol = (unityUnit / 0.4f) * colMeasure * CScaleFactor;
                float RsideMove = 0.0f;
                float LsideMove = 0.0f;
                scaleCol = scaleCol / 2;
                if (checkSelectBoth == true)
                {
                    if (yForCol1 < 0.32f || (yForCol1 + yForCol2) / 2 < 0.32f)
                    {
                        RsideMove = 4.5f + scaleCol;
                        LsideMove = (-4.5f) - scaleCol;
                    }
                }
                else
                {
                    if ((yForCol1 + yForCol2) / 2 < 0.32f)
                    {
                        RsideMove = 4.5f + scaleCol;
                        LsideMove = (-4.5f) - scaleCol;
                    }
                    else
                    {
                        RsideMove = 5 + scaleCol;
                        LsideMove = (-5) - scaleCol;
                    }
                }
                Transform Rside = SModel2.transform.GetChild(0);
                Transform Lside = SModel2.transform.GetChild(1);
                //Debug.Log(RsideMove);
                //Debug.Log(LsideMove);
                Rside.transform.localPosition = new UnityEngine.Vector3(0, -2, RsideMove);
                Lside.transform.localPosition = new UnityEngine.Vector3(0, -2, LsideMove);

                //using UnityEditor
                //Selection.activeGameObject = GameObject.Find("Noseplug_Edited_S");
                Selection.activeGameObject = GameObject.Find(SModel2.name);
                Debug.Log("Small Carved Model Name " + SModel2.name);
                ColumellaReshapedStatus = "Columella is reshaped 2";
            }
        }
        Reshape_Tag_End = true;
    }

    //Export
    public void Export3DModel()
    {
        Export_Tag_Start = true;
        string path = System.IO.Directory.GetParent(".").FullName + "/Result/";

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        if(StaticData.Marker05)
        {
            //LModel1
            if (LModel.activeSelf)
            {
                string File_Model1 = Path.Combine(path, "Export1" + ".stl");

                LModel.transform.GetChild(0).gameObject.SetActive(true);
                LModel.transform.GetChild(1).gameObject.SetActive(true);
                LModel.transform.GetChild(2).gameObject.SetActive(true);
                Mesh mesh1 = LModel.GetComponent<MeshFilter>().mesh;
                Exporter.WriteFile(File_Model1, mesh1, FileType.Ascii);

                ExportStatus = "Export Large Complete 1";
                Debug.Log("Export Large Complete 1");
            }

            //LModel2
            if (LModel2.activeSelf)
            {
                string File_Model2 = Path.Combine(path, "Export2" + ".stl");

                LModel2.transform.GetChild(0).gameObject.SetActive(true);
                LModel2.transform.GetChild(1).gameObject.SetActive(true);
                LModel2.transform.GetChild(2).gameObject.SetActive(true);
                Mesh mesh2 = LModel2.GetComponent<MeshFilter>().mesh;
                Exporter.WriteFile(File_Model2, mesh2, FileType.Ascii);

                ExportStatus = "Export Large Complete 2";
                Debug.Log("Export Large Complete 2");
            }
        }


        /*if (MModel.activeSelf)
        {
            MModel.transform.GetChild(0).gameObject.SetActive(true);
            MModel.transform.GetChild(1).gameObject.SetActive(true);
            MModel.transform.GetChild(2).gameObject.SetActive(true);
            Mesh mesh = MModel.GetComponent<MeshFilter>().mesh;
            Exporter.WriteFile(path, mesh, FileType.Ascii);

            ExportStatus = "Export Medium Complete";
            Debug.Log("Export Medium Complete");
        }*/

        else if (StaticData.Marker03)
        {
            //S Model Original
            if (SModel.activeSelf)
            {
                string File_Model1 = Path.Combine(path, "Export1" + ".stl");

                SModel.transform.GetChild(0).gameObject.SetActive(true);
                SModel.transform.GetChild(1).gameObject.SetActive(true);
                SModel.transform.GetChild(2).gameObject.SetActive(true);
                Mesh mesh1 = SModel.GetComponent<MeshFilter>().mesh;
                Exporter.WriteFile(File_Model1, mesh1, FileType.Ascii);

                ExportStatus = "Export Small Complete 1";
                Debug.Log("Export Small Complete 1");
            }
            //S Model Carved
            if (SModel2.activeSelf)
            {
                string File_Model2 = Path.Combine(path, "Export2" + ".stl");

                SModel2.transform.GetChild(0).gameObject.SetActive(true);
                SModel2.transform.GetChild(1).gameObject.SetActive(true);
                SModel2.transform.GetChild(2).gameObject.SetActive(true);
                Mesh mesh2 = SModel2.GetComponent<MeshFilter>().mesh;
                Exporter.WriteFile(File_Model2, mesh2, FileType.Ascii);

                ExportStatus = "Export Small Complete 2";
                Debug.Log("Export Small Complete 2");
            }
        }
        Export_Tag_End = true;
    }

    public void RefreshJSON()
    {
        AssetDatabase.Refresh();
        Debug.Log("Refreshed Database");
    }

    public void FixBugModel2()
    {
        Fix_Tag_Start = true;

        if(StaticData.Marker05)
        {
            Selection.activeGameObject = GameObject.Find(LModel.name);
        }
        else if(StaticData.Marker03)
        {
            Selection.activeGameObject = GameObject.Find(SModel.name);
        }

    }


    /*
    public void SaveFileBrowser()
    {
        var bp = new AnotherFileBrowser.Windows.BrowserProperties();
        bp.filter = "3D Model file (*.stl) | *.stl";
        //bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg, *.jpeg, *.jpe, *.jfif, *.png";
        //bp.filter = "";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp, "untitled", ".stl", path =>
        {
            StartCoroutine(Save3DModel(path));
        });
    }

    IEnumerator Save3DModel(string path)
    {
        yield return null;

        //LModel1
        if (LModel.activeSelf)
        {
            //string File_Model1 = Path.Combine(path, "Export1" + ".stl");
            string File_Model1 = path;
            LModel.transform.GetChild(0).gameObject.SetActive(true);
            LModel.transform.GetChild(1).gameObject.SetActive(true);
            LModel.transform.GetChild(2).gameObject.SetActive(true);
            Mesh mesh1 = LModel.GetComponent<MeshFilter>().mesh;
            Exporter.WriteFile(File_Model1, mesh1, FileType.Ascii);

            ExportStatus = "Export Large Complete 1";
            Debug.Log("Export Large Complete 1");
        }
    }*/

}
