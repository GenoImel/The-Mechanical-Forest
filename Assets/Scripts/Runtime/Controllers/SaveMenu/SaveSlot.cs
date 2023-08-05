using Akashic.Core;
using Akashic.Runtime.MonoSystems.GameStates;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.Scene;
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
        
        private ISceneMonoSystem sceneMonoSystem;
        private IPartyMonoSystem partyMonoSystem;
        
        private void Awake()
        {
            sceneMonoSystem = GameManager.GetMonoSystem<ISceneMonoSystem>();
            partyMonoSystem = GameManager.GetMonoSystem<IPartyMonoSystem>();
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

        private void OnNewGameButtonPressed()
        {
            partyMonoSystem.CreateNewParty();
            sceneMonoSystem.LoadExplorationScene();
            GameManager.Publish(new HideSaveMenuMessage());
        }

        private void OnSaveGameButtonPressed()
        {
            
        }

        private void OnLoadGameButtonPressed()
        {
            
        }

        private void OnGameStateChangedMessage(GameStateChangedMessage message)
        {
            if (message.NextState == GameState.MainMenu)
            {
                NewGameButtonEnabled(true);
            }
            else
            {
                NewGameButtonEnabled(false);
            }
        }

        private void NewGameButtonEnabled(bool isEnabled)
        {
            newGameButton.gameObject.SetActive(isEnabled);
            saveGameButton.gameObject.SetActive(!isEnabled);
        }

        private void AddListeners()
        {
            newGameButton.onClick.AddListener(OnNewGameButtonPressed);
            saveGameButton.onClick.AddListener(OnSaveGameButtonPressed);
            loadGameButton.onClick.AddListener(OnLoadGameButtonPressed);
            
            GameManager.AddListener<GameStateChangedMessage>(OnGameStateChangedMessage);
        }

        private void RemoveListeners()
        {
            newGameButton.onClick.RemoveListener(OnNewGameButtonPressed);
            saveGameButton.onClick.RemoveListener(OnSaveGameButtonPressed);
            loadGameButton.onClick.RemoveListener(OnLoadGameButtonPressed);
            
            GameManager.RemoveListener<GameStateChangedMessage>(OnGameStateChangedMessage);
        }
    }
}