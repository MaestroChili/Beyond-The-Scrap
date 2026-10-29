using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : RobotState
{

    private float horiVelo;

    public Walking(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Walking State");
    }

    public override void Update() {
        // Get the input from the InputActions -> Player -> Movement vector
        // Use the inputs x (horizontal) value to determine the velocity for the current frame
        // Mathf.MoveTowards is used to make a smooth dampened acceleration and deceleration
        Vector2 moveInput = robot.inputActions.Player.Movement.ReadValue<Vector2>();
        if (moveInput != Vector2.zero) {
            horiVelo = Mathf.MoveTowards(robot.velocity.x, robot.horizontalSpeed * moveInput.x, robot.acceleration);
        } else {
            horiVelo = Mathf.MoveTowards(robot.velocity.x, 0, robot.acceleration);
        }

        // Update The Robots velocity on the horizontal axis based on calculation
        robot.velocity.x = horiVelo;

        // Get the input from inputActions -> Player -> Jump action
        bool jumpPressed = robot.inputActions.Player.Jump.WasPressedThisFrame();
        if (jumpPressed) {
            // Launch the robot up. Probably adding vertical  velocity to current velocity.
            robot.velocity.y = robot.jumpForce;
            robot.SwitchState(new Jumping(robot));
            // Switch to jumping state
        } else {
            jumpPressed = false;
            robot.velocity.y = 0;
        }
    }

    public override void OnStateExit() {
        Debug.Log("Exited Walking State");
    }
}
