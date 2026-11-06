using UnityEngine;

public class Boiler : HeatItem
{
    public GameObject prompt;
    [SerializeField]
    private TriggerDetector triggerDetector;

    [SerializeField]
    private Sprite hotSprite;
    [SerializeField]
    private Sprite coldSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hot) {
            sr.sprite = hotSprite;
        } else {
            sr.sprite = coldSprite;
        }

        prompt.SetActive(triggerDetector.playerInRange);

        if (triggerDetector.playerInRange) {
            // display the interaction menu
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleHeat();
            }
        }
    }
}
