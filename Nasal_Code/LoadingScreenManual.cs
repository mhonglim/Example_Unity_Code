using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingScreenManual : MonoBehaviour
{

    ManualReshape ManualReshape_Script;
    CommandLine4 CommandLine4_Script;
    Scene_Manager Scene_Manager_Script;
    AutoRunCodeManual AutoRunCode_Script;

    public Slider Load_Slider;
    public TMP_Text Process_Text;

    private void Start()
    {
        ManualReshape_Script = GameObject.FindGameObjectWithTag("Reshape").GetComponent<ManualReshape>();
        CommandLine4_Script = GameObject.FindGameObjectWithTag("Smooth").GetComponent<CommandLine4>();
        Scene_Manager_Script = GameObject.FindGameObjectWithTag("Scene").GetComponent<Scene_Manager>();
        AutoRunCode_Script = GameObject.FindGameObjectWithTag("Autorun").GetComponent<AutoRunCodeManual>();

        Invoke(nameof(LoadScreen), 2);
    }

    public void LoadScreen()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        while (AutoRunCode_Script.AutoRun_Start == false)
        {
            yield return null;
        }
        Debug.Log("AutoRun Started");
        Process_Text.text = "Calculating shape and length of nasal splint";
        Load_Slider.value = 0.2f;

        while (ManualReshape_Script.Reshape_Tag_Start == false)
        {
            yield return null;
        }
        Debug.Log("Reshape Started");
        Process_Text.text = "Reshaping 3D models of nasal splint";
        Load_Slider.value = 0.3f;

        while (ManualReshape_Script.Reshape_Tag_End == false)
        {
            yield return null;
        }
        Debug.Log("Reshape Ended");
        Process_Text.text = "Reshaping 1st 3D model of nasal splint";
        Load_Slider.value = 0.4f;

        while (ManualReshape_Script.Fix_Tag_Start == false)
        {
            yield return null;
        }
        Debug.Log("Fix 2nd Model Started");
        Process_Text.text = "Reshaping 2nd 3D model of nasal splint";
        Load_Slider.value = 0.6f;

        while (ManualReshape_Script.Export_Tag_Start == false)
        {
            yield return null;
        }
        Debug.Log("Export Rough Started");
        Process_Text.text = "Transfering 3D models to Blender";
        Load_Slider.value = 0.7f;


        while (ManualReshape_Script.Export_Tag_End == false)
        {
            yield return null;
        }
        Debug.Log("Export Rough Loaded");
        Process_Text.text = "Transfered 3D models to Blender";
        Load_Slider.value = 0.8f;

        while (CommandLine4_Script.Smooth_Tag_Start == false)
        {
            yield return null;
        }
        Debug.Log("Smooth Started");
        Process_Text.text = "Smoothing 3D models of nasal splint";
        Load_Slider.value = 0.9f;

        while (CommandLine4_Script.Smooth_Tag_End == false)
        {
            yield return null;
        }
        Debug.Log("Smooth Ended");
        Process_Text.text = "Smoothed 3D models of nasal splint";
        Load_Slider.value = 1.0f;
    }
}
