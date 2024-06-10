using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroyClick : MonoBehaviour
{
    public GameObject MusicObj;

    public void DestroySound()
    {
        MusicObj = GameObject.FindWithTag("Soundless");
        Destroy(MusicObj);
    }
}