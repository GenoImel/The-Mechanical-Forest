using Akashic.Core;
using Akashic.Runtime.MonoSystems.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button newGameButton;
        
        private ISceneMonoSystem sceneMonoSystem;

        private void Awake()
        {
            sceneMonoSystem = GameManager.GetMonoSystem<ISceneMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void NewGameButtonClicked()
        {
            sceneMonoSystem.LoadExplorationScene();
        }
        
        private void AddListeners()
        {
            newGameButton.onClick.AddListener(NewGameButtonClicked);
        }
        
        private void RemoveListeners()
        {
            newGameButton.onClick.RemoveListener(NewGameButtonClicked);
        }
    }
}