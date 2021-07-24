using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject[] armArray, legArray;
    public int armCount, legCount;
    [SerializeField] int armMax, legMax; // should be hard capped to 10 but hey shut up
    [SerializeField] PlayerInputHandler input;

    private void Start()
    {
        input = gameObject.GetComponentInParent<PlayerInputHandler>();
        for(int i = 0; i < armCount; i++)
        {
            armArray[i].SetActive(true);
        }
        for(int i2 = 0; i2 < legCount; i2++)
        {
            legArray[i2].SetActive(true);
        }
    }

    private void Update()
    {
        if (input.addArmPlease2)
        {
            input.UseArm();
            SwitchAnArm();
        }
        if (input.addLegPlease2 == true)
        {
            input.UseLeg();
            SwitchALeg();
        }
    }

    void SwitchAnArm()
    {
        armCount++;
        legCount--;
        if (armCount > armMax) armCount = armMax;
        if (legCount < 0) legCount = 0;
        armArray[armCount - 1].SetActive(true);
        legArray[legCount - 1].SetActive(false);
    }

    void SwitchALeg()
    {
        legCount++;
        armCount--;
        if (legCount > legMax) legCount = legMax;
        if (armCount < 0) armCount = 0;
        legArray[legCount - 1].SetActive(true);
        armArray[armCount - 1].SetActive(false);
    }


}
