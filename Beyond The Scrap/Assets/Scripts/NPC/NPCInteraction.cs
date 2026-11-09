using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactPrompt;

    public DialogueManager dialogueManager;

    public GameObject npcPrompt;

    private bool playerInRange = false;

    private RobotController playerRobot;

    private SpriteRenderer npcSprite;

    public bool isCrank = true;

    public string npcName = "NPC";

    public string[] npcDialogue =
    {
        "If you want me to move then yer gonna have to make me!"
    };
    private bool steamHit = false;
    private Animator npcAnimator;
    private bool runningAway = false;
    public float runAwaySpeed = 3f;
    private void Start()
    {
        if (dialogueManager == null)
            dialogueManager = FindFirstObjectByType<DialogueManager>();
        
        npcPrompt.SetActive(false);

        npcSprite = GetComponent<SpriteRenderer>();
        npcAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Show prompt above Crank when Clanker is nearby and dialogue is not active
        npcPrompt.SetActive(playerInRange && !dialogueManager.IsDialogueActive());

        // Clanker is close to Crank
        if (playerInRange && !dialogueManager.IsDialogueActive())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Start dialogue
                if (isCrank)
                {
                    dialogueManager.StartDialogue();
                }
                else
                {
                    dialogueManager.StartCustomDialogue(npcName, npcDialogue);
                }

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
        if (playerRobot != null && playerRobot.IsInDialogueState() && !dialogueManager.IsDialogueActive() && !runningAway)
        {
            // Return to walking
            playerRobot.SwitchState(new Walking(playerRobot));
        }
        // New NPC runs away after steam dialogue finishes
        if (!isCrank && steamHit && !dialogueManager.IsDialogueActive() && !runningAway)
        {
            StartCoroutine(RunAway());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect Clanker entering NPC's interaction range
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            playerRobot = other.GetComponent<RobotController>();

            npcPrompt.SetActive(true);
        }

        // Detect steam hitting NPC
        Nozzle nozzle = other.GetComponentInParent<Nozzle>();

        if (nozzle != null)
        {

            if (!steamHit && !isCrank)
            {

                if (!dialogueManager.IsDialogueActive())
                {
                    steamHit = true;
                    dialogueManager.SteamHit();
                }
            }
        }
    }

        private string GetFullPath(Transform current)
    {
        string path = current.name;

        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }

        return path;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator RunAway()
    {
        runningAway = true;

        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
            runningAway = false;
            yield break;
        }

        // Stop the player from moving
        if (playerRobot != null)
            playerRobot.SwitchState(new RobotDialogueState(playerRobot));

        npcAnimator.SetBool("IsWalking", true);

        float cameraDistance = Mathf.Abs(
            mainCamera.transform.position.z - transform.position.z
        );

        float rightEdge = mainCamera.ViewportToWorldPoint(
            new Vector3(1.1f, 0f, cameraDistance)
        ).x;

        while (transform.position.x < rightEdge)
        {
            transform.position += Vector3.right * runAwaySpeed * Time.deltaTime;
            yield return null;
        }

        // Destroy the NPC after it runs offscreen
        Destroy(gameObject);

        // Allow the player to move again
        if (playerRobot != null)
            playerRobot.SwitchState(new Walking(playerRobot));

        // Destroy the NPC after it runs offscreen
        Destroy(gameObject);
    }
}