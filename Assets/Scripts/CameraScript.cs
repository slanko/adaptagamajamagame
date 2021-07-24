using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public List<Transform> playerList;
    [SerializeField] float lerpSpeed;

    Vector3 calculateMidpoint()
    {
        float totalX = 0f, totalY = 0f, totalZ = 0f;

        foreach(var player in playerList)
        {
            totalX += player.position.x;
            totalY += player.position.y;
            totalZ += player.position.z;
        }
        Vector3 midpoint = new Vector3(totalX / playerList.Count, totalY / playerList.Count, totalZ / playerList.Count);
        return midpoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, calculateMidpoint(), lerpSpeed * Time.deltaTime);
    }
}
