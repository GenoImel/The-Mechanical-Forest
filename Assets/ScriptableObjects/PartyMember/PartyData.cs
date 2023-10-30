using Akashic.ScriptableObjects.PartyMember;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic
{
	[CreateAssetMenu(menuName = "Akashic/Party Member/New Party")]
	internal sealed class PartyData : ScriptableObject
	{
		/// <summary>
		/// 
		/// </summary>
		public List<PartyMemberData> partyMembers = new List<PartyMemberData>();
    }
}
