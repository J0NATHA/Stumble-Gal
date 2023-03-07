using UnityEngine;

public class Jump : State
{
    private PlayerController controller;
    private bool hasJumped;
    private float cooldown;
    public Jump(PlayerController controller) : base("Jump")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        hasJumped = false;
        cooldown = .5f;

        controller.animator.SetBool("isJumping", true);

    }

    public override void Exit()
    {
        base.Exit();

        controller.animator.SetBool("isJumping", false);
    }

    public override void Update()
    {
        base.Update();

        cooldown -= Time.deltaTime;

        if(hasJumped && controller.isGrounded && cooldown <= 0)
        {
            controller.stateMachine.ChangeState(controller.idleState);
            
            return;
        }

    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!hasJumped)
        {
            hasJumped = true;
            ApplyImpulse();
        }

        Vector3 movementVector3 = new Vector3(controller.movementVector.x, 0f, controller.movementVector.y);
        movementVector3 = controller.GetForward() * movementVector3;
        movementVector3 *= controller.speed * controller.jumpMovementFactor;


        controller.thisRigidBody.AddForce(movementVector3, ForceMode.Force);
        controller.RotateBodyToFaceInput();
    }

    private void ApplyImpulse()
    {
        Vector3 forceVector = Vector3.up * controller.jumpForce;
        controller.thisRigidBody.AddForce(forceVector, ForceMode.Impulse);
    }

    

    
}
