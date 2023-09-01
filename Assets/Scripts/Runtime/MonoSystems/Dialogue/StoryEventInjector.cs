using UnityEngine;
using Akashic.ScriptableObjects.Scripts;

namespace Akashic.Runtime.MonoSystems.Dialogue
{
    internal sealed class StoryEventInjector : MonoBehaviour
    {
        public StoryEventBaseData storyEvent;

        private void OnMouseDown()
        {
            Debug.Log($"{storyEvent.storyPoints[0].dialogueLine}");
        }
    }
}
