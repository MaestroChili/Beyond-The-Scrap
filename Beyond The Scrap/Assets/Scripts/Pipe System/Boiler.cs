using UnityEngine;

public class Boiler : HeatItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hot) {
            // change sprite to show that it is on
            return;
        } else {
            // change sprite to show that it is off
            return;
        }
    }
}
