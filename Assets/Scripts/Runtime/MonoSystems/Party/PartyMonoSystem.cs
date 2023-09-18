using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Controllers.PartyMemberBattle;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Party
{
    internal sealed class PartyMonoSystem : MonoBehaviour, IPartyMonoSystem
    {
        [Header("Party Member Prefabs")]
        [SerializeField]private PartyMemberController airyPrefab;
        
        [SerializeField]private PartyMemberController benoitPrefab;
        
        [SerializeField]private PartyMemberController conradPrefab;
        
        [SerializeField]private PartyMemberController lenaPrefab;
        
        private ICollection<PartyMemberController> partyMembers = new List<PartyMemberController>();

        public void CreateNewParty()
        {
            CreateNewAiry();
            CreateNewBenoit();
            CreateNewConrad();
            CreateNewLena();
        }

        public List<PartyMemberController> GetPartyMembers()
        {
            return partyMembers.ToList();
        }

        public void LoadPartyFromSaveFile()
        {
            
        }

        private void AddNewPartyMember(PartyMemberController partyMemberControllerPrefab)
        {
            var partyMember = Instantiate(partyMemberControllerPrefab, transform);
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