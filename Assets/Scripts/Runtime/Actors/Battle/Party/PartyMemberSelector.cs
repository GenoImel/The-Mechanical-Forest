using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Actors.Battle.Base;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Actors.Battle.Party
{
    internal sealed class PartyMemberSelector : BattleActorSelector
    {
        [SerializeField] private SerializedDictionary<SpriteRenderer, int> pipSelectors;

        public override void SetSelected(int numberOfPips)
        {
            pipSelectors.FirstOrDefault(x => x.Value == numberOfPips)
                .Key
                .enabled = true;
        }

        public override void SetDeselected()
        {
            foreach (var selector in pipSelectors)
            {
                selector.Key.enabled = false;
            }
            
            targetSelector.enabled = false;
        }
    }
}