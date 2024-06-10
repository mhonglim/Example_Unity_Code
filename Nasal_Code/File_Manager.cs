using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;
using System.IO;
using UnityEditor.Scripting.Python;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;

public class File_Manager : MonoBehaviour
{
    public RawImage rawImage;

    public void OpenFileBrowser()
    {
        var bp = new AnotherFileBrowser.Windows.BrowserProperties();
        //bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg, *.jpeg, *.jpe, *.jfif, *.png";
        bp.filter = "";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            StartCoroutine(LoadImage(path));
        });
    }

    IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                var texWidth = uwrTexture.width;
                var texHeight = uwrTexture.height;
                float TargetPixel = 600.0000f;
                float Scale = 0.0000f;
                //float RawImageScale = 0.5f; --Old Constant that's replaced by Scale
                StaticData.ImageWidthToKeep = texWidth;
                StaticData.ImageHeightToKeep = texHeight;

                Debug.Log("Height:" + texHeight + "Width:" + texWidth);

                if(texWidth > texHeight)
                {
                    Scale = texWidth / TargetPixel;
                }
                else if(texHeight > texWidth)
                {
                    Scale = texHeight/ TargetPixel;
                }

                Debug.Log("Scale:" + Scale);

                rawImage.texture = uwrTexture;
                rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2((texWidth / Scale), (texHeight / Scale));
                //rawImage.rectTransform.localScale = new Vector3(Scale, Scale, Scale);
            }
        }
    }

    public void SaveImage()
    {
        if (rawImage != null && rawImage.texture != null)
        {
            string FileName = "Patient_01.jpg";

            Texture2D tex = rawImage.texture as Texture2D;
            if (tex != null)
            {
                // Convert texture to byte array
                byte[] bytes = tex.EncodeToJPG();

                // Choose a file path to save
                //string path = Path.Combine(Application.persistentDataPath, "Patient_01.jpg");
                string path = Application.dataPath + "/Nasal_Image/" + FileName;

                // Write bytes to the chosen path
                File.WriteAllBytes(path, bytes);

                Debug.Log($"Image saved to: {path}");
            }
        }
        else
        {
            Debug.Log("Please Choose the Image");
        }

        RefreshImage();
    }

    public void RefreshImage()
    {
        AssetDatabase.Refresh();
        Debug.Log("Refreshed Database");
    }

    public void DeleteFolder()
    {
        string path = "C:/Users/acer/Desktop/Python_Ai/Yolo8/Image_Predicted/" + "Predict_Folder";

        if (Directory.Exists(Path.GetDirectoryName(path)))
        {
            FileUtil.DeleteFileOrDirectory(path);
        }
    }
}
