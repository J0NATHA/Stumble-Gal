using UnityEngine;

public class Walking : State
{
    private PlayerController controller;
    public Walking(PlayerController controller) : base("Walking")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (controller.hasJumpInput)
        {
            controller.stateMachine.ChangeState(controller.jumpingState);
            return;
        }

        if (controller.movementVector.IsZero())
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

        Vector3 movementVector3 = new Vector3(controller.movementVector.x, 0f, controller.movementVector.y);
        movementVector3 = controller.GetForward() * movementVector3;
        movementVector3 *= controller.speed;
        

        controller.thisRigidBody.AddForce(movementVector3, ForceMode.Force);
        controller.RotateBodyToFaceInput();
    }
}

