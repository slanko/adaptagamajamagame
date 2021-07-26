using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip sound)
    {
        aud.PlayOneShot(sound);
        Debug.Log("Played " + sound);
    }
}
