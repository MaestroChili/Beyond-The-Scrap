using UnityEngine;

public class Pipe : HeatItem
{
    [SerializeField]
    private HeatItem source;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        
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
    }
}
