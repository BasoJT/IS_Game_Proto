using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CombatSys combatSys; // Reference to CombatSys
    public List<CharacterData> allCharacters; // List of all available characters
    public List<CharacterData> charactersInPlay; // List of characters to assign to CombatSys
    public Transform playersContainer; // Parent GameObject containing p1, p2, p3, p4

    void Start()
    {
        // Initially, no characters are assigned to play
        charactersInPlay.Clear();
    }

    /// <summary>
    /// Call this from a UI Button to spawn a character by name and fill the next available slot.
    /// </summary>
    /// <param name="characterName">The name of the character to spawn (must match ScriptableObject's characterName)</param>
    public void SpawnCharacterByName(string characterName)
    {
        SelectCharacter(characterName);
        AssignCharactersToCombat();
    }

    public void SelectCharacter(string characterName)
    {
        // Find the character in the list of all available characters
        CharacterData selectedCharacter = allCharacters.Find(character => character.characterName == characterName);

        if (selectedCharacter != null && !charactersInPlay.Contains(selectedCharacter))
        {
            // Add the selected character to the list of characters in play
            charactersInPlay.Add(selectedCharacter);
            Debug.Log($"Character {characterName} added to the game.");
        }
        else
        {
            Debug.Log($"Character {characterName} is already in play or does not exist.");
        }
    }

    public void AssignCharactersToCombat()
    {
        List<GameObject> instantiatedCharacters = new List<GameObject>();

        // Get all child positions from playersContainer
        Transform[] playerPositions = playersContainer.GetComponentsInChildren<Transform>();

        // Ensure there are enough positions for the characters
        if (charactersInPlay.Count > playerPositions.Length - 1) // Exclude the parent itself
        {
            Debug.LogError("Not enough positions in playersContainer for all characters!");
            return;
        }

        for (int i = 0; i < charactersInPlay.Count; i++)
        {
            CharacterData characterData = charactersInPlay[i];

            // Instantiate the character prefab from CharacterData
            GameObject character = Instantiate(characterData.characterPrefab);
            character.name = characterData.characterName;

            // Attach CharacterData to the prefab
            CharacterProfile profile = character.GetComponent<CharacterProfile>();
            if (profile != null)
            {
                profile.characterData = characterData;
            }

            // Assign the character to the corresponding child position
            Transform position = playerPositions[i + 1]; // Skip the parent itself (index 0)
            character.transform.SetParent(position);
            character.transform.localPosition = Vector3.zero; // Reset position relative to parent

            // Tag the character appropriately for CombatSys
            character.tag = "Player"; // Assuming these are players; use "Enemy" for enemies

            instantiatedCharacters.Add(character);
        }

        // Pass the instantiated characters to CombatSys
        combatSys.SetCharacters(instantiatedCharacters);
        Debug.Log("Characters assigned to CombatSys.");
    }
}
                                                                                                                                            