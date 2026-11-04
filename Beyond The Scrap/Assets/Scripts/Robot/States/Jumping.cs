using UnityEngine;

public class Jumping : RobotState
{
    // private float airHoriVelo;
    // private float horiVelo;

    public Jumping(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Jumping State");
    }

    public override void Update() {

        robot.hasLeftGround = true;

        MoveHorizontally();
        

        if (robot.velocity.y <= 0f) {
            robot.SwitchState(new Falling(robot));
        }        
    }

    public override void OnStateExit() {
        Debug.Log("Exited Jumping State");
    }
}