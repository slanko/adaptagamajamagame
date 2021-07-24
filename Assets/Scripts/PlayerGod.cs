using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGod : MonoBehaviour
{
    public RootState curState { get; private set; }
    Rigidbody rb = null;
    private bool inShove = false;

    void Start()
    {
        Initialize(new IdleState("Idle"));
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ouchie")
        {
            curState.OnBonked(this);
        }
        else if(other.tag == "Shovey")
        {
            Vector3 dir = (transform.position - other.transform.position).normalized;
            curState.OnShoved(this, 5, dir);
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
    }

    private void FixedUpdate()
    {
        curState.PhysicsUpdate(this);
    }

    public Vector2 moveVal = Vector2.zero;
    [SerializeField]
    float speed = 5;
    Vector3 storedVel = Vector3.zero;
    public void Move()
    {
        print("hit");
        Vector3 Move3 = new Vector3(moveVal.x, 0, moveVal.y);
        rb.velocity = Move3 * speed + storedVel;
        storedVel = rb.velocity * 0.1f;
        Vector3 moveDir = (moveVal.x * Vector3.right) + (moveVal.y * Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.25F);
    }
    public void Stop()
    {
        if (!inShove)
        {
            rb.velocity *= 0.8f;
        }
    }

    public void Fall()
    {

    }
    public void Shove(float force, Vector3 direction)
    {
        if (!inShove)
        {
            inShove = true;
            StartCoroutine(ShoveCD());
            rb.AddForce(direction * force, ForceMode.Impulse);
            Debug.Log("Being shoved with " + force);
        }
    }
    IEnumerator ShoveCD()
    {
        yield return new WaitForSeconds(0.5f);
        inShove = false;
    }
    public void GetUp()
    {

    }

}
