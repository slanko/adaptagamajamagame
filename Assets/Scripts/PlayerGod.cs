using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGod : MonoBehaviour
{
    public RootState curState { get; private set; }
    Rigidbody rb = null;

    void Start()
    {
        Initialize(new IdleState("Idle"));
        rb = GetComponent<Rigidbody>();
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
        rb.velocity *= 0.8f;
    }



}
