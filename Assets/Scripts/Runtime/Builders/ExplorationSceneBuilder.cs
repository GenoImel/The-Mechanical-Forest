using System;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Scene;
using UnityEngine;

namespace Akashic.Runtime.Builders
{
    internal sealed class ExplorationSceneBuilder : MonoBehaviour
    {
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnSceneInitializationStartedMessage(StartSceneInitializationMessage message)
        {
            // Do some stuff for scene initialization here.
            GameManager.Publish(new SceneInitializedMessage());
        }

        private void AddListeners()
        {
            GameManager.AddListener<StartSceneInitializationMessage>(OnSceneInitializationStartedMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<StartSceneInitializationMessage>(OnSceneInitializationStartedMessage);
        }
    }
}