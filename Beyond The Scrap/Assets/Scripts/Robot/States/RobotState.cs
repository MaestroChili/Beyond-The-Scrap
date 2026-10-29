public abstract class RobotState
{
    protected RobotController robot;

    public RobotState(RobotController robot)
    {
        this.robot = robot;
    }

    public virtual void OnStateEnter() { }

    public virtual void Update() { 
        
    }

    public virtual void OnStateExit() { }
}