using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject[] armArray, legArray;
    public int armCount, legCount;
    [SerializeField] int armMax, legMax; // should be hard capped to 10 but hey shut up
    [SerializeField] PlayerInputHandler input;
    [SerializeField] bool canSwitch = false;

    IEnumerator makeSureStupidLimbGlitchDoesntHappen()
    {
        yield return new WaitForSeconds(1);
        canSwitch = true;
    }

    private void Start()
    {
        input = gameObject.GetComponentInParent<PlayerInputHandler>();
        StartCoroutine(makeSureStupidLimbGlitchDoesntHappen());
        disableAllMyLimbs();
        for (int i = 0; i < armCount; i++)
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
        if (input.addArmPlease2 && canSwitch)
        {
            input.UseArm();
            SwitchAnArm();
        }
        if (input.addLegPlease2 && canSwitch)
        {
            input.UseLeg();
            SwitchALeg();
        }
    }

    void SwitchAnArm()
    {
        armCount++;
        if (armCount > armMax) armCount = armMax;
        if (legCount < 0) legCount = 0;
        armArray[armCount - 1].SetActive(true);
        //legArray[legCount].SetActive(false);
    }

    void SwitchALeg()
    {
        legCount++;
        if (legCount > legMax) legCount = legMax;
        if (armCount < 0) armCount = 0;
        legArray[legCount - 1].SetActive(true);
        //armArray[armCount].SetActive(false);
    }


    void disableAllMyLimbs()
    {
        foreach(GameObject limb in armArray) limb.SetActive(false);
        foreach (GameObject limb in legArray) limb.SetActive(false);
    }

}
