using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSys : MonoBehaviour
{
    [SerializeField] private GameObject[] sortOrder; // Array to store players and enemies for sorting
    private int currentTurnIndex = 0; // Tracks whose turn it is
    private bool isTurnInProgress = false; // Prevents overlapping turns
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>(); // Stores original colors
    private Coroutine pulsingCoroutine; // Tracks the current pulsing coroutine
    private int currentRound = 1; // Tracks the current round number

    void Start()
    {
        RandomizeRollSpeed(); // Randomize rollSpeed at the start
        PopulateAndSort();
        CacheOriginalColors();
        StartRound();
    }

    void Update()
    {
        if (!isTurnInProgress)
        {
            HandleTurn();
        }
    }

    private void RandomizeRollSpeed()
    {
        // Find all GameObjects tagged as "Player" and "Enemy"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Randomize rollSpeed for players
        foreach (GameObject player in players)
        {
            Speed speedComponent = player.GetComponent<Speed>();
            if (speedComponent != null)
            {
                speedComponent.RollSpeed = Random.Range(1, 9); // Random value between 1 and 8
                Debug.Log($"{player.name} RollSpeed set to {speedComponent.RollSpeed}");
            }
        }

        // Randomize rollSpeed for enemies
        foreach (GameObject enemy in enemies)
        {
            ESpeed eSpeedComponent = enemy.GetComponent<ESpeed>();
            if (eSpeedComponent != null)
            {
                eSpeedComponent.RollSpeed = Random.Range(1, 9); // Random value between 1 and 8
                Debug.Log($"{enemy.name} RollSpeed set to {eSpeedComponent.RollSpeed}");
            }
        }
    }

    private void PopulateAndSort()
    {
        // Find all GameObjects tagged as "Player" and "Enemy"
        List<GameObject> allCharacters = new List<GameObject>();
        allCharacters.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        allCharacters.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        // Sort the characters by roundSpeed (descending order)
        allCharacters.Sort((char1, char2) =>
        {
            float speed1 = GetRoundSpeedValue(char1);
            float speed2 = GetRoundSpeedValue(char2);

            return speed2.CompareTo(speed1); // Descending order
        });

        // Assign the sorted list to the sortOrder array
        sortOrder = allCharacters.ToArray();

        // Debug log the sorted order
        Debug.Log("Sorted Order (Fastest to Slowest):");
        foreach (GameObject character in sortOrder)
        {
            Debug.Log($"{character.name} - RoundSpeed: {GetRoundSpeedValue(character)}");
        }
    }

    private float GetRoundSpeedValue(GameObject character)
    {
        // Check for Speed component (Player)
        Speed playerSpeed = character.GetComponent<Speed>();
        if (playerSpeed != null)
        {
            return playerSpeed.RoundSpeed;
        }

        // Check for ESpeed component (Enemy)
        ESpeed enemySpeed = character.GetComponent<ESpeed>();
        if (enemySpeed != null)
        {
            return enemySpeed.RoundSpeed;
        }

        return 0f; // Default roundSpeed if no component is found
    }

    private void CacheOriginalColors()
    {
        foreach (GameObject character in sortOrder)
        {
            Renderer renderer = character.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalColors[character] = renderer.material.color; // Cache the original color
            }
        }
    }

    private void StartRound()
    {
        Debug.Log($"Round {currentRound} Start!");
        currentTurnIndex = 0; // Start with the first character in the sorted order
        HandleTurn();
    }

    private void HandleTurn()
    {
        if (currentTurnIndex >= sortOrder.Length)
        {
            EndRound(); // End the round if all characters have acted
            return;
        }

        GameObject currentCharacter = sortOrder[currentTurnIndex];
        Debug.Log($"Current Turn: {currentCharacter.name} (Index: {currentTurnIndex})");

        if (currentCharacter.CompareTag("Player"))
        {
            StartCoroutine(HandlePlayerTurn(currentCharacter));
        }
        else if (currentCharacter.CompareTag("Enemy"))
        {
            StartCoroutine(HandleEnemyTurn(currentCharacter));
        }
    }

    private IEnumerator HandlePlayerTurn(GameObject player)
    {
        isTurnInProgress = true;
        Debug.Log($"{player.name}'s turn! Press Space to end turn.");

        // Start pulsing effect
        pulsingCoroutine = StartCoroutine(PulsateColor(player, Color.blue, new Color(0.1f, 0.1f, 0.8f), 1f));

        // Wait for the player to press space
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null; // Wait until the next frame
        }

        Debug.Log($"{player.name} has ended their turn.");
        StopPulsingEffect(); // Stop pulsing
        RestoreOriginalColor(player); // Restore original color
        yield return new WaitForSeconds(1f); // Pause before moving to the next turn
        EndTurn();
    }

    private IEnumerator HandleEnemyTurn(GameObject enemy)
    {
        isTurnInProgress = true;
        Debug.Log($"{enemy.name} attacks!");

        // Start pulsing effect
        pulsingCoroutine = StartCoroutine(PulsateColor(enemy, Color.red, new Color(0.5f, 0f, 0f), 1f));

        yield return new WaitForSeconds(1f); // Simulate enemy action delay

        StopPulsingEffect(); // Stop pulsing
        RestoreOriginalColor(enemy); // Restore original color
        EndTurn();
    }

    private IEnumerator PulsateColor(GameObject character, Color startColor, Color endColor, float duration)
    {
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            float time = 0f;
            while (true) // Loop indefinitely until stopped
            {
                time += Time.deltaTime;
                float t = Mathf.PingPong(time / duration, 1f);
                renderer.material.color = Color.Lerp(startColor, endColor, t);
                yield return null; // Wait until the next frame
            }
        }
    }

    private void StopPulsingEffect()
    {
        if (pulsingCoroutine != null)
        {
            StopCoroutine(pulsingCoroutine); // Stop the pulsing coroutine
            pulsingCoroutine = null;
        }
    }

    private void RestoreOriginalColor(GameObject character)
    {
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null && originalColors.ContainsKey(character))
        {
            renderer.material.color = originalColors[character]; // Restore the original color
        }
    }

    private void EndTurn()
    {
        currentTurnIndex++; // Move to the next character in the sorted order
        Debug.Log($"Turn Ended. Moving to next character (Index: {currentTurnIndex}).");
        isTurnInProgress = false; // Allow the next turn to start
    }

    private void EndRound()
    {
        Debug.Log($"Round {currentRound} Ended!");
        currentRound++; // Increment the round number
        RandomizeRollSpeed(); // Re-randomize rollSpeed for the next round
        PopulateAndSort(); // Re-read speeds and re-sort characters
        StartRound(); // Start the next round
    }
}

