using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : RobotState
{

    private float airHoriVelo;

    public Walking(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Walking State");
    }

    public override void Update() {
        // Get the input from the ImputActions player -> movement vector
        Vector2 moveInput = robot.inputActions.Player.Movement.ReadValue<Vector2>();
        if (moveInput != Vector2.zero) {
            airHoriVelo = Mathf.MoveTowards(robot.velocity.x, robot.horizontalSpeed * moveInput.x, robot.acceleration);
        } else {
            airHoriVelo = Mathf.MoveTowards(robot.velocity.x, 0, robot.acceleration);
        }

        Debug.Log(airHoriVelo);
        robot.velocity.x = airHoriVelo;

        bool jumpPressed = robot.inputActions.Player.Jump.WasPressedThisFrame();
        if (jumpPressed) {
            // Launch the robot up. Probably adding vertical  velocity to current velocity.
            Debug.Log("Jump Button Pressed");
            // Switch to jumping state
        }
    }

    public override void OnStateExit() {
        Debug.Log("Exited Walking State");
    }
}
