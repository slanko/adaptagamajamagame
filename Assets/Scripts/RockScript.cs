using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] AudioClip impactSound;
    AudioSource aud;
    bool ready = false;

    IEnumerator waitASecThenFlipBool()
    {
        yield return new WaitForSeconds(3);
        ready = true;
        Debug.Log("ready!");
    }
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        StartCoroutine(waitASecThenFlipBool());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Lava" && ready) aud.PlayOneShot(impactSound);
        Debug.Log("played my sound");
    }
}
