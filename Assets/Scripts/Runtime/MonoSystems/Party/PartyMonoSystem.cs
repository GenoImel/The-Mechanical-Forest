using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Controllers.PartyMemberBattle;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Debugger;
using Akashic.Runtime.Serializers;
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
        		
        private List<PartyMember> partyMembers = new List<PartyMember>();

		private IConfigMonoSystem configMonoSystem;
		private IDebuggerMonoSystem debuggerMonoSystem;

		private void Awake()
		{
			configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
			debuggerMonoSystem = GameManager.GetMonoSystem<IDebuggerMonoSystem>();
		}

		public void CreateNewParty()
		{
            partyMembers = new List<PartyMember>();
		}

        public List<PartyMember> GetPartyMembers()
        {
            return partyMembers;
        }

        public void LoadPartyFromSaveFile()
        {
            
        }

    }
}