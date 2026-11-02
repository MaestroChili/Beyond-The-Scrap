using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactPrompt;

    public DialogueManager dialogueManager;

    public GameObject npcPrompt;

    private bool playerInRange = false;

    private RobotController playerRobot;

    private void Start()
    {
        npcPrompt.SetActive(false);
    }

    private void Update()
    {
        // Show prompt above Crank when Clanker is nearby
        // and dialogue is not active
        npcPrompt.SetActive(playerInRange && !dialogueManager.IsDialogueActive());

        // Clanker is close to Crank
        if (playerInRange && !dialogueManager.IsDialogueActive())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Start dialogue
                dialogueManager.StartDialogue();

                // Switch to dialogue state
                playerRobot.SwitchState(new RobotDialogueState(playerRobot));
            }
        }

        // Dialogue is currently happening
        else if (dialogueManager.IsDialogueActive())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueManager.NextLine();
            }
        }

        // Dialogue has finished
        if (playerRobot != null &&
            playerRobot.IsInDialogueState() &&
            !dialogueManager.IsDialogueActive())
        {
            // Return to walking
            playerRobot.SwitchState(new Walking(playerRobot));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            playerRobot = other.GetComponent<RobotController>();

            npcPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}