using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    public AudioClip ClickSound;

    private AudioSource PlayAudio;

    void Start()
    {
        PlayAudio = GetComponent<AudioSource>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        PlayAudio.PlayOneShot(ClickSound, 0.4f);
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
