using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character System/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName; // Name of the character
    public string element; // Element type (e.g., "Dark")
    public int health; // Health points
    public int speed; // Speed attribute
    public List<SkillData> skills; // List of skill Scriptable Objects
    public GameObject characterPrefab; // Reference to the character's prefab
}
