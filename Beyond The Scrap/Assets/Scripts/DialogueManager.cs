
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    //public GameObject interactPrompt;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    private int currentLine = 0;
    private bool dialogueActive = false;
    private bool introDialogueComplete = false;
    private bool isIntroDialogue = false;

    private Coroutine typingCoroutine;
    public float typingSpeed = 0.03f;
    private bool isTyping = false;
    private string currentSentence;

    // CURRENT DIALOGUE
    private string[] currentDialogue;
    private string[] currentSpeakers;

    public bool IsTyping() { return isTyping; }

    // INTRO DIALOGUE (CRANK)
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

    // DEFAULT DIALOGUE (CRANK)
    private string[] defaultDialogue =
    {
        "You got the Parts?",
        "Go away!"
    };

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // CRANK DIALOGUE
    public void StartDialogue()
    {
        dialogueActive = true;
        currentLine = 0;

        dialoguePanel.SetActive(true);
        //interactPrompt.SetActive(true);

        if (!introDialogueComplete)
        {
            isIntroDialogue = true;

            currentDialogue = introDialogue;
            currentSpeakers = introSpeakers;

            ShowCurrentLine();
        }
        else
        {
            isIntroDialogue = false;

            int randomDialogue = Random.Range(0, defaultDialogue.Length);

            currentDialogue = new string[]
            {
                defaultDialogue[randomDialogue]
            };

            currentSpeakers = new string[] { "Crank" };

            ShowCurrentLine();
        }
    }

    // NEW NPC DIALOGUE
    public void StartCustomDialogue(string speaker, string[] dialogue)
    {

        dialogueActive = true;
        isIntroDialogue = false;
        currentLine = 0;

        currentDialogue = dialogue;

        currentSpeakers = new string[dialogue.Length];

        for (int i = 0; i < dialogue.Length; i++)
        {
            currentSpeakers[i] = speaker;
        }

        dialoguePanel.SetActive(true);
        //interactPrompt.SetActive(true);

        ShowCurrentLine();
    }

    // ADVANCE DIALOGUE
    public void NextLine()
    {
        // Finish typing the current sentence first
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        currentLine++;

        if (currentLine >= currentDialogue.Length)
        {
            if (isIntroDialogue)
            {
                introDialogueComplete = true;
            }

            EndDialogue();
            return;
        }

        ShowCurrentLine();
    }

    // DISPLAY CURRENT LINE
    private void ShowCurrentLine()
    {
        speakerText.text = currentSpeakers[currentLine];

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(
            TypeDialogue(currentDialogue[currentLine])
        );
    }

    // STEAM REACTION
    public void SteamHit()
    {
        if (dialogueActive)
        {
            return;
        }

        StartCustomDialogue(
            "NPC",
            new string[]
            {
                "OW! HOT! HOT! HOT!"
            }
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

    // TYPEWRITER EFFECT
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
