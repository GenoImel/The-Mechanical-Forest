using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class BattleEnvironment : MonoBehaviour
    {
        [SerializeField] public PartyBattleActorInstantiator partyBattleActorInstantiator;
        [SerializeField] public EnemyBattleActorInstantiator enemyBattleActorInstantiator;
        [SerializeField] private Transform initialCameraPosition;
    }
}