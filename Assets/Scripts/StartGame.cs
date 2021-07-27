using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public int playersInArea = 0;
    public CameraScript camScript;
    public GameObject[] panels = null;
    public GameObject col = null;
    public GameObject worldGod = null;
    [SerializeField] Animator lavaAnim;
    public bool gameStarted;
    [SerializeField] Text joinText, winText;
    [SerializeField] AudioSource ambiance;

    private void Start()
    {
        lavaAnim.speed = 0;
    }
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
            gameStarted = true;
            joinText.gameObject.SetActive(false);
            lavaAnim.speed = 1;
            ambiance.Play();
        }

        if(camScript.playerList.Count <= 1 && gameStarted)
        {
            if (!winText.gameObject.activeSelf)
            {
                winText.gameObject.SetActive(true);
                winText.text = camScript.playerList[0].GetComponent<PlayerGod>().playerData.characterName.ToUpper() + " WINZ!!";
            }
            Invoke("LoadScene", 3);
        }

    }

    void LoadScene()
    {
        print("hit");
        SceneManager.LoadScene(0);
    }
}
