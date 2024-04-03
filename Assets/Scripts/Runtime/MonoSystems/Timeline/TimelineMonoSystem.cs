using System.Collections.Generic;
using System.Linq;
using Akashic.Core;
using Akashic.Runtime.StateMachines.TurnStates;
using Akashic.Runtime.Utilities.Random;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Timeline
{
    internal sealed class TimelineMonoSystem : MonoBehaviour, ITimelineMonoSystem
    {
        [SerializeField] private int numberOfTimelineMoves = 12;

        private IList<TimelineMove> timelineMoves = new List<TimelineMove>();

        public List<TimelineMove> TimelineMoves => timelineMoves.ToList();
        
        private ITurnStateMachine turnStateMachine;
        
        private void Awake()
        {
            turnStateMachine = GameManager.GetStateMachine<ITurnStateMachine>();
        }

        private void Start()
        {
            for (var i = 0; i < numberOfTimelineMoves; i++)
            {
                timelineMoves.Add(new TimelineMove());
            }
        }
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void ReserveMovesForParty()
        {
            var totalMoves = timelineMoves.Count;
            var thirdSize = totalMoves / 3;
            var availableIndices = new HashSet<int>();
            
            for (int i = 0; i < totalMoves; i++)
            {
                availableIndices.Add(i);
            }
            
            var firstThirdMoveIndex = Random.Range(0, thirdSize);
            timelineMoves[firstThirdMoveIndex].ReserveForParty();
            availableIndices.Remove(firstThirdMoveIndex);
            
            var lastThirdMoveIndex = Random.Range(2 * thirdSize, totalMoves);
            timelineMoves[lastThirdMoveIndex].ReserveForParty();
            availableIndices.Remove(lastThirdMoveIndex);
            
            var remainingIndices = new List<int>(availableIndices);
            remainingIndices = Shufflers.FisherYates(remainingIndices);
            
            for (var i = 0; i < 2; i++)
            {
                timelineMoves[remainingIndices[i]].ReserveForParty();
            }
        }

        private void ClearTimeline()
        {
            timelineMoves = new List<TimelineMove>(numberOfTimelineMoves);
            
            for (var i = 0; i < numberOfTimelineMoves; i++)
            {
                timelineMoves.Add(new TimelineMove());
            }
        }

        private void OnTurnStateChangedMessage(TurnStateChangedMessage message)
        {
            if (message.NextState is TurnFiniteState.Promise)
            {
                ReserveMovesForParty();
                GameManager.Publish(new SlotsReservedForPartyMessage());
                turnStateMachine.SetEnemyPlanningState();
            }

            if (message.NextState is TurnFiniteState.PostExecution)
            {
                GameManager.Publish(new TimelineResetMessage());
            }
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
        }
    }
}