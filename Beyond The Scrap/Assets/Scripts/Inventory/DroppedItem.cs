using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DroppedItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    bool autoStart;

    [SerializeField]
    float enabledPickupDelay = 3.0f;

    [Header("State")]
    public Item item;
    public bool pickedUp = false;

    void Start()
    {
        if (autoStart && item != null)
        {
            Initialize(item);
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
        var droppedItem = Instantiate(item.prefab, transform);
        droppedItem.transform.SetLocalPositionAndRotation(new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        StartCoroutine(EnablePickup(enabledPickupDelay));
    }

    IEnumerator EnablePickup(float dealy)
    {
        yield return new WaitForSeconds(dealy);
        GetComponent<Collider2D>().enabled = true;
    }
}
