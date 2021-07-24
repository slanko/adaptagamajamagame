using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            curState.OnShoved(this, 50, dir);
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
        Vector3 Move3 = new Vector3(moveVal.x, 0, moveVal.y);
        Vector3 holdthis = Move3 * speed + storedVel;
        rb.velocity = new Vector3(holdthis.x, rb.velocity.y, holdthis.z);
        storedVel = rb.velocity * 0.1f; 
        Vector3 moveDir = (moveVal.x * Vector3.right) + (moveVal.y * Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.25F);
    }
    public void Stop()
    {
         rb.velocity = new Vector3 (rb.velocity.x * 0.8f, rb.velocity.y, rb.velocity.z * 0.8f);
    }

    public void Fall()
    {

    }
    public void Shove(float force, Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
        inShove = true;
         Debug.Log("Being shoved with " + rb.velocity.magnitude);
    }

    public void ShoveLockout()
    {
        if (inShove)
        {
            if(rb.velocity.magnitude >= 2.5f)
            {
                rb.velocity *= 0.95f;
            }
            else
            {
                ChangeState(new MoveState("Move"));
            }
        }
    }
    public void SetStateText(string state)
    {
        Text stateDisplay = GameObject.Find("StateDisplay").GetComponent<Text>();
        stateDisplay.text = state;
    }

    public void GetUp()
    {

    }

}
