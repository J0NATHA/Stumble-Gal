using UnityEngine;

public class Dead : State
{
    PlayerController controller;
    public Dead(PlayerController controller) : base("Dead")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        controller.animator.SetTrigger("TGameOver");
    }

   
    
}
