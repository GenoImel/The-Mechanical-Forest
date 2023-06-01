using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour, IDataPresistence
{

    public PartyMember BaseStats;
    [HideInInspector] public CharacterData MemberData;

    /*
     * Load data from a CharacterData object
     */
    public void LoadFromOtherCharacter(CharacterData other)
    {
        MemberData.CurrentHealth          = other.CurrentHealth;
        MemberData.CurrentExp             = other.CurrentExp;
        MemberData.CurrentLevel           = other.CurrentLevel;
        MemberData.CurrentPhysicalAttack  = other.CurrentPhysicalAttack;
        MemberData.CurrentMagicalAttack   = other.CurrentMagicalAttack;
        MemberData.CurrentEvade           = other.CurrentEvade;
        MemberData.CurrentPhysicalDefense = other.CurrentPhysicalDefense;
        MemberData.CurrentMagicalDefense  = other.CurrentMagicalDefense;
    }

    /*
     * Initalize a characters stats to baseline values
     */
    public void InitializeCharacter()
    {
        MemberData.CurrentHealth          = BaseStats.BaseHealth;
        MemberData.CurrentExp             = BaseStats.BaseExp;
        MemberData.CurrentLevel           = BaseStats.BaseLevel;
        MemberData.CurrentPhysicalAttack  = BaseStats.BasePhysicalAttack;
        MemberData.CurrentMagicalAttack   = BaseStats.BaseMagicalAttack;
        MemberData.CurrentEvade           = BaseStats.BaseEvade;
        MemberData.CurrentPhysicalDefense = BaseStats.BasePhysicalDefense;
        MemberData.CurrentMagicalDefense  = BaseStats.BaseMagicalDefense;
    }

    /*
     * Saves the characters data
     */
    public void SaveData(ref GameData gameData)
    {
        if (!gameData.Characters.Contains(this.MemberData))
        {
            gameData.Characters.Add(this.MemberData);
        }
        else gameData.Characters[MemberData.CharacterID] = this.MemberData;
    }

    /*
     * Loads characters data
     */
    public void LoadData(GameData data)
    {
        MemberData.CharacterID = CharacterData.CharacterCount;
        CharacterData.CharacterCount += 1;

        if (data.Characters.Count > MemberData.CharacterID) LoadFromOtherCharacter(data.Characters[MemberData.CharacterID]);
    }

    void OnDestroy()
    {
        CharacterData.CharacterCount -= 1;
    }

    void Awake()
    {
        if (MemberData == null) MemberData = new();
        if (BaseStats != null) InitializeCharacter();
    }

    void Update()
    {

        // Test code
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(MemberData);
            MemberData.CurrentHealth -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Loading New Scene");
            if (SceneManager.GetActiveScene().name == "SavingTestScene") SceneManager.LoadSceneAsync("SavingTestScene2");
            else SceneManager.LoadSceneAsync("SavingTestScene");
            MemberData.CurrentHealth -= 1;
        }
    }
}
