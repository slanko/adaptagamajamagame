using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropper : MonoBehaviour
{
    [SerializeField] GameObject rock1, rock2, rock3, limbPickup;
    [SerializeField] Transform dropPoint;

    private void Start()
    {
        StartCoroutine("dropRocksRandomly");
    }

    IEnumerable dropRocksRandomly()
    {
        yield return new WaitForSeconds(Random.Range(5, 15));
        int objectToInstantiate;
        objectToInstantiate = Random.Range(1, 5);

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
            case 4:
                Instantiate(limbPickup, dropPoint.position, dropPoint.rotation);
                break;
        }
        StartCoroutine("dropRocksRandomly");
    }
}
