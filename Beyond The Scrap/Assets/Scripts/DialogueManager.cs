using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject interactPrompt;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    private int currentLine = 0;
    private bool dialogueActive = false;
    private bool introDialogueComplete = false;

    private Coroutine typingCoroutine;
    public float typingSpeed = 0.03f;
    private bool isTyping = false;
    private string currentSentence;
    // INTRO DIALOGUE
    private string[] introSpeakers =
    {
        "Crank",
        "Clanker",
        "Crank",
        "Clanker",
        "Crank",
        "Clanker",
        "Crank",
        "Clanker",
        "Crank",
        "Clanker",
        "Crank",
        "Clanker",
        "Crank",
        "Crank",
        "Crank"
    };


    private string[] introDialogue =
    {
        "Whatcha looking at!",
        "I'm eyeing your blimp...",
        "It's an old piece of art, but it's broken down and unusable.",
        "Can it fly?",
        "You lost your hearing or something?! I just said it's broken down!",
        "Can we fix it?",
        "I tried for half my life, but I'm too old and creaky to get all the parts I need.",
        "If I got you the parts, could you fix it and fly us out of town?",
        "Hm... Where do you want to go, kid?",
        "Aren't you curious of what's outside our town? Maybe there'll be more than just... trash?",
        "Too old to care.",
        "There is so much to see behind these heaps of trash.",
        "Hm... I'll indulge in your young endeavors...",
        "Now bring me the parts and oil while you're at it...",
        "*mumbles* These damn joints."
    };

    // DEFAULT DIALOGUE
    private string[] defaultDialogue =
    {
        "You got the Parts?",
        "Go away!"
    };


    private void Start()
    {
        dialoguePanel.SetActive(false);
    }


    // Called when Clanker presses E near Crank
    public void StartDialogue()
    {
        dialogueActive = true;
        currentLine = 0;

        dialoguePanel.SetActive(true);
        interactPrompt.SetActive(true);

        // FIRST TIME TALKING
        if (!introDialogueComplete)
        {
            ShowIntroLine();
        }

        // AFTER INTRODUCTION
        else
        {
            ShowDefaultDialogue();
        }
    }


    // INTRO DIALOGUE
    public void NextLine()
    {
        // If text is still typing, show the entire sentence
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        // Only advance the intro conversation
        if (!introDialogueComplete)
        {
            currentLine++;

            if (currentLine >= introDialogue.Length)
            {
                introDialogueComplete = true;
                EndDialogue();
                return;
            }

            ShowIntroLine();
        }

        // Default dialogue is only one line
        else
        {
            EndDialogue();
        }
    }


    private void ShowIntroLine()
    {
        speakerText.text = introSpeakers[currentLine];

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(
            TypeDialogue(introDialogue[currentLine])
        );
    }


    // DEFAULT DIALOGUE
    private void ShowDefaultDialogue()
    {
        speakerText.text = "Crank";

        int randomDialogue =
            Random.Range(0, defaultDialogue.Length);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(
            TypeDialogue(defaultDialogue[randomDialogue])
        );
    }

    // END DIALOGUE

    private void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;
        dialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    private IEnumerator TypeDialogue(string sentence)
    {
        currentSentence = sentence;
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}