using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.Timeline
{
    internal sealed class TimelineSlot : MonoBehaviour
    {
        [SerializeField] private Image timelineImage;
        
        [Header("Test Settings")]
        [SerializeField] private Color unoccupiedColor;
        [SerializeField] private Color occupiedColor;
        [SerializeField] private Color reservedColor;
        
        public void ReserveForParty()
        {
            timelineImage.color = reservedColor;
        }
    }
}