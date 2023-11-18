using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] float powerUpDuration = 10f;
    [SerializeField] float scalingFactor = 0.5f;
    [SerializeField] float respawnDelay = 5f;

    private bool isActivated = false;
    private SpriteRenderer powerUpSpriteRenderer;
    private BoxCollider2D powerUpCollider;

    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        powerUpSpriteRenderer = GetComponent<SpriteRenderer>();
        powerUpCollider = GetComponent<BoxCollider2D>();

        // Delay the initial spawn by 10 seconds
        StartCoroutine(InitialSpawnDelay());
    }

    IEnumerator InitialSpawnDelay()
    {
        yield return new WaitForSeconds(10f);
        RespawnPowerUp();
        StartCoroutine(SpawnDelay()); // Start the regular respawn delay
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        RespawnPowerUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            ActivatePowerUp();
        }
    }

    void ActivatePowerUp()
    {
        // Scale down all objects with the specified tags
        ScaleDownGameObjectsWithTag("RedBall");
        ScaleDownGameObjectsWithTag("RedBall2");

        // Deactivate the SpriteRenderer and BoxCollider2D
        powerUpSpriteRenderer.enabled = false;
        powerUpCollider.enabled = false;

        // Reset the respawn delay
        StopAllCoroutines(); // Stop any existing respawn delay coroutine
        StartCoroutine(ReactivatePowerUp());
    }

    IEnumerator ReactivatePowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration);

        // Return all objects to their normal size after the power-up duration
        ScaleUpGameObjectsWithTag("RedBall");
        ScaleUpGameObjectsWithTag("RedBall2");

        // Reactivate the SpriteRenderer and BoxCollider2D
        powerUpSpriteRenderer.enabled = true;
        powerUpCollider.enabled = true;

        // Reset the activation flag
        isActivated = false;

        // Respawn the power-up after a delay
        StartCoroutine(SpawnDelay());
    }

    void RespawnPowerUp()
    {
        // Set a random position for the power-up
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        transform.position = randomPosition;

        // Activate the SpriteRenderer and BoxCollider2D
        powerUpSpriteRenderer.enabled = true;
        powerUpCollider.enabled = true;
    }

    void ScaleDownGameObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            // Store the original scale before scaling down
            originalScales[obj] = obj.transform.localScale;

            // Scale down the object
            obj.transform.localScale *= scalingFactor;
        }
    }

    void ScaleUpGameObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            // Check if the object was previously scaled down
            if (originalScales.TryGetValue(obj, out Vector3 originalScale))
            {
                // Scale it back to its original size
                obj.transform.localScale = originalScale;

                // Remove the entry from the dictionary
                originalScales.Remove(obj);
            }
        }
    }
}
