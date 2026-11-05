using UnityEngine;

public abstract class HeatItem : MonoBehaviour
{

    private Collider2D collider1, collider2;
    public bool hot = false;
    public float heat = 0f;
    [SerializeField]
    private float maxHeat = 100f;
    [SerializeField]
    private float heatRate = 10f;
    [SerializeField]
    private float coolRate = 5f;

    public SpriteRenderer sr;
    public Color baseColor;
    [SerializeField]
    private float redPerHeat = 0.005f;   // red added per point of heat


    public void HeatOn(){
        this.hot = true;
    }

    public void HeatOff(){
        this.hot = false;
    }

    public void ControlTemperature() {
        if (this.hot) {
            heat = Mathf.MoveTowards(heat, maxHeat, heatRate * Time.deltaTime);
        } else {
            heat = Mathf.MoveTowards(heat, 0f, coolRate * Time.deltaTime);
        }
    }

    public void UpdateColor() {
        sr.color = Color.Lerp(baseColor, Color.red, heat * redPerHeat);
    }
}
