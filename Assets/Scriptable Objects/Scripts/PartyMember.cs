using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Party Member", menuName = "Party/Member", order = 1)]
public class PartyMember : ScriptableObject
{
    // Base Memebr Stats
    [Header("Health")]
    [Range(0, 999)] [SerializeField] public int baseHealth;

    [Header("Leveling")]
    [Range(0, 99)] [SerializeField] public int baseExp;
    [Range(0, 99)] [SerializeField] public int baseLevel;

    [Header("Attack")]
    [Range(0, 99)] [SerializeField] public int basePhysicalAttack;
    [Range(0, 99)] [SerializeField] public int baseMagicalAttack;

    [Header("Defenses")]
    [Range(0, 99)] [SerializeField] public int baseEvade;
    [Range(0, 99)] [SerializeField] public int basePhysicalDefense;
    [Range(0, 99)] [SerializeField] public int baseMagicalDefense;
}
