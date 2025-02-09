using System.Collections;
using UnityEngine;

public class ReadTextOnProximity : MonoBehaviour
{
    // Reference to the GameObject that holds the text (could be a UI element or world space text)
    public GameObject textObject;

    // Flag to track if the player is within range
    private bool playerInRange = false;

    // Flag to prevent multiple coroutine calls
    private bool isCoroutineRunning = false;

    void Start()
    {
        // Ensure the text is hidden at the start
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }

    void Update()
    {
        // When the player is near and presses the X key, start the coroutine to show the text after 6 seconds
        if (playerInRange && Input.GetKeyDown(KeyCode.X) && !isCoroutineRunning)
        {
            if (textObject != null)
            {
                StartCoroutine(ShowTextAfterDelay(6f));
            }
        }
    }

    // Coroutine that waits for a specified delay before setting the text active
    private IEnumerator ShowTextAfterDelay(float delay)
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(delay);
        textObject.SetActive(true);
        isCoroutineRunning = false;
    }

    // Called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Optionally, check for a specific tag like "Player"
        Debug.Log("krob");
        playerInRange = true;
    }

    // Called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        // Optionally, hide the text when the player leaves the area
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }
}