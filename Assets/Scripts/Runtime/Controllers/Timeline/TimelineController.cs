using Akashic.Core;
using Akashic.Runtime.MonoSystems.Timeline;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Timeline
{
    internal sealed class TimelineController : MonoBehaviour
    {
        [SerializeField] private TimelineContainer timelineContainer;

        private ITimelineMonoSystem timelineMonoSystem;

        private void Awake()
        {
            timelineMonoSystem = GameManager.GetMonoSystem<ITimelineMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnReserveTimelineSlotsForPartyMessage(SlotsReservedForPartyMessage message)
        {
            var timelineMoves = timelineMonoSystem.TimelineMoves;
            
            timelineContainer.ReserveSlotsForParty(timelineMoves);
        }

        private void OnEnemyMovesChosenMessage(EnemyMovesChosenMessage message)
        {
            var timeLineMoves = timelineMonoSystem.TimelineMoves;

            timelineContainer.OccupySlotsWithEnemy(timeLineMoves);
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<SlotsReservedForPartyMessage>(OnReserveTimelineSlotsForPartyMessage);
            GameManager.AddListener<EnemyMovesChosenMessage>(OnEnemyMovesChosenMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<SlotsReservedForPartyMessage>(OnReserveTimelineSlotsForPartyMessage);
            GameManager.RemoveListener<EnemyMovesChosenMessage>(OnEnemyMovesChosenMessage);
        }
    }
}