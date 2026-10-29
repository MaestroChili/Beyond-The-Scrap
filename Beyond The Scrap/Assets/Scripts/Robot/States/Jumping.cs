using UnityEngine;

public class Jumping : RobotState
{

    private float airHoriVelo;

    public Jumping(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Jumping State");
    }

    public override void Update() {
        // Get the input from the InputActions -> Player -> Movement vector
        // Use the inputs x (horizontal) value to determine the velocity for the current frame
        // Mathf.MoveTowards is used to make a smooth dampened acceleration and deceleration
        Vector2 moveInput = robot.inputActions.Player.Movement.ReadValue<Vector2>();
        if (moveInput != Vector2.zero) {
            airHoriVelo = Mathf.MoveTowards(robot.velocity.x, robot.horizontalSpeed * moveInput.x, robot.acceleration);
        } else {
            airHoriVelo = Mathf.MoveTowards(robot.velocity.x, 0, robot.acceleration);
        }

        // Update The Robots velocity on the horizontal axis based on calculation
        robot.velocity.x = airHoriVelo;

        if (robot.grounded) {
            robot.SwitchState(new Walking(robot));
        }

    }

    public override void OnStateExit() {
        Debug.Log("Exited Jumping State");
    }

}
