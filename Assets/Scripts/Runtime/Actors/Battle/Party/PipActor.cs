using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PipActor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer pipSprite;

        [Header("Colors")]
        [SerializeField] private Color availableColor;

        [SerializeField] private Color planningColor;

        [SerializeField] private Color spentColor;

        private bool isAvailable = true;

        public bool IsAvailable => isAvailable;

        public void SetPipAvailable()
        {
            isAvailable = true;
            pipSprite.color = availableColor;
        }

        public void PlanToSpendPip()
        {
            pipSprite.color = planningColor;
        }

        public void SetPipSpent()
        {
            isAvailable = false;
            pipSprite.color = spentColor;
        }

        public void HidePip()
        {
            pipSprite.enabled = false;
        }

        public void ShowPip()
        {
            pipSprite.enabled = true;
        }
    }
}