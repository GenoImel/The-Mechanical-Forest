using Akashic.Core;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.StateMachines.GameStates;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.Save;
using Akashic.Runtime.MonoSystems.Inventory;
using Akashic.Runtime.Serializers.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class SaveSlot : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button newGameButton;

        [SerializeField] private Button saveGameButton;
        
        [SerializeField] private Button loadGameButton;

        [Header("Text")]
        [SerializeField] private TMP_Text saveSlotNameText;

        private string saveFileName;

        private ISaveMonoSystem saveMonoSystem;
		private IPartyMonoSystem partyMonoSystem;
		private IInventoryMonoSystem inventoryMonoSystem;
        private IConfigMonoSystem configMonoSystem;

		private bool isAwaitingNewSlotName = false;
        
        private void Awake()
        {
            saveMonoSystem = GameManager.GetMonoSystem<ISaveMonoSystem>();
			partyMonoSystem = GameManager.GetMonoSystem<IPartyMonoSystem>();
			inventoryMonoSystem = GameManager.GetMonoSystem<IInventoryMonoSystem>();
            configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
		}

        private void Start()
        {
            NewGameButtonEnabled(true);
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        public void SetSaveSlotName(string saveSlotName)
        {
            saveSlotNameText.text = saveSlotName;
        }
        
        public void SetSaveFileName(string fileName)
        {
            saveFileName = fileName;
        }

        private void InitializeNewSaveFile(string fileName)
        {
            SetSaveSlotName(fileName);
            
            partyMonoSystem.CreateNewParty();

            inventoryMonoSystem.CreateNewInventory();

            var saveLocation = new PartyLocation(
                configMonoSystem.GetRoomId(),
                configMonoSystem.GetSpawnPointId()
                );

			var saveFile = new SaveFile(
				saveSlotNameText.text,
				partyMonoSystem.GetPartyMembers(),
				inventoryMonoSystem.GetInventory(),
                saveLocation);
            saveMonoSystem.InitializeNewFile(saveFile,this.saveFileName);
            
            GameManager.Publish(new HideSaveMenuMessage());
        }

        private void OnNewGameButtonPressed()
        {
            isAwaitingNewSlotName = true;
            GameManager.Publish(new RequestNewFileNameMessage());
        }

        private void OnSaveGameButtonPressed()
        {
            
        }

        private void OnLoadGameButtonPressed()
        {
            
        }

        private void OnGameStateChangedMessage(GameStateChangedMessage message)
        {
            NewGameButtonEnabled(message.NextState is GameFiniteState.MainMenu);
        }

        private void NewGameButtonEnabled(bool isEnabled)
        {
            newGameButton.gameObject.SetActive(isEnabled);
            saveGameButton.gameObject.SetActive(!isEnabled);
        }
        
        private void OnNewFileNameConfirmedMessage(NewFileNameConfirmedMessage message)
        {
            if (isAwaitingNewSlotName)
            {
                InitializeNewSaveFile(message.NewFileName);
            }
            
            isAwaitingNewSlotName = false;
        }

        private void AddListeners()
        {
            newGameButton.onClick.AddListener(OnNewGameButtonPressed);
            saveGameButton.onClick.AddListener(OnSaveGameButtonPressed);
            loadGameButton.onClick.AddListener(OnLoadGameButtonPressed);
            
            GameManager.AddListener<GameStateChangedMessage>(OnGameStateChangedMessage);
            GameManager.AddListener<NewFileNameConfirmedMessage>(OnNewFileNameConfirmedMessage);
        }

        private void RemoveListeners()
        {
            newGameButton.onClick.RemoveListener(OnNewGameButtonPressed);
            saveGameButton.onClick.RemoveListener(OnSaveGameButtonPressed);
            loadGameButton.onClick.RemoveListener(OnLoadGameButtonPressed);
            
            GameManager.RemoveListener<GameStateChangedMessage>(OnGameStateChangedMessage);
            GameManager.RemoveListener<NewFileNameConfirmedMessage>(OnNewFileNameConfirmedMessage);
        }
    }
}