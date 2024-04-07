using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PipsContainer : MonoBehaviour
    {
        [SerializeField] private List<PipActor> pipActors;

        private void Awake()
        {
            HidePips();
        }

        public void HidePips()
        {
            foreach (var pip in pipActors)
            {
                pip.HidePip();
            }
        }

        public void ShowPips()
        {
            foreach (var pip in pipActors)
            {
                pip.ShowPip();
            }
        }

        public void SpendPip()
        {
            var firstAvailable = pipActors.FirstOrDefault(pip => pip.IsAvailable);

            if (firstAvailable != null)
            {
                firstAvailable.SetPipSpent();
            }
        }
    }
}