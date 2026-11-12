using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneratorInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TriggerDetector triggerDetector;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject popup;
    [SerializeField] private PipePuzzle puzzle;
    [SerializeField] private ParticleSystem generatorSparks;

    [Header("Settings")]
    [SerializeField] private float closeDelay = 0.8f;

    public bool solved = false;
    private bool open;

    private void Start()
    {
        popup.SetActive(false);
        if (generatorSparks != null) generatorSparks.Stop();
        puzzle.Solved += OnSolved;
    }

    private void OnDestroy()
    {
        if (puzzle != null) puzzle.Solved -= OnSolved;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (solved || Keyboard.current == null) return;

        if (prompt != null)
            prompt.SetActive(triggerDetector.playerInRange && !open);

        if (!open)
        {
            if (triggerDetector.playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
                Open();
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Close();
        }
    }

    private void Open()
    {
        open = true;
        popup.SetActive(true);
        puzzle.Begin();
        Time.timeScale = 0f; 
    }

    private void Close()
    {
        open = false;
        popup.SetActive(false);
        puzzle.End();
        Time.timeScale = 1f;
    }

    private void OnSolved()
    {
        solved = true;
        if (prompt != null) prompt.SetActive(false);
        StartCoroutine(FinishRoutine());
    }

    private IEnumerator FinishRoutine()
    {
        yield return new WaitForSecondsRealtime(closeDelay);
        Close();
        if (generatorSparks != null) generatorSparks.Play();
    }
}