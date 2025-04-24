using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerTouch : MonoBehaviour
{
    public GameObject objectToDisable; // Select the object to disable
    public TextMeshProUGUI touchCooldownText; // For displaying touch cooldown message
    public TextMeshProUGUI playerStatusText; // For displaying player status message
    public float disableDuration = 3f; // Duration for which the object will be disabled
    public float cooldownDuration = 5f; // Cooldown duration for the enemy

    private bool isPlayerDisabled = false; // Check if player is disabled
    private bool isEnemyCooldown = false; // Check if enemy cooldown is active

    private void Start()
    {
        // Display initial messages
        touchCooldownText.text = "Ready to interact"; // Changed text
        playerStatusText.text = "Player: Active"; // Changed text
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collision is with the player (make sure both player and enemy colliders are triggers)
        if (other.gameObject.CompareTag("Player") && !isEnemyCooldown)
        {
            if (!isPlayerDisabled)
            {
                // Change message when starting to disable the object
                touchCooldownText.text = "Interaction on cooldown..."; // Changed text
                playerStatusText.text = "Player disabled for " + disableDuration + "s"; // Changed text

                // Start the process of disabling the object
                StartCoroutine(DisableObjectTemporarily());
            }

            // Start enemy cooldown
            StartCoroutine(EnemyCooldown());
        }
    }

    private IEnumerator DisableObjectTemporarily()
    {
        isPlayerDisabled = true;
        objectToDisable.SetActive(false); // Disable the object

        // Start countdown and display the time remaining in the status text
        float timeRemaining = disableDuration;
        while (timeRemaining > 0f)
        {
            playerStatusText.text = "Player disabled: " + Mathf.Ceil(timeRemaining) + "s"; // Update countdown
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timeRemaining -= 1f; // Reduce the time remaining
        }

        objectToDisable.SetActive(true); // Enable the object again
        isPlayerDisabled = false;

        // After the cooldown ends, show the "Ready to interact" message again
        touchCooldownText.text = "Ready to interact"; // Changed text
        playerStatusText.text = "Player: Active"; // Changed text
    }

    private IEnumerator EnemyCooldown()
    {
        isEnemyCooldown = true;
        float remainingCooldown = cooldownDuration; // Store the remaining cooldown time

        // Cooldown for the interaction
        while (remainingCooldown > 0f)
        {
            touchCooldownText.text = "Cooldown: " + Mathf.Ceil(remainingCooldown) + "s"; // Display the cooldown time
            yield return new WaitForSeconds(1f); // Wait for 1 second
            remainingCooldown -= 1f; // Reduce the remaining cooldown time
        }

        // Once the cooldown is over, clear the message
        touchCooldownText.text = "Ready to interact"; // Changed text
        isEnemyCooldown = false;
    }
}
