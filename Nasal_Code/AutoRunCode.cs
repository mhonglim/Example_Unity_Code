using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunCode : MonoBehaviour
{
    Run_Ai Run_Ai_Script;
    JSONReader JSONReader_Script;
    CommandLine4 CommandLine4_Script;
    Scene_Manager Scene_Manager_Script;

    //Tag
    public bool AutoRun_Start = false;


    private void Start()
    {
        Run_Ai_Script = GameObject.FindGameObjectWithTag("Ai").GetComponent<Run_Ai>();
        JSONReader_Script = GameObject.FindGameObjectWithTag("Reshape").GetComponent<JSONReader>();
        CommandLine4_Script = GameObject.FindGameObjectWithTag("Smooth").GetComponent<CommandLine4>();
        Scene_Manager_Script = GameObject.FindGameObjectWithTag("Scene").GetComponent<Scene_Manager>();

        Invoke(nameof(Auto_1), 2);
    }

    private void Auto_1()
    {
        StartCoroutine(LateStart0());
    }

    IEnumerator LateStart0()
    {
        Debug.Log("LateStart0 Start");
        yield return StartCoroutine(LateStart1()); 
        CommandLine4_Script.Smoothing();
        while (CommandLine4_Script.Smooth_Tag_End == false)
        {
            yield return null;
        }
        Debug.Log("CommandLine4 End");
        Scene_Manager_Script.Scene_Browse("Automatic_02_SaveModels");
    }

    IEnumerator LateStart1()
    {
        Debug.Log("LateStart1 Start");
        yield return StartCoroutine(LateStart2(3));
        JSONReader_Script.JSON_Start();
        while (JSONReader_Script.Export_Tag_End == false)
        {
            yield return null;
        }
        Debug.Log("JSON Export Rough Model End");

    }

    IEnumerator LateStart2(float waitTime)
    {
        Debug.Log("LateStart2 Start");
        AutoRun_Start = true;
        Run_Ai_Script.NasalAi();
        yield return new WaitForSeconds(waitTime);
        while (Run_Ai_Script.Ai_Tag == false)
        {
            yield return null;
        }
        Debug.Log("Run_AI End");
        
    }
}
