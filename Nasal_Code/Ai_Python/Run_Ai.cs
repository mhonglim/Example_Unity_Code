using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Scripting.Python;

public class Run_Ai : MonoBehaviour
{
    private string Path;
    private string FileName;
    //Tag
    public bool Ai_Tag = false;

    public void NasalAi()
    {
        FileName = "Segment_Package_New.py";
        Path = Application.dataPath + "/Scripts/Ai_Python/" + FileName;
        PythonRunner.RunFile(Path);
        Debug.Log("Call Ai Python Path:" + Path);
        Ai_Tag = true;
    }
}
