
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

        if (switchIndex == correctOrder[currentStep] - 1)
        {
            currentStep++;

            if (currentStep == correctOrder.Length)
            {
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
                switchAnimators[i].SetTrigger("Reset");
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
}
