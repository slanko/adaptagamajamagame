using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    public int playersInArea = 0;
    public CameraScript camScript;
    public GameObject[] panels = null;
    public GameObject col = null;
    public GameObject worldGod = null;
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.tag == "Player")
        {
            playersInArea++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print(other);
        if (other.tag == "Player")
        {
            playersInArea--;
        }
    }

    private void Update()
    {
        if (playersInArea == (camScript.playerList.Count * 2) && playersInArea > 0)
        {
            foreach (GameObject obj in panels)
                obj.SetActive(false);
            col.SetActive(true);
            worldGod.GetComponent<PlayerInputManager>().DisableJoining();
        }
    }
}
