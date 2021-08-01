using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpLightingOnGameStart : MonoBehaviour
{
    [SerializeField] Color ground, equator, sky;
    [SerializeField] float lerpSpeed;

    public void beginTheLerp()
    {
        RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, ground, lerpSpeed * Time.deltaTime);
        RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, equator, lerpSpeed * Time.deltaTime);
        RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, sky, lerpSpeed * Time.deltaTime);
    }

}
