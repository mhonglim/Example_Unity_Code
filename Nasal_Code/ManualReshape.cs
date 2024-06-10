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

public class ManualReshape : MonoBehaviour
{
    //Calculate
    public float resultCol;
    public float resultLeftRed;
    public float resultLeftBlue;
    public float resultRightRed;
    public float resultRightBlue;

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

    //Tag
    public bool ManualReshape_Tag = false;
    public bool Reshape_Tag_Start = false;
    public bool Reshape_Tag_End = false;
    public bool Export_Tag_Start = false;
    public bool Export_Tag_End = false;
    public bool Fix_Tag_Start = false;

    // Start is called before the first frame update

    private void Awake()
    {
        resultCol = CalculateLine.resultCol;
        resultLeftRed = CalculateLine.resultLeftRed;
        resultLeftBlue = CalculateLine.resultLeftBlue;
        resultRightRed = CalculateLine.resultRightRed;
        resultRightBlue = CalculateLine.resultRightBlue;
    }

    public void Reshape_Start()
    {
        StartCoroutine(LateStart1(1));
    }

    IEnumerator LateStart1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ReShapeColumella();
        StartCoroutine(LateStart2(5));
    }

    IEnumerator LateStart2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ReShapeNostril();
        StartCoroutine(LateStart4(5));
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

    //Reshape Manual Right = Model.transform.GetChild(0) / Left = Model.transform.GetChild(1)
    public void ReShapeNostril()
    {
        if(StaticData.Marker05)
        {
            // L Model Original
            if (LModel.transform.GetChild(0).gameObject.activeSelf)
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
                Transform Rside = LModel.transform.GetChild(0);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped";
            }

            if (LModel.transform.GetChild(1).gameObject.activeSelf)
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

                Transform Lside = LModel.transform.GetChild(1);
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

            //LModel2 = L Model Carved
            if (LModel2.transform.GetChild(0).gameObject.activeSelf)
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
                Transform Rside = LModel2.transform.GetChild(0);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped";
            }

            if (LModel2.transform.GetChild(1).gameObject.activeSelf)
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

                Transform Lside = LModel2.transform.GetChild(1);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped";
            }

            /*        
                    if (LModel2.transform.GetChild(0).gameObject.activeSelf && LModel2.transform.GetChild(1).gameObject.activeSelf)
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

        // M Model
        /*
        if (MModel.transform.GetChild(0).gameObject.activeSelf)
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
        }
        */

        else if (StaticData.Marker03)
        {
            // S Model Original
            if (SModel.transform.GetChild(0).gameObject.activeSelf)
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

                Transform Rside = SModel.transform.GetChild(0);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped";
            }
            if (SModel.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure_L = resultLeftRed;
                //Doctor said Y = Y * 1.2
                float scaleX_L = (unityUnit / 0.85f) * xMeasure_L * YScaleFactor;
                float yMeasure_L = resultLeftBlue;
                yForCol2 = yMeasure_L;
                float scaleY_L = (unityUnit / 0.67f) * yMeasure_L;
                //Doctor said X = (Y + X)/2
                scaleY_L = (scaleY_L + scaleX_L) / 2;

                Transform Lside = SModel.transform.GetChild(1);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped";
            }

            /*
            if (SModel.transform.GetChild(0).gameObject.activeSelf && SModel.transform.GetChild(1).gameObject.activeSelf)
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
            }
            */

            // S Model Carved
            if (SModel2.transform.GetChild(0).gameObject.activeSelf)
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

                Transform Rside = SModel2.transform.GetChild(0);
                Rside.transform.localScale = new UnityEngine.Vector3(scaleX_R, 1, scaleY_R); //scaleY(in program) = x coordinate (in model), scaleX (in program) = z coordinate (in model) 

                NostrilReshapedStatus = "Right side is reshaped 2";
            }
            if (SModel2.transform.GetChild(1).gameObject.activeSelf)
            {
                float xMeasure_L = resultLeftRed;
                //Doctor said Y = Y * 1.2
                float scaleX_L = (unityUnit / 0.85f) * xMeasure_L * YScaleFactor;
                float yMeasure_L = resultLeftBlue;
                yForCol2 = yMeasure_L;
                float scaleY_L = (unityUnit / 0.67f) * yMeasure_L;
                //Doctor said X = (Y + X)/2
                scaleY_L = (scaleY_L + scaleX_L) / 2;

                Transform Lside = SModel2.transform.GetChild(1);
                Lside.transform.localScale = new UnityEngine.Vector3(scaleX_L, 1, scaleY_L);

                NostrilReshapedStatus = "Left side is reshaped 2";
            }
        }
        Selection.activeGameObject = GameObject.Find("Main Camera");
    }


    public void ReShapeColumella()
    {
        Reshape_Tag_Start = true;
        if(StaticData.Marker05)
        {
            // L Model Original
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

            //L Model Carved
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
        
        /*
        if (MModel.activeSelf)
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


        else if(StaticData.Marker03)
        {
            //S Model Original
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

                Debug.Log("Export Large Complete 2");
            }
        }

        /*
        if (MModel.activeSelf)
        {
            MModel.transform.GetChild(0).gameObject.SetActive(true);
            MModel.transform.GetChild(1).gameObject.SetActive(true);
            MModel.transform.GetChild(2).gameObject.SetActive(true);
            Mesh mesh = MModel.GetComponent<MeshFilter>().mesh;
            Exporter.WriteFile(path, mesh, FileType.Ascii);

            Debug.Log("Export Medium Complete");
        }*/

        else if(StaticData.Marker03)
        {
            //S Model 1
            if (SModel.activeSelf)
            {
                string File_Model1 = Path.Combine(path, "Export1" + ".stl");

                SModel.transform.GetChild(0).gameObject.SetActive(true);
                SModel.transform.GetChild(1).gameObject.SetActive(true);
                SModel.transform.GetChild(2).gameObject.SetActive(true);
                Mesh mesh1 = SModel.GetComponent<MeshFilter>().mesh;
                Exporter.WriteFile(File_Model1, mesh1, FileType.Ascii);

                Debug.Log("Export Small Complete 1");
            }

            //S Model 2
            if (SModel2.activeSelf)
            {
                string File_Model2 = Path.Combine(path, "Export2" + ".stl");

                SModel2.transform.GetChild(0).gameObject.SetActive(true);
                SModel2.transform.GetChild(1).gameObject.SetActive(true);
                SModel2.transform.GetChild(2).gameObject.SetActive(true);
                Mesh mesh2 = SModel2.GetComponent<MeshFilter>().mesh;
                Exporter.WriteFile(File_Model2, mesh2, FileType.Ascii);

                Debug.Log("Export Small Complete 2");
            }
        }
        
        Export_Tag_End = true;
        ManualReshape_Tag = true;
    }

    public void FixBugModel2()
    {
        Fix_Tag_Start = true;

        if (StaticData.Marker05)
        {
            Selection.activeGameObject = GameObject.Find(LModel.name);
        }
        else if (StaticData.Marker03)
        {
            Selection.activeGameObject = GameObject.Find(SModel.name);
        }
    }
}
