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
    [SerializeField] GameObject limbPickup;
    [SerializeField] Transform limbPickupDropPoint;
    //i am stupid please yell at me for doing this here
    public ParticleSystem myFire;

    IEnumerator makeSureStupidLimbGlitchDoesntHappen()
    {
        yield return new WaitForSeconds(1);
        canSwitch = true;
    }

    private void Start()
    {
        input = gameObject.GetComponentInParent<PlayerInputHandler>();
        StartCoroutine(makeSureStupidLimbGlitchDoesntHappen());
        initializeLimbs();
    }

    public void initializeLimbs()
    {
        disableAllMyLimbs();
        for (int i = 0; i < armCount; i++)
        {
            armArray[i].SetActive(true);
        }
        for (int i2 = 0; i2 < legCount; i2++)
        {
            legArray[i2].SetActive(true);
        }
    }
    private void Update()
    {
        if (input.addArmPlease2 && canSwitch && legCount > 0)
        {
            input.UseArm();
            SwitchAnArm();
        }
        if (input.addLegPlease2 && canSwitch && armCount > 0)
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
        legArray[legCount].SetActive(false);
    }

    void SwitchALeg()
    {
        legCount++;
        armCount--;
        if (legCount > legMax) legCount = legMax;
        if (armCount < 0) armCount = 0;
        legArray[legCount - 1].SetActive(true);
        armArray[armCount].SetActive(false);
    }

    public void getALimb()
    {
        if (armCount < armMax)
        {
            armCount++;
            armArray[armCount - 1].SetActive(true);
        }
        if(legCount < legMax)
        {
            legCount++;
            legArray[legCount - 1].SetActive(true);
        }
    }
    /*
    public void loseALimb(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            if(armCount > 0)
            {
                armCount--;
                armArray[armCount - 1].SetActive(false);
            }
            else if(legCount > 0)
            {
                legCount--;
                legArray[legCount - 1].SetActive(true);
            }
            var newPickup = GameObject.Instantiate(limbPickup, limbPickupDropPoint.position, limbPickupDropPoint.rotation);
            newPickup.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), 2, Random.Range(-1, 1)) * 5, ForceMode.Impulse);
            Debug.Log("fired");

        }
    }
    */

    void disableAllMyLimbs()
    {
        foreach(GameObject limb in armArray) limb.SetActive(false);
        foreach (GameObject limb in legArray) limb.SetActive(false);
    }

}
