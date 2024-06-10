using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CommandLine4 : MonoBehaviour
{
    //Tag
    public bool Smooth_Tag_Start = false;
    public bool Smooth_Tag_End = false;

    public void Smoothing()
    {
        Smooth_Tag_Start = true;
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.FileName = "C:\\Program Files\\Blender Foundation\\Blender 3.4\\blender.exe";
        process.StartInfo.Arguments = "--background --python \"C:\\Users\\acer\\NasalSplint_V2\\Assets\\Scripts\\Blender_Python\\Blender_Subdivision.py\"";

        //use to create no window when running cmd script
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

        process.Start();

        //if you want program to halt until script is finished
        process.WaitForExit();
        Debug.Log("Smoothing is finished");

        if (process != null && !process.HasExited )
        {
            process.Kill();
        }

        System.Diagnostics.Process process2 = new System.Diagnostics.Process();
        process2.StartInfo.FileName = "C:\\Program Files\\Blender Foundation\\Blender 3.4\\blender.exe";
        process2.StartInfo.Arguments = "--background --python \"C:\\Users\\acer\\NasalSplint_V2\\Assets\\Scripts\\Blender_Python\\Blender_Subdivision2.py\"";

        //use to create no window when running cmd script
        process2.StartInfo.UseShellExecute = true;
        process2.StartInfo.CreateNoWindow = true;
        process2.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

        process2.Start();

        //if you want program to halt until script is finished
        process2.WaitForExit();
        Debug.Log("Smoothing is finished");

        if (process2 != null && !process2.HasExited)
        {
            process2.Kill();
        }
        Smooth_Tag_End = true;
    }
}
