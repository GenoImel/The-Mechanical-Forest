using System.Collections.Generic;
using Akashic.Runtime.Serializers.Party;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers
{
    internal sealed class SaveFile
    {
        [JsonProperty("saveFileName")]
        public string SaveFileName { private set; get; }

		[JsonProperty("party")]
		public List<PartyMember> PartyMembers { private set; get; }

		[JsonProperty("inventory")]
		public PartyInventory PartyInventory { private set; get; }

		[JsonConstructor]
        public SaveFile(
            [JsonProperty("saveFileName")] string saveFileName,
			[JsonProperty("party")] List<PartyMember> partyMembers,
			[JsonProperty("inventory")] PartyInventory partyInventory
			)
        {
            SaveFileName = saveFileName;
			PartyMembers = partyMembers;
			PartyInventory = partyInventory;
		}
    }
}