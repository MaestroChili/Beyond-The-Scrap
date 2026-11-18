
using UnityEngine;

public class FuseboxInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject fuseboxPopup;
    [SerializeField] private Trapdoor trapdoor;
    [SerializeField] private FuseboxPuzzle fuseboxPuzzle;
    private bool playerInRange = false;

    void Start()
    {
        interactPrompt.SetActive(false);
        fuseboxPopup.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool openPopup = !fuseboxPopup.activeSelf;

            if (!openPopup)
            {
                if (fuseboxPuzzle.IsCompleted)
                {
                    trapdoor.Open();
                }
                else
                {
                    fuseboxPuzzle.ResetPuzzleState();
                }
            }

            fuseboxPopup.SetActive(openPopup);
            interactPrompt.SetActive(!openPopup);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactPrompt.SetActive(true);
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         playerInRange = false;
    //         interactPrompt.SetActive(false);
    //         fuseboxPopup.SetActive(false);
    //     }
    // }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactPrompt.SetActive(false);

            if (!fuseboxPuzzle.IsCompleted)
            {
                fuseboxPuzzle.ResetPuzzleState();
            }

            fuseboxPopup.SetActive(false);
        }
    }
}
