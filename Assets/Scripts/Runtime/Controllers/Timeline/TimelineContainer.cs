using System.Collections.Generic;
using Akashic.Runtime.Utilities.Random;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Timeline
{
    internal sealed class TimelineContainer : MonoBehaviour
    {
        [SerializeField] private List<TimelineSlot> timelineSlots;

        public void ReserveSlotsForParty()
        {
            var totalSlots = timelineSlots.Count;
            var thirdSize = totalSlots / 3;
            var availableIndices = new HashSet<int>();
            
            for (int i = 0; i < totalSlots; i++)
            {
                availableIndices.Add(i);
            }
            
            var firstThirdSlotIndex = Random.Range(0, thirdSize);
            timelineSlots[firstThirdSlotIndex].ReserveForParty();
            availableIndices.Remove(firstThirdSlotIndex);
            
            var lastThirdSlotIndex = Random.Range(2 * thirdSize, totalSlots);
            timelineSlots[lastThirdSlotIndex].ReserveForParty();
            availableIndices.Remove(lastThirdSlotIndex);
            
            var remainingIndices = new List<int>(availableIndices);
            remainingIndices = Shufflers.FisherYates(remainingIndices);
            
            for (var i = 0; i < 2; i++)
            {
                timelineSlots[remainingIndices[i]].ReserveForParty();
            }
        
        }
    }
}