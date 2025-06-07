using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Character System/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName; // Name of the skill
    public int damage; // Damage dealt by the skill
    public int cooldown; // Cooldown in rounds
    public bool isAoe; // Whether the skill is AoE (Area of Effect)
    public bool isTargeted; // Whether the skill targets a specific entity
    public bool isDebuff; // Whether the skill applies a debuff
    public bool isBuff; // Whether the skill applies a buff
    public bool isRange; // Whether the skill is a ranged attack
    public bool isMelee; // Whether the skill is a melee attack
    public float range; // Range of the skill
    public List<string> buffTypes; // List of buffs applied by the skill
    public List<string> debuffTypes; // List of debuffs applied by the skill
    public float debuffMultiplier; // Multiplier for debuff stacks (e.g., 2.0 for doubling)
}


