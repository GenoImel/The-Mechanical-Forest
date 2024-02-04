using System;
using Akashic.Core;
using Akashic.ScriptableObjects.Exploration;
using UnityEngine;

namespace Akashic.Runtime.Builders.Exploration
{
    internal sealed class Door : MonoBehaviour
    {
        [SerializeField] private ExplorationEnvironmentData targetEnvironment;

        private void OnCollisionEnter(Collision collision)
        {
            GameManager.Publish(new LoadNewExplorationSceneMessage(targetEnvironment));
        }
    }
}