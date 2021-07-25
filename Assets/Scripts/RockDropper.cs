using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropper : MonoBehaviour
{
    [SerializeField] GameObject rock1, rock2, rock3, limbPickup;
    [SerializeField] Transform dropPoint;
    [SerializeField] StartGame gameStarter;
    bool started = false;

    private void Update()
    {
        if(!started && gameStarter.gameStarted)
        {
            Debug.Log("DING!!!!!");
            StartCoroutine(dropRocksRandomly());
            started = true;
        }
    }

    IEnumerator dropRocksRandomly()
    {
        Debug.Log("DONG!!!!");
        yield return new WaitForSeconds(Random.Range(5, 15));
        int objectToInstantiate;
        objectToInstantiate = Random.Range(1, 3 + gameStarter.camScript.playerList.Count);

        switch (objectToInstantiate)
        {
            case 1:
                Instantiate(rock1, dropPoint.position, dropPoint.rotation);
                break;
            case 2:
                Instantiate(rock2, dropPoint.position, dropPoint.rotation);
                break;
            case 3:
                Instantiate(rock3, dropPoint.position, dropPoint.rotation);
                break;
            default:
                Instantiate(limbPickup, dropPoint.position, dropPoint.rotation);
                break;
        }
        StartCoroutine("dropRocksRandomly");
    }
}
