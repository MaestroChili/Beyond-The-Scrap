using UnityEngine;

public class Valve : HeatItem
{

    [SerializeField]
    private HeatItem source;
    [SerializeField]
    private bool turnedOn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (source.hot && turnedOn) {
            // change sprite to show that it is on
            HeatOn();
        } else {
            HeatOff();
        }
        ControlTemperature();
        UpdateColor();
    }

    public void ToggleValve() {
        turnedOn = !turnedOn;
    }
}
