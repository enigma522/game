using System.Collections;
using TMPro;
using UnityEngine;

public class EndMazeTrigger : MonoBehaviour
{
    public Light transitionLight;  // This will be the point light at the end of the maze
    public float lightIncreaseSpeed = 2f;  // Speed at which light increases
    public float lightDecreaseSpeed = 2f; // Speed at which light decreases
    public float motherDescendSpeed = 1f; // Speed at which the mother descends
    private bool playerInside = false;  // Checks if the player is inside the trigger zone

    // You can modify the global ambient light for the entire scene
    public Color glowingAmbientColor = Color.white;  // The color for glowing effect (white here)

    public GameObject Mother;
    public float targetY = 5.3f;  // The target Y position to which the mother will descend

    private bool motherAppeared = false;  // Flag to check if mother has already appeared
    private bool motherDescended = false;  // Flag to check if mother has fully descended

    // Reference to the right shoulder (or right arm) bone in the mother prefab's hierarchy
    public Transform rightShoulder;

    public Transform leftShoulder;

    void Start()
    {
        if (Mother != null)
        {
            Mother.SetActive(false);
        }
        else
        {
            Debug.Log("Mother is not set in the inspector");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ch29_nonPBR new")
        {
            playerInside = true;
            PlayerMovement2 movementScript = other.gameObject.GetComponent<PlayerMovement2>();
            if (movementScript != null)
            {
                movementScript.animator.SetBool("moving", false);
                movementScript.enabled = false;
            }
        }
    }

    void Update()
    {
        if (playerInside)
        {
            // Gradually increase the light intensity
            transitionLight.intensity = Mathf.Lerp(transitionLight.intensity, 100100f, lightIncreaseSpeed * Time.deltaTime);

            transitionLight.color = Color.white;
        }

        // When light intensity exceeds the threshold and the mother hasn't appeared yet
        if (transitionLight.intensity >= 100000f && !motherAppeared)
        {
            motherAppeared = true;  // Ensure the mother only appears once

            // Activate the mother
            if (Mother != null)
            {
                Mother.SetActive(true);
                playerInside = false;
            }
        }

        // After the mother appears, gradually reduce light intensity and make the mother descend
        if (motherAppeared && !motherDescended)
        {

            // Slowly reduce the light intensity to 0
            transitionLight.intensity = Mathf.Lerp(transitionLight.intensity, 0f, lightDecreaseSpeed * Time.deltaTime);

            // Move the mother down the Y-axis to the target Y position
            float currentY = Mother.transform.position.y;
            Debug.Log("currentY: " + currentY);

            // Smoothly move the mother to the target Y position
            float newY = Mathf.Lerp(currentY, targetY, motherDescendSpeed * Time.deltaTime);
            Mother.transform.position = new Vector3(Mother.transform.position.x, newY, Mother.transform.position.z);

            // Check if the mother has reached the target Y position with a small tolerance
            if (Mathf.Abs(newY - targetY) < 0.01f)  // Small threshold to handle floating-point precision
            {
                Debug.Log("Mother reached the target Y position");

                // Set the flag to indicate the mother has fully descended
                motherDescended = true;

                // Optionally, you can stop light intensity from reducing further
                transitionLight.intensity = 0f;
            }

            Debug.Log(rightShoulder);

            // Apply arm movement (extend right shoulder) as the mother descends
            if (rightShoulder != null)
            {
                // Calculate the target rotation for the right shoulder (simulating an arm extending out)
                Quaternion targetRotation = Quaternion.Euler(-90f, -45f, 90f);  // Adjust the angle to fit your scene (this is an example)

                // Smoothly rotate the right shoulder towards the target rotation
                rightShoulder.rotation = Quaternion.Lerp(rightShoulder.rotation, targetRotation, 0.1f);  // Adjust smoothing factor as needed
            }

            if (leftShoulder != null)
            {
                // Calculate the target rotation for the left shoulder (simulating an arm extending out)
                Quaternion targetRotation = Quaternion.Euler(-90f, 45f, -90f);  // Adjust the angle to fit your scene (this is an example)

                // Smoothly rotate the left shoulder towards the target rotation
                leftShoulder.rotation = Quaternion.Lerp(leftShoulder.rotation, targetRotation, 0.1f);  // Adjust smoothing factor as needed
            }
        }

        // New part to make everything disappear and display "To Be Continued..."
        if (motherDescended)
        {
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                // Skip the glow effect AND the text/camera objects
                if (obj.gameObject.name == "Glow" || obj.gameObject.name == "EndCamera" || obj.gameObject.name == "EndText")
                {
                    continue;  // Keep these objects active
                }

                obj.SetActive(false);
            }

            // Display the text "To Be Continued..." only once
            if (GameObject.Find("EndText") == null)
            {
                DisplayText();
            }
        }
    }

    // Method to display the "To Be Continued..." text
    private void DisplayText()
    {
        // Create a new camera if none exists
        GameObject cameraObject = new GameObject("EndCamera");
        Camera endCamera = cameraObject.AddComponent<Camera>();

        // Set camera properties
        endCamera.clearFlags = CameraClearFlags.SolidColor;  // Prevent rendering the skybox if unwanted
        endCamera.backgroundColor = Color.black;  // Make the background black
        endCamera.orthographic = false;  // Use perspective mode for a normal view
        endCamera.transform.position = new Vector3(0, 1, -10);  // Adjust the position so the text is visible
        endCamera.transform.LookAt(Vector3.zero);  // Make sure the camera is looking at the center

        // Create the text object
        GameObject textObject = new GameObject("EndText");
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();

        // Set initial text properties
        textMesh.text = "";  // Start with an empty text
        textMesh.color = Color.white;  // Ensure visibility
        textMesh.fontSize = 20;
        textMesh.alignment = TextAlignmentOptions.Center;


        // Position the text in front of the camera
        textObject.transform.position = new Vector3(0, 1, 0);

        // Start the animation coroutine
        StartCoroutine(AnimateText(textMesh, "To Be Continued..."));
    }

    // Coroutine to animate text letter by letter
    private IEnumerator AnimateText(TextMeshPro textMesh, string fullText)
    {
        textMesh.text = "";  // Clear text initially

        for (int i = 0; i <= fullText.Length; i++)
        {
            textMesh.text = fullText.Substring(0, i);  // Reveal letters one by one
            yield return new WaitForSeconds(0.1f);  // Delay between letters
        }
    }
}
