using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static string MeasureMethod;
    public void Scene_Browse(string sceneName)
    {
        Debug.Log("Load scene: " + sceneName);

        if(MeasureMethod == "Automatic" && sceneName == "BrowseImageChoice")
        {
            SceneManager.LoadScene("Automatic_99_Prototype", LoadSceneMode.Single);
        }
        else if(MeasureMethod == "Manual" && sceneName == "BrowseImageChoice")
        {
            SceneManager.LoadScene("Manual_01_AdjustImage_n_DrawLine", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        
    }

    public void SelectManual()
    {
        MeasureMethod = "Manual";
        Debug.Log(MeasureMethod);
    }

    public void SelectAutomatic()
    {
        MeasureMethod = "Automatic";
        Debug.Log(MeasureMethod);
    }
}
