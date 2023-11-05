using System.Collections.Generic;
using Akashic.Runtime.Controllers.BattlePartyMember;
using Akashic.Runtime.Serializers;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Party
{
    internal sealed class PartyMonoSystem : MonoBehaviour, IPartyMonoSystem
    {
        [Header("Party Member Prefabs")]
        [SerializeField]private BattlePartyMemberController airyPrefab;
        
        [SerializeField]private BattlePartyMemberController benoitPrefab;
        
        [SerializeField]private BattlePartyMemberController conradPrefab;
        
        [SerializeField]private BattlePartyMemberController lenaPrefab;
        
        private ICollection<BattlePartyMemberController> partyMembers = new List<BattlePartyMemberController>();

        public void CreateNewParty()
        {
            CreateNewAiry();
            CreateNewBenoit();
            CreateNewConrad();
            CreateNewLena();
        }

        public List<PartyMember> GetPartyMembers()
        {
            return new List<PartyMember>();
        }

        public void LoadPartyFromSaveFile()
        {
            
        }

        private void AddNewPartyMember(BattlePartyMemberController battlePartyMemberControllerPrefab)
        {
            var partyMember = Instantiate(battlePartyMemberControllerPrefab, transform);
            partyMember.InitializeNewPartyMemberFromScriptableObject();
            partyMembers.Add(partyMember);
        }

        private void CreateNewAiry()
        {
            AddNewPartyMember(airyPrefab);
        }

        private void CreateNewBenoit()
        {
            AddNewPartyMember(benoitPrefab);
        }

        private void CreateNewConrad()
        {
            AddNewPartyMember(conradPrefab);
        }

        private void CreateNewLena()
        {
            AddNewPartyMember(lenaPrefab);
        }
    }
}