using UnityEngine;

public class Jumping : RobotState
{
    private float airHoriVelo;
    private bool hasLeftGround = false; // Track if we've actually cleared the floor

    public Jumping(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter() {
        Debug.Log("Entered Jumping State");
        hasLeftGround = false;
    }

    public override void Update() {
        Vector2 moveInput = robot.inputActions.Player.Movement.ReadValue<Vector2>();
        if (moveInput != Vector2.zero) {
            airHoriVelo = Mathf.MoveTowards(robot.velocity.x, robot.horizontalSpeed * moveInput.x, robot.acceleration);
        } else {
            airHoriVelo = Mathf.MoveTowards(robot.velocity.x, 0, robot.acceleration);
        }

        robot.velocity.x = airHoriVelo;

        // Check if we've left the ground yet
        if (!robot.grounded) {
            hasLeftGround = true;
        }

        // Only land if we left the ground first, are now grounded, and aren't moving upward anymore
        if (hasLeftGround && robot.grounded && robot.velocity.y <= 0.1f) {
            robot.SwitchState(new Walking(robot));
        }
    }

    public override void OnStateExit() {
        Debug.Log("Exited Jumping State");
    }
}