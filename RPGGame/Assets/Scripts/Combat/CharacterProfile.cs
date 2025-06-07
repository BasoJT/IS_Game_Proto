using System.Collections.Generic;
using UnityEngine;

public class CharacterProfile : MonoBehaviour
{
    public CharacterData characterData; // Reference to Lazhar's CharacterData

    private string characterName;
    private string element;
    private int health;
    private int speed;
    private List<SkillData> skills;

    void Start()
    {
        if (characterData != null)
        {
            // Transfer data from CharacterData to this GameObject
            characterName = characterData.characterName;
            element = characterData.element;
            health = characterData.health;
            speed = characterData.speed;
            skills = characterData.skills;

            Debug.Log($"Character Loaded: {characterName}");
            Debug.Log($"Element: {element}, Health: {health}, Speed: {speed}");

            foreach (var skill in skills)
            {
                Debug.Log($"Skill: {skill.skillName}, Damage: {skill.damage}, Cooldown: {skill.cooldown}, AoE: {skill.isAoe}");
            }
        }
        else
        {
            Debug.LogError("CharacterData is not assigned!");
        }
    }
}

