using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip footStepSound;
    float myPitch;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        myPitch = aud.pitch;
    }

    public void playSound(AudioClip sound)
    {
        aud.PlayOneShot(sound);
        Debug.Log("Played " + sound);
    }

    public void playFootstepSound()
    {
        aud.pitch = myPitch + Random.Range(myPitch * 0.5f * -1, myPitch * 0.5f);
        aud.PlayOneShot(footStepSound);
        aud.pitch = myPitch;
    }
}
