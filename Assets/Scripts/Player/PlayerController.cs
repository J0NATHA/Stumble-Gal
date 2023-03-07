using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpMovementFactor = 1f;

    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpingState;
    [HideInInspector] public Dead deadState;

    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public Rigidbody thisRigidBody;
    [HideInInspector] public CapsuleCollider thisCollider;

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public bool isGrounded;
    private void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        thisCollider = GetComponent<CapsuleCollider>();   
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpingState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver)
        {
            if(stateMachine.currentStateName != deadState.name)
            {
                stateMachine.ChangeState(deadState);
            }
        }


        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1f : isLeft ? -1f : 0f;
        float inputY = isUp ? 1f : isDown ? -1f : 0f;
        movementVector = new Vector2(inputX, inputY);
        hasJumpInput = Input.GetKey(KeyCode.Space);

        float velocity = thisRigidBody.velocity.magnitude;
        float velocityRate = velocity / speed;
        animator.SetFloat("fVelocity", velocityRate);


        DetectGround();

        stateMachine.Update();
    }

    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0f, eulerY, 0f);
    }

    public void RotateBodyToFaceInput()
    {
        if (movementVector.IsZero()) return;

        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0f, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up); 
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0f);    
        Quaternion toRotation = q1 * q2;
       
        var newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, .15f);
        thisRigidBody.MoveRotation(newRotation);
    }

    private void DetectGround()
    {
        isGrounded = false;

        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.5f;
        float maxDistance = bounds.size.y * 0.25f;

        if(Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance ))
        {
            GameObject hitGameObject = hitInfo.transform.gameObject;
            if (hitGameObject.CompareTag("Platform"))
                isGrounded = true;
        }

    }


}
