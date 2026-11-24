using System.Collections;
using UnityEngine;

public class Nozzle : HeatItem
{
    [SerializeField]
    private HeatItem source;
    private Collider2D steamCollider;
    public ParticleSystem ps;

    [SerializeField] private float launchForce = 20f;

    [SerializeField] private TriggerDetector triggerDetector;


    void Start() {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        steamCollider = GetComponentInChildren<Collider2D>();
        steamCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (source.hot) {
            HeatOn();
        } else {
            HeatOff();
        }
        ControlTemperature();
        UpdateColor();
        ControlSteam();
    }

    public void ControlSteam() {
        if (source.heat >= 50f) {
            steamCollider.enabled = true;
            Debug.Log("nozzle on");
            if (!ps.isPlaying) {
                ps.Play();
            }
        } else {
            steamCollider.enabled = false;
            if (ps.isPlaying) {
                ps.Stop();
            }
        }
    }

    public void LaunchObject()
    {
        Vector2 launchDirection = steamCollider.transform.up;
        if (triggerDetector.triggeringObject != null)
        {
            Rigidbody2D rb = triggerDetector.triggeringObject.rb;
            rb.linearVelocity = launchDirection * launchForce;
        }

        if (triggerDetector.triggeringRobot != null)
        {
            RobotController robot = triggerDetector.triggeringRobot;
            robot.velocity += launchDirection * launchForce;
            robot.SwitchState(new Jumping(robot));
        }
    }
}
