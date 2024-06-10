using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCont : MonoBehaviour
{
    private static SoundCont instance = null;
    public static SoundCont Instance
    {
        get { return instance;}
    }

    void Awake()
    {
        //GameObject[] MusicObj = GameObject.FindGameObjectsWithTag("Sound");
        if (instance != null && instance != this)
        {
            //Destroy(this.gameObject);
            return;
        }
        else
        {
            //instance = this;
        }
        this.gameObject.tag = "Soundless";
        DontDestroyOnLoad(this.gameObject);
    }
}
