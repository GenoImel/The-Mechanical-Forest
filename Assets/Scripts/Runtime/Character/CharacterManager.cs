using Akashic.Runtime.DataManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akashic.Runtime.Character
{
    internal sealed class CharacterManager : MonoBehaviour, IDataPersistence
    {

        [Header("Character Base Stats")]
        [SerializeField] private PartyMember baseStats;
        
        private CharacterData memberData;
        
        void OnDestroy()
        {
            CharacterData.CharacterCount -= 1;
        }

        void Awake()
        {
            if (memberData == null)
            {
                memberData = new CharacterData();
            }

            if (baseStats != null)
            {
                InitializeCharacter();
            }
        }

        void Update()
        {
            // Test code
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log(memberData);
                memberData.CurrentHealth -= 1;
            }
        }

        /// <summary>
        /// Load data from a CharacterData object.
        /// </summary>
        public void LoadFromOtherCharacter(CharacterData other)
        {
            memberData.CurrentHealth          = other.CurrentHealth;
            memberData.CurrentExp             = other.CurrentExp;
            memberData.CurrentLevel           = other.CurrentLevel;
            memberData.CurrentPhysicalAttack  = other.CurrentPhysicalAttack;
            memberData.CurrentMagicalAttack   = other.CurrentMagicalAttack;
            memberData.CurrentEvade           = other.CurrentEvade;
            memberData.CurrentPhysicalDefense = other.CurrentPhysicalDefense;
            memberData.CurrentMagicalDefense  = other.CurrentMagicalDefense;
        }

        /// <summary>
        /// Initialize a characters stats to baseline values.
        /// </summary>
        public void InitializeCharacter()
        {
            memberData.CurrentHealth          = baseStats.BaseHealth;
            memberData.CurrentExp             = baseStats.BaseExp;
            memberData.CurrentLevel           = baseStats.BaseLevel;
            memberData.CurrentPhysicalAttack  = baseStats.BasePhysicalAttack;
            memberData.CurrentMagicalAttack   = baseStats.BaseMagicalAttack;
            memberData.CurrentEvade           = baseStats.BaseEvade;
            memberData.CurrentPhysicalDefense = baseStats.BasePhysicalDefense;
            memberData.CurrentMagicalDefense  = baseStats.BaseMagicalDefense;
        }

        /// <summary>
        /// Saves the characters data.
        /// </summary>
        public void SaveData(ref GameData gameData)
        {
            if (!gameData.characters.Contains(this.memberData))
            {
                gameData.characters.Add(this.memberData);
            }
            else gameData.characters[memberData.CharacterID] = this.memberData;
        }

        /// <summary>
        /// Loads characters data.
        /// </summary>
        public void LoadData(GameData data)
        {
            memberData.CharacterID = CharacterData.CharacterCount;
            CharacterData.CharacterCount += 1;

            if (data.characters.Count > memberData.CharacterID) LoadFromOtherCharacter(data.characters[memberData.CharacterID]);
        }
    }
}
