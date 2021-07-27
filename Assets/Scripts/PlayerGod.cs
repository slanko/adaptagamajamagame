using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGod : MonoBehaviour
{
    public RootState curState { get; private set; }
    Rigidbody rb = null;
    public GameObject mesh;
    public PlayerInputHandler input;
    public PlayerData playerData;
    [SerializeField] private SphereCollider pushBox;
    [SerializeField] private SphereCollider painBox;
    public Transform pickBoxTarget;
    private GameObject myRock = null;
    public Animator anim;
    [SerializeField] private GameObject uIElementPrefab = null;
    [SerializeField] private GameObject uIElement = null;
    public LimbSwitcherScript limbScript = null;
    public float lavaYLevel;
    [SerializeField] Transform lava;
    private Text legsText;
    private Text armsText;
    private AudioScript aud;
    ParticleSystem fire;
    [SerializeField] AudioClip bonkSound, bonkReverb, shovedSound, fireSound;
    public bool altCharacter;
    [SerializeField] GameObject mySkeleton;
    bool dead = false;

    void Awake()
    {
        lava = GameObject.Find("Lava").transform;
        transform.position = new Vector3(Random.Range(-5, 5), 42, 15.75f);
        rb = GetComponent<Rigidbody>();
        mesh = Instantiate(playerData.characterMesh, transform);
        input = GetComponent<PlayerInputHandler>();
        anim = mesh.GetComponent<Animator>();
        limbScript = GetComponentInChildren<LimbSwitcherScript>();
        uIElement = Instantiate(uIElementPrefab, GameObject.Find("Horizontal Thingum").transform);
        uIElement.transform.Find("PlayerIcon").GetComponent<Image>().sprite = playerData.characterSprite;
        armsText = uIElement.transform.Find("ArmCountText").GetComponent<Text>();
        legsText = uIElement.transform.Find("LegCountText").GetComponent<Text>();
        aud = mesh.GetComponent<AudioScript>();
        fire = limbScript.myFire;
        foreach (Transform child in mesh.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                MeshRenderer target = child.GetComponent<MeshRenderer>();
                target.material = playerData.characterMaterial;
            }
        }
        CameraScript cam = Camera.main.transform.parent.GetComponent<CameraScript>();
        cam.playerList.Add(transform);
        Initialize(new IdleState("Idle"));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shovey") aud.playSound(shovedSound);
        if(other.tag == "Ouchie") aud.playSound(bonkReverb);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Shovey")
        {
                Vector3 dir = (transform.position - other.transform.position).normalized;
                curState.OnShoved(this, 20, dir);
        }
        if (other.tag == "Ouchie")
        {
            curState.OnBonked(this, 10, (transform.position - other.transform.position).normalized, 5);
        }
        if(other.tag == "lava")
        {
            Invoke("HUDDeath", 5);
            if(!fire.gameObject.activeSelf) fire.gameObject.SetActive(true);
            if (!fire.isPlaying)
            {
                fire.Play();
                aud.playSound(fireSound);
                Debug.Log("Caught fire!");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        float rockVelocity = 0;
        if (collision.gameObject.tag == "Rock1" || collision.gameObject.tag == "Rock2" || collision.gameObject.tag == "Rock3") 
            rockVelocity = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        //sorry about that
        if (collision.gameObject.tag == "Rock1" && rockVelocity > 3) 
        {
            curState.OnBonked(this, 10, (transform.position - collision.transform.position).normalized, 1);
            if (collision.relativeVelocity.magnitude > 5) aud.playSound(bonkSound);
        }
        else if(collision.gameObject.tag == "Rock2" && rockVelocity > 3)
        {
            curState.OnBonked(this, 15, (transform.position - collision.transform.position).normalized, 2);
            if (collision.relativeVelocity.magnitude > 5) aud.playSound(bonkSound);
        }
        else if (collision.gameObject.tag == "Rock3" && rockVelocity > 3)
        {
            curState.OnBonked(this, 20, (transform.position - collision.transform.position).normalized, 3);
            if (collision.relativeVelocity.magnitude > 5) aud.playSound(bonkReverb);
        }
        if (collision.gameObject.tag == "LimbPickup")
        {
            limbScript.getALimb();
            Destroy(collision.gameObject);
        }
    }

    void Initialize(RootState startState)
    {
        curState = startState;
        curState.Enter(this);
    }

    public void ChangeState(RootState newState)
    {
        curState.Exit(this);
        curState = newState;
        curState.Enter(this);
    }

    private void Update()
    {
        curState.RegularUpdate(this);
        legsText.text = limbScript.legCount + "LEGS";
        armsText.text = limbScript.armCount + "ARMS";
        lavaYLevel = lava.position.y;
    }

    private void FixedUpdate()
    {
        curState.PhysicsUpdate(this);
    }

    [SerializeField]
    float speed = 5;
    Vector3 storedVel = Vector3.zero;
    public void Move()
    {
        float uSpeed = (speed * ((float)limbScript.legCount / 4f) * (float)playerData.speedMultiplier) + playerData.baseSpeed;
        Vector3 Move3 = new Vector3(input.moveVals.x, 0, input.moveVals.y);
        anim.SetFloat("WalkSpeed", Vector2.SqrMagnitude(input.moveVals));
        Vector3 holdthis = Move3 * uSpeed + storedVel;
        rb.velocity = new Vector3(holdthis.x, rb.velocity.y, holdthis.z);
        storedVel = rb.velocity * 0.1f; 
        Vector3 moveDir = (input.moveVals.x * Vector3.right) + (input.moveVals.y * Vector3.forward);
        if(input.moveVals != Vector2.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.25F);
        }
    }
    public void Stop()
    {
         rb.velocity = new Vector3 (rb.velocity.x * 0.8f, rb.velocity.y, rb.velocity.z * 0.8f);
    }
    public void Shove(float force, Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    public void ShoveLockout()
    {
            if(rb.velocity.magnitude >= 2.5f)
            {
                Debug.Log("Slow down plz");
                rb.velocity *= 0.95f;
            }
            else
            {
                ChangeState(new MoveState("Move"));
            }
    }
    public void CommitViolenceSoftly()
    {
        pushBox.enabled = true;
        StartCoroutine(CommitViolenceSoftlyLockout());
    }
    public void CommitViolenceViolently()
    {
        StartCoroutine(CommitViolenceViolentlyLockout());
    }
    IEnumerator CommitViolenceViolentlyLockout()
    {
        yield return new WaitForSeconds(0.1f);
        painBox.enabled = true;
        myRock.transform.position = transform.position + transform.forward * 1.5f + transform.up * 0.5f;
        yield return new WaitForSeconds(0.25f);
        painBox.enabled = false;
        Destroy(myRock);
        ChangeState(new MoveState("Move"));
    }
    IEnumerator CommitViolenceSoftlyLockout()
    {
        yield return new WaitForSeconds(0.4f);
        pushBox.enabled = false;
        ChangeState(new MoveState("Move"));
    }

    public void BeginPickupLoop()
    {
        if (pickBoxTarget != null)
        {
            StartCoroutine(PickupLoop());
        }
        else
        {
            StartCoroutine(PickupFail());
        }
    }
    IEnumerator PickupLoop()
    {
        GameObject holdThis = pickBoxTarget.gameObject;
        myRock = Instantiate(holdThis, transform);
        myRock.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        myRock.GetComponent<Collider>().isTrigger = true;
        Destroy(holdThis);
        myRock.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        yield return new WaitForSeconds(0.2f);
        ChangeState(new RockMoveState("RockMove"));
    }
    IEnumerator PickupFail()
    {
        yield return new WaitForSeconds(0.2f);
        ChangeState(new MoveState("Move"));
    }

    public void BeginThrowLoop()
    {
        StartCoroutine(ThrowTimer());
    }
    public IEnumerator ThrowTimer()
    {
        yield return new WaitForSeconds(.4f);
        Rigidbody rockRB = myRock.GetComponent<Rigidbody>();
        rockRB.constraints = RigidbodyConstraints.None;
        myRock.GetComponent<Collider>().isTrigger = false;
        myRock.transform.position = transform.position + transform.forward * 1.5f + transform.up;
        myRock.transform.parent = null;
        rockRB.AddForce(transform.forward * (10f * ((float)limbScript.armCount / 5f) * (float)playerData.distanceMultiplier), ForceMode.Impulse);
        rockRB.AddForce(transform.up * 5, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        myRock = null;
        ChangeState(new MoveState("Move"));
    }

    public void Bonked(float rockLevel, Vector3 dir)
    {
        StartCoroutine(BonkedLockout(rockLevel, dir));
    }
    IEnumerator BonkedLockout(float rockLevel, Vector3 dir)
    {
        if (rockLevel != 5)
        {
            rb.AddForce(dir * 10 * rockLevel, ForceMode.Impulse);
            //limbScript.loseALimb(1);
        }
        else
        {
            rb.AddForce(dir * 2 * rockLevel, ForceMode.Impulse);
            //limbScript.loseALimb(1);
        }
        yield return new WaitForSeconds(3);
        ChangeState(new MoveState("Move"));
    }
    public void BonkedFixedupdate()
    {
        if (rb.velocity.magnitude >= 2.5f)
        {
            Debug.Log("Slow down plz");
            rb.velocity *= 0.95f;
        }
    }

    public void DropRock()
    {
        Rigidbody rockRB = myRock.GetComponent<Rigidbody>();
        rockRB.constraints = RigidbodyConstraints.None;
        myRock.GetComponent<Collider>().isTrigger = false;
        Vector3 rSphere = Random.insideUnitSphere;
        rSphere = new Vector3(rSphere.x, 1.5f, rSphere.z);
        myRock.transform.position = transform.position + rSphere.normalized;
        rockRB.AddForce(rSphere.normalized * 5, ForceMode.Impulse);
        myRock.transform.parent = null;
    }
    public void HUDDeath()
    {
        if(!dead)
        {
            GameObject.FindWithTag("buddy").GetComponent<CameraScript>().playerList.Remove(transform);
            uIElement.transform.Find("PlayerIcon").GetComponent<Image>().color = Color.black;
            Instantiate(mySkeleton, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            fire.Stop();
            fire.transform.parent = null;
            dead = true;
            Destroy(fire, 5);
            Destroy(gameObject);
        }
    }
    public void ChangePlayerData(PlayerData targetData)
    {
        Debug.Log("Changing Data");
        Destroy(mesh);
        playerData = targetData;
        mesh = Instantiate(playerData.characterMesh, transform);
        anim = mesh.GetComponent<Animator>();
        limbScript = mesh.GetComponent<LimbSwitcherScript>();
        uIElement.transform.Find("PlayerIcon").GetComponent<Image>().sprite = playerData.characterSprite;
        aud = mesh.GetComponent<AudioScript>();
        fire = limbScript.myFire;
        limbScript.initializeLimbs();
        ChangeState(new MoveState("Move"));
    }
}
