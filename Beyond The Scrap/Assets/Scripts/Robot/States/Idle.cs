using UnityEngine;

public class Idle : RobotState
{

    public Idle(RobotController robot) : base(robot)
    {
        
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Idle State");
    }

    public override void Update() {
        if (robot.inputActions.Player.Movement.ReadValue<Vector2>() != Vector2.zero) {
            robot.SwitchState(new Walking(robot));
        }
        JumpAndFall();
        MoveHorizontally();
    }

    public override void OnStateExit() {
        Debug.Log("Exited Idle State");
    }
}
