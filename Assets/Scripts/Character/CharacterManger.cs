using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManger : MonoBehaviour, IDataPresistence
{

    public PartyMember baseStats;
    [HideInInspector] public CharacterData characterData;

    /*
     * Load data from a CharacterData object
     */
    public void LoadFromOtherCharacter(CharacterData other)
    {
        characterData.currentHealth          = other.currentHealth;
        characterData.currentExp             = other.currentExp;
        characterData.currentLevel           = other.currentLevel;
        characterData.currentPhysicalAttack  = other.currentPhysicalAttack;
        characterData.currentMagicalAttack   = other.currentMagicalAttack;
        characterData.currentEvade           = other.currentEvade;
        characterData.currentPhysicalDefense = other.currentPhysicalDefense;
        characterData.currentMagicalDefense  = other.currentMagicalDefense;
    }

    /*
     * Initalize a characters stats to baseline values
     */
    public void InitializeCharacter()
    {
        characterData.currentHealth          = baseStats.baseHealth;
        characterData.currentExp             = baseStats.baseExp;
        characterData.currentLevel           = baseStats.baseLevel;
        characterData.currentPhysicalAttack  = baseStats.basePhysicalAttack;
        characterData.currentMagicalAttack   = baseStats.baseMagicalAttack;
        characterData.currentEvade           = baseStats.baseEvade;
        characterData.currentPhysicalDefense = baseStats.basePhysicalDefense;
        characterData.currentMagicalDefense  = baseStats.baseMagicalDefense;
    }

    /*
     * Saves the characters data
     */
    public void SaveData(ref GameData gameData)
    {
        if (!gameData.characters.Contains(this.characterData))
        {
            gameData.characters.Add(this.characterData);
        }
        else gameData.characters[characterData.characterID] = this.characterData;
    }

    /*
     * Loads characters data
     */
    public void LoadData(GameData data)
    {
        characterData.characterID = CharacterData.characterCount;
        CharacterData.characterCount += 1;

        if (data.characters.Count > characterData.characterID) LoadFromOtherCharacter(data.characters[characterData.characterID]);
    }

    void Awake()
    {
        if (characterData == null) characterData = new();
        if (baseStats != null) InitializeCharacter();
    }

    void Update()
    {

        // Test code
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(characterData);
            characterData.currentHealth -= 1;
        }
    }
}
