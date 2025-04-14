using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    private int currentPlayerIndex = 0;
    private int currentRound = 1;
    private bool isPlayerPhase = true;

    void Start()
    {
        InitializeCombat();
        SortPlayersBySpeed(); // Sort players by speed at the start of the game
        StartPlayerPhase();
    }

    void Update()
    {
        if (isPlayerPhase)
        {
            HandlePlayerTurn();
        }
    }

    void InitializeCombat()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject);
            StoreOriginalColor(playerObject);
        }

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            enemies.Add(enemyObject);
            StoreOriginalColor(enemyObject);
        }

        Debug.Log($"Round {currentRound} Start!");
    }

    void StoreOriginalColor(GameObject character)
    {
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColors[character] = renderer.material.color;
        }
    }

    void SortPlayersBySpeed()
    {
        players.Sort((player1, player2) =>
        {
            Speed speed1 = player1.GetComponent<Speed>();
            Speed speed2 = player2.GetComponent<Speed>();

            if (speed1 != null && speed2 != null)
            {
                // Sort in descending order (highest speed first)
                return speed2.SpeedValue.CompareTo(speed1.SpeedValue);
            }

            return 0; // If Speed component is missing, leave the order unchanged
        });

        Debug.Log("Players sorted by speed:");
        foreach (GameObject player in players)
        {
            Speed speed = player.GetComponent<Speed>();
            if (speed != null)
            {
                Debug.Log($"{player.name} - Speed: {speed.SpeedValue}");
            }
        }
    }

    void StartPlayerPhase()
    {
        isPlayerPhase = true;
        currentPlayerIndex = 0;
        Debug.Log("Player Phase Start!");
        HighlightCurrentPlayer();
    }

    void HandlePlayerTurn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndPlayerTurn();
        }
    }

    void EndPlayerTurn()
    {
        ResetColor(players[currentPlayerIndex]);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            StartCoroutine(StartEnemyPhase());
        }
        else
        {
            HighlightCurrentPlayer();
        }
    }

    IEnumerator StartEnemyPhase()
    {
        isPlayerPhase = false;
        Debug.Log("Enemy Phase Start!");

        foreach (GameObject enemy in enemies)
        {
            PulsateColor(enemy, Color.red, Color.black, 1f);
            yield return new WaitForSeconds(1f); // Simulate enemy action delay
            Debug.Log("ENEMY ATTACKED");
            ResetColor(enemy);
        }

        Debug.Log("Enemy Phase End!");
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before starting the next round
        StartNewRound();
    }

    void StartNewRound()
    {
        currentRound++;
        Debug.Log($"Round {currentRound} Start!");
        SortPlayersBySpeed(); // Re-sort players by speed at the start of each round
        StartPlayerPhase();
    }

    void HighlightCurrentPlayer()
    {
        if (currentPlayerIndex < players.Count)
        {
            // Pulsate between blue and light blue
            PulsateColor(players[currentPlayerIndex], Color.blue, new Color(0.68f, 0.85f, 1f), 1f);
        }
    }

    void PulsateColor(GameObject character, Color startColor, Color endColor, float duration)
    {
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            float t = Mathf.PingPong(Time.time / duration, 1f);
            renderer.material.color = Color.Lerp(startColor, endColor, t);
        }
    }

    void ResetColor(GameObject character)
    {
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null && originalColors.ContainsKey(character))
        {
            renderer.material.color = originalColors[character];
        }
    }
}
