using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : RobotState
{

    public Walking(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Walking State");
    }

    public override void Update() {
        MoveHorizontally();
        JumpAndFall();
        if (robot.inputActions.Player.Movement.ReadValue<Vector2>() == Vector2.zero) {
            robot.SwitchState(new Idle(robot));
        }
    }

    public override void OnStateExit() {
        Debug.Log("Exited Walking State");
    }
}
