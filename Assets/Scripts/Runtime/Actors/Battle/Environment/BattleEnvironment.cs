using Akashic.Runtime.Actors.Battle.Party;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Environment
{
    internal sealed class BattleEnvironment : MonoBehaviour
    {
        [SerializeField] public PartyBattleActorInstantiator partyBattleActorInstantiator;
        [SerializeField] public EnemyBattleActorInstantiator enemyBattleActorInstantiator;
        [SerializeField] private Transform initialCameraPosition;
    }
}