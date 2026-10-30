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
}