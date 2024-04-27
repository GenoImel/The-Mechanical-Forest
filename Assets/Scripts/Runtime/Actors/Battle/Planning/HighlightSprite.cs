using System;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Planning
{
    internal sealed class HighlightSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer highlightSpriteRenderer;
        [SerializeField] private MeshRenderer textMeshRenderer;
        
        public Action OnSelectAction;

        public void SetHighlighted(bool isHighlighted)
        {
            highlightSpriteRenderer.enabled = isHighlighted;
            textMeshRenderer.enabled = isHighlighted;
        }
        
        public void InvokeAction()
        {
            OnSelectAction?.Invoke();
        }
    }
}