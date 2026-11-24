using UnityEngine;

public class TimedValve : HeatItem
{

    [SerializeField] private HeatItem source;
    [SerializeField] private bool turnedOn = false;
    public GameObject prompt;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private float maxTemp = 99.9f;

    [SerializeField] private TriggerDetector triggerDetector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        prompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (source.hot && turnedOn) {
            HeatOn();
        } else {
            HeatOff();
        }

        if (this.heat > maxTemp) {
            HeatOff();
            turnedOn = false;
            sr.sprite = offSprite;
        }
        ControlTemperature();
        UpdateColor();

        prompt.SetActive(triggerDetector.playerInRange);

        if (triggerDetector.playerInRange) {
            // display the interaction menu
            if (Input.GetKeyDown(KeyCode.E) && !turnedOn)
            {
                ToggleValve();
                sr.sprite = onSprite;
            }
        }
    }

    public void ToggleValve() {
        turnedOn = !turnedOn;
    }
}
