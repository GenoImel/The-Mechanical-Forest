using Akashic.Runtime.Actors.Battle.Party;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.BattleHud
{
    internal sealed class PartyMemberBattleProfile : MonoBehaviour
    {
        [SerializeField] private Image profilePicture;

        [SerializeField] private PartyMemberHealthBar healthBar;
        
        [SerializeField] private TMP_Text partyMemberHealthText;

        [SerializeField] private TMP_Text partyMemberNameText;

        private PartyBattleActor partyBattleActor;

        public void InitializeBattleProfile(PartyBattleActor battleActor)
        {
            partyBattleActor = battleActor;
            
            partyMemberNameText.text = partyBattleActor.ActorName;
            
            healthBar.SetBarFillByPercent((float)partyBattleActor.statHandler.CurrentHitPoints /
                                          partyBattleActor.statHandler.MaxHitPoints);
            partyMemberHealthText.text =
                $"{partyBattleActor.statHandler.CurrentHitPoints}";
        }
    }
}