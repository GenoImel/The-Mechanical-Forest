using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Party Member", menuName = "Party/Member", order = 1)]
public class PartyMember : ScriptableObject
{
    // Base Memebr Stats
    [Header("Health")]
    [Range(0, 999)] public int BaseHealth;

    [Header("Leveling")]
    [Range(0, 99)] public int BaseExp;
    [Range(0, 99)] public int BaseLevel;

    [Header("Attack")]
    [Range(0, 99)] public int BasePhysicalAttack;
    [Range(0, 99)] public int BaseMagicalAttack;

    [Header("Defenses")]
    [Range(0, 99)] public int BaseEvade;
    [Range(0, 99)] public int BasePhysicalDefense;
    [Range(0, 99)] public int BaseMagicalDefense;
}
