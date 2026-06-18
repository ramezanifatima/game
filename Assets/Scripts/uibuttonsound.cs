using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uibuttonsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
