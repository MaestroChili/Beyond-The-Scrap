using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class RobotState
{
    protected RobotController robot;

    public RobotState(RobotController robot)
    {
        this.robot = robot;
    }

    public virtual void OnStateEnter() { }

    public virtual void Update() { }

    public virtual void OnStateExit() { }

    public void MoveHorizontally() {
        // Get the input from the InputActions -> Player -> Movement vector
        // Use the inputs x (horizontal) value to determine the velocity for the current frame
        // Mathf.MoveTowards is used to make a smooth dampened acceleration and deceleration
        Vector2 moveInput = robot.inputActions.Player.Movement.ReadValue<Vector2>();
        if (moveInput != Vector2.zero) {
            robot.velocity.x = Mathf.MoveTowards(robot.velocity.x, robot.horizontalSpeed * moveInput.x, robot.acceleration);
        } else {
            robot.velocity.x = Mathf.MoveTowards(robot.velocity.x, 0, robot.acceleration);
        }
    }

    public void JumpAndFall() {
        /// Get the input from inputActions -> Player -> Jump action
        /// if jumpAcion was pressed and the coyote timer is still running
        ///     reset jump timers, jump and switch to jupm state
        /// else if the robot is in in the air
        ///     robot has left ground
        ///     robot switches to falling state
        bool jumpPressed = robot.jumpBufferCounter > 0f;
        if (jumpPressed && robot.coyoteCounter > 0f) {
            robot.jumpBufferCounter = 0f;
            robot.coyoteCounter = 0f;
            robot.velocity.y = robot.jumpForce;
            robot.SwitchState(new Jumping(robot));
        } else if (!robot.grounded) {
                robot.hasLeftGround = true;
                robot.SwitchState(new Falling(robot));
        }
    }
}