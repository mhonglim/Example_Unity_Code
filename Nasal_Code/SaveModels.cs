using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherFileBrowser.Windows;
using Parabox.Stl;
using System.IO;
using UnityEditor;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Runtime.ConstrainedExecution;
using System;

public class SaveModels : MonoBehaviour
{
    public void SaveFileBrowser_01()
    {
        var bp = new AnotherFileBrowser.Windows.BrowserProperties();
        bp.filter = "3D Model file (*.stl) | *.stl";
        //bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg, *.jpeg, *.jpe, *.jfif, *.png";
        //bp.filter = "";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp, "untitled", ".stl", path =>
        {
            StartCoroutine(Save3DModel_01(path));
        });
    }

    IEnumerator Save3DModel_01(string path)
    {
        Debug.Log(path);
        yield return null;
        //Model 1
        string fileName = "Export1.stl";
        string sourcePath = @"C:\Users\acer\NasalSplint_V2\Result_Smooth";
        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);

        // To copy a file to another location and 
        // overwrite the destination file if it already exists.
        System.IO.File.Copy(sourceFile, path, true);

        Debug.Log("Save Nasal Splint Original");
    }

    public void SaveFileBrowser_02()
    {
        var bp = new AnotherFileBrowser.Windows.BrowserProperties();
        bp.filter = "3D Model file (*.stl) | *.stl";
        //bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg, *.jpeg, *.jpe, *.jfif, *.png";
        //bp.filter = "";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp, "untitled", ".stl", path =>
        {
            StartCoroutine(Save3DModel_02(path));
        });
    }

    IEnumerator Save3DModel_02(string path)
    {
        Debug.Log(path);
        yield return null;
        //Model 2
        string fileName = "Export2.stl";
        string sourcePath = @"C:\Users\acer\NasalSplint_V2\Result_Smooth";
        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);

        // To copy a file to another location and 
        // overwrite the destination file if it already exists.
        System.IO.File.Copy(sourceFile, path, true);

        Debug.Log("Save Nasal Splint Curved");
    }
}
