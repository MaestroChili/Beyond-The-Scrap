using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
}
