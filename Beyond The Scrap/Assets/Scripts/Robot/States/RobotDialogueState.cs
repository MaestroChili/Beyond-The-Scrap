using UnityEngine;

public class RobotDialogueState : RobotState
{
    public RobotDialogueState(RobotController robot) : base(robot)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Dialogue State");

        // Stop all movement
        robot.velocity = Vector2.zero;
    }

    public override void Update()
    {
        // Keep robot stationary during dialogue
        robot.velocity = Vector2.zero;
    }

    public override void OnStateExit()
    {
        Debug.Log("Exited Dialogue State");
    }
}