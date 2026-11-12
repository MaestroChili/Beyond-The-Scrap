using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class PipeTile : MonoBehaviour, IPointerClickHandler
{
    public enum Kind { Straight, Corner }

    [SerializeField] private Kind kind;
    [SerializeField] private float turnSpeed = 720f;

    [Header("Electric look (pulses between the two colors while powered)")]
    [SerializeField] private Color electricA = new Color(1f, 0.95f, 0.4f);
    [SerializeField] private Color electricB = new Color(0.4f, 0.9f, 1f);
    [SerializeField] private float pulseSpeed = 10f;

    public event Action<PipeTile> Clicked;
    public int Rotation { get; private set; }

    private Image image;
    private Quaternion targetRotation = Quaternion.identity;
    private bool powered;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetRotation(int quarterTurns)
    {
        Rotation = ((quarterTurns % 4) + 4) % 4;
        targetRotation = Quaternion.Euler(0f, 0f, -90f * Rotation);
        transform.localRotation = targetRotation;
    }

    public void RotateClockwise()
    {
        Rotation = (Rotation + 1) % 4;
        targetRotation = Quaternion.Euler(0f, 0f, -90f * Rotation);
    }

    public bool Has(int direction)
    {
        int local = ((direction - Rotation) % 4 + 4) % 4;
        return kind == Kind.Straight
            ? (local == 1 || local == 3)
            : (local == 0 || local == 1);
    }

    public void SetPowered(bool value)
    {
        powered = value;
        if (!powered && image != null) image.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Clicked?.Invoke(this);
    }

    private void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation, targetRotation, turnSpeed * Time.unscaledDeltaTime);

        if (powered)
        {
            float t = 0.5f + 0.5f * Mathf.Sin(Time.unscaledTime * pulseSpeed);
            image.color = Color.Lerp(electricA, electricB, t);
        }
    }
}