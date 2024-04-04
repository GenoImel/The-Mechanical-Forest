using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.PartyMember
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
