using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroy : MonoBehaviour
{
    public GameObject MusicObj;

    void Start()
    {
        Invoke("DestroySound", 3);
    }

    // Update is called once per frame
    public void DestroySound()
    {
        MusicObj = GameObject.FindWithTag("Soundless");
        Destroy(MusicObj);
    }
}
