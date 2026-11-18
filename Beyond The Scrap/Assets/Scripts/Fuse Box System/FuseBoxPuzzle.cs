
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FuseboxPuzzle : MonoBehaviour
{
    [SerializeField] private Button[] switchButtons = new Button[6];
    [SerializeField] private Animator[] switchAnimators = new Animator[6];
    [SerializeField] private int[] correctOrder = { 1, 2, 3, 4, 5, 6 };
    [SerializeField] private float resetAnimationDuration = 1f;
    [SerializeField] private GameObject completionMessage;
    public bool IsCompleted { get; private set; } = false;
    private bool[] isFlipped = new bool[6];
    private int currentStep = 0;
    private bool isResetting = false;

    void Start()
    {
        completionMessage.SetActive(false);
        
        for (int i = 0; i < switchButtons.Length; i++)
        {
            int switchIndex = i;

            switchButtons[i].onClick.AddListener(() => ClickSwitch(switchIndex));
        }
    }

    void ClickSwitch(int switchIndex)
    {
        if (isResetting || currentStep >= correctOrder.Length)
        {
            return;
        }

        switchAnimators[switchIndex].SetTrigger("Flip");
        isFlipped[switchIndex] = true;
        switchButtons[switchIndex].interactable = false;

        Debug.Log("Pressed switch: " + (switchIndex + 1) +
          " | Current step: " + currentStep +
          " | Expected switch: " + correctOrder[currentStep]);

        if (switchIndex == correctOrder[currentStep] - 1)
        {
            currentStep++;

            if (currentStep == correctOrder.Length)
            {
                IsCompleted = true;
                completionMessage.SetActive(true);
                Debug.Log("Fusebox puzzle completed!");
            }
        }
        else
        {
            StartCoroutine(ResetPuzzle());
        }
    }

    IEnumerator ResetPuzzle()
    {
        isResetting = true;
        yield return new WaitForSeconds(resetAnimationDuration);

        for (int i = 0; i < switchButtons.Length; i++)
        {
            switchButtons[i].interactable = false;

            if (isFlipped[i])
            {
                switchAnimators[i].Play("Switch_Reset", 0, 0f);
            }
        }

        yield return new WaitForSeconds(resetAnimationDuration);

        for (int i = 0; i < switchButtons.Length; i++)
        {
            isFlipped[i] = false;
            switchButtons[i].interactable = true;
        }

        currentStep = 0;
        isResetting = false;
    }

    public void ResetPuzzleState()
    {
        StopAllCoroutines();

        for (int i = 0; i < switchButtons.Length; i++)
        {
            isFlipped[i] = false;
            switchButtons[i].interactable = true;

            switchAnimators[i].ResetTrigger("Flip");
            switchAnimators[i].ResetTrigger("Reset");

            switchAnimators[i].Play("Off", 0, 0f);
            switchAnimators[i].Update(0f);
        }

        currentStep = 0;
        isResetting = false;
        IsCompleted = false;

        completionMessage.SetActive(false);
    }
}
