using UnityEngine;

public class Falling : RobotState
{

    //private float airHoriVelo;


    private bool hasLeftGround = false;

    public Falling(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Falling State");
    }

    public override void Update() {

        MoveHorizontally();

        // Only land if we left the ground first, are now grounded, and aren't moving upward anymore
        if (robot.hasLeftGround && robot.grounded && robot.velocity.y <= 0.1f) {
            robot.SwitchState(new Walking(robot));
        }
    }

    public override void OnStateExit() {
        Debug.Log("Exited Falling State");
    }
}
