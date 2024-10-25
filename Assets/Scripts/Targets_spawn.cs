using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public GameObject target;
    private string level;
    public float spawnInterval = 3f; // Seconds between spawns
    public int maxTargets = 10; // Max targets allowed in scene
    public Transform player; // Reference to the player's transform
    public float spawnDistance = 10f; // Distance in front of the player where the target will spawn
    public float spawnRadius = 5f; // Radius around the player's forward direction for random spawning

    private float spawnTimer = 0f;
    private bool isTrainingMode = false; // Flag to indicate training mode

    void Start()
    {
        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player reference is missing!");
        }

        level = PlayerPrefs.GetString("level", "easy");

        // Check if we are in training mode
        isTrainingMode = level.Equals("training", System.StringComparison.OrdinalIgnoreCase);

        // If in training mode, spawn one target at a specific position
        if (isTrainingMode)
        {
            SpawnSingleTarget(new Vector3(-258.4f, 1.27f, 53.28f)); // Specify the position where you want the target
        }
    }

    void Update()
    {
        if (isTrainingMode) return; // Skip spawning if in training mode

        spawnTimer += Time.deltaTime;

        // Check if we should spawn a new target
        if (spawnTimer >= spawnInterval && GameObject.FindGameObjectsWithTag("Target").Length < maxTargets)
        {
            SpawnTarget();
            spawnTimer = 0f; // Reset the timer
        }
    }

    void SpawnTarget()
    {
        if (player == null) return; // Prevent null reference errors

        // Calculate the spawn position in front of the player
        Vector3 spawnPosition = player.position + player.forward * spawnDistance;

        // Add a random offset within a radius around the forward direction
        Vector3 randomOffset = new Vector3(
            UnityEngine.Random.Range(-spawnRadius * 2, spawnRadius * 2), // Increased left-right movement range
            UnityEngine.Random.Range(1f, 5f), // Y position is between 1 and 5 to avoid being underground
            UnityEngine.Random.Range(-spawnRadius, spawnRadius)  // Random offset forward-backward for variety
        );

        // Apply the random offset to the spawn position
        spawnPosition += randomOffset;

        GameObject newTarget = Instantiate(target, spawnPosition, Quaternion.identity);

        // Set the tag for the new target
        newTarget.tag = "Target";

        // Assign the difficulty level from PlayerPrefs to the new target
        newTarget.GetComponent<Targets_movements>().level = level;
    }

    void SpawnSingleTarget(Vector3 position)
    {
        // Spawn a single target at the specified position
        GameObject newTarget = Instantiate(target, position, Quaternion.identity);
        newTarget.tag = "Target"; // Set the tag for the target
        newTarget.GetComponent<Targets_movements>().level = level; // Assign difficulty level
    }
}
