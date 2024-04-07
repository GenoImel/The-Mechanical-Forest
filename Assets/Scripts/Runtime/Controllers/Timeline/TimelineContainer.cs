using System.Collections.Generic;
using Akashic.Runtime.MonoSystems.Timeline;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Timeline
{
    internal sealed class TimelineContainer : MonoBehaviour
    {
        [SerializeField] private List<TimelineSlot> timelineSlots;

        public List<TimelineSlot> TimelineSlots => timelineSlots;

        public void ReserveSlotsForParty(List<TimelineMove> timelineMoves)
        {
            var i = 0;
            foreach (var move in timelineMoves)
            {
                if (move.isReservedForParty)
                {
                    timelineSlots[i].ReserveForParty();
                }

                i++;
            }
        }

        public void OccupySlotsWithEnemy(List<TimelineMove> timelineMoves)
        {
            var i = 0;
            foreach (var move in timelineMoves)
            {
                if (move.isOccupied)
                {
                    timelineSlots[i].SetOccupiedByEnemy();
                }

                i++;
            }
        }
    }
}