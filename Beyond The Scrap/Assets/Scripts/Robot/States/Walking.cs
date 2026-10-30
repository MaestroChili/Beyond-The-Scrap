using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : RobotState
{

    //private float horiVelo;

    public Walking(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Walking State");
    }

    public override void Update() {

        MoveHorizontally();

        // Get the input from inputActions -> Player -> Jump action
        bool jumpPressed = robot.inputActions.Player.Jump.WasPressedThisFrame();
        if (jumpPressed && robot.grounded) {
            Debug.Log("jump");
            // Launch the robot up. Probably adding vertical  velocity to current velocity.
            robot.velocity.y = robot.jumpForce;
            robot.SwitchState(new Jumping(robot));
            // Switch to jumping state
        } else {
            jumpPressed = false;
            robot.velocity.y = 0;
            // Check if we've left the ground yet
            if (!robot.grounded) {
                robot.hasLeftGround = true;
                robot.SwitchState(new Falling(robot));
            }
        }
    }

    public override void OnStateExit() {
        Debug.Log("Exited Walking State");
    }
}
