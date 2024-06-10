using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunCodeManual : MonoBehaviour
{
    ManualReshape ManualReshape_Script;
    CommandLine4 CommandLine4_Script;
    Scene_Manager Scene_Manager_Script;

    //Tag
    public bool AutoRun_Start = false;

    // Start is called before the first frame update
    private void Awake()
    {

    }

    private void Start()
    {
        ManualReshape_Script = GameObject.FindGameObjectWithTag("Reshape").GetComponent<ManualReshape>();
        CommandLine4_Script = GameObject.FindGameObjectWithTag("Smooth").GetComponent<CommandLine4>();
        Scene_Manager_Script = GameObject.FindGameObjectWithTag("Scene").GetComponent<Scene_Manager>();

        Invoke(nameof(Auto_1), 2);
    }

    private void Update()
    {

    }

    private void Auto_1()
    {
        StartCoroutine(LateStart0());
    }

    IEnumerator LateStart0()
    {
        Debug.Log("LateStart0 Start");
        yield return StartCoroutine(LateStart1(2));
        CommandLine4_Script.Smoothing();
        while (CommandLine4_Script.Smooth_Tag_End == false)
        {
            yield return null;
        }
        Debug.Log("CommandLine4 End");
        Scene_Manager_Script.Scene_Browse("Automatic_02_SaveModels");
    }

    IEnumerator LateStart1(float waitTime)
    {
        Debug.Log("LateStart1 Start");
        AutoRun_Start = true;
        ManualReshape_Script.Reshape_Start();
        yield return new WaitForSeconds(waitTime);
        while (ManualReshape_Script.ManualReshape_Tag == false)
        {
            yield return null;
        }
        Debug.Log("Manual Reshape End");

    }

}
