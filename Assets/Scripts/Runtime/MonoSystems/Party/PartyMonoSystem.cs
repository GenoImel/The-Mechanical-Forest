using System.Collections.Generic;
using Akashic.Runtime.Controllers.BattlePartyMember;
using Akashic.Core;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Debugger;
using Akashic.Runtime.Serializers;
using Akashic.Runtime.Serializers.Party;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Party
{
    internal sealed class PartyMonoSystem : MonoBehaviour, IPartyMonoSystem
    {        		
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
			partyMembers = PartyMemberConverter.ConvertPartyMemberDataListToParyMemberList(configMonoSystem.GetDefaultParty());
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