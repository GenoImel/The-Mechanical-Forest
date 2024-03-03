using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.GameDebug;
using Akashic.Runtime.Serializers.Save;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Party
{
    internal sealed class PartyMonoSystem : MonoBehaviour, IPartyMonoSystem
    {        		
        private List<PartyMember> partyMembers = new List<PartyMember>();

		private IConfigMonoSystem configMonoSystem;
		private IDebugMonoSystem debugMonoSystem;

		private void Awake()
		{
			configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
			debugMonoSystem = GameManager.GetMonoSystem<IDebugMonoSystem>();
		}

		private void Start()
		{
			if(debugMonoSystem.IsDebugMode)
			{
				CreateNewParty();
			}
		}

		public void CreateNewParty()
		{
			partyMembers = PartyMemberConverter.
				ConvertPartyMemberDataListToPartyMemberList(
					debugMonoSystem.IsDebugMode ? 
						debugMonoSystem.GetDebugParty() : configMonoSystem.GetDefaultParty()
						);
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