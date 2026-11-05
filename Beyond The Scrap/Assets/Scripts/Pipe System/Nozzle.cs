using UnityEngine;

public class Nozzle : HeatItem
{
    [SerializeField]
    private HeatItem source;
    private Collider2D steamCollider;
    public ParticleSystem ps;


    void Start() {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        steamCollider = GetComponentInChildren<Collider2D>();
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
        if (this.heat >= 50f) {
            steamCollider.enabled = true;
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
}
