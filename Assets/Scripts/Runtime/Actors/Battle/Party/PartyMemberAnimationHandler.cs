using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberAnimationHandler : MonoBehaviour
    {
        [SerializeField] private PipsContainer pipsContainer;

        public void SpendPips(int numberToSpend)
        {
            for (int i = 0; i < numberToSpend; i++)
            {
                pipsContainer.SpendPip();
            }
        }
    }
}