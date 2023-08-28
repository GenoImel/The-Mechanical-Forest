using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers
{
    internal sealed class SaveFile
    {
        [JsonProperty("party")]
        public List<PartyMember> PartyMembers { private set; get; }
        
        [JsonConstructor]
        public SaveFile([JsonProperty("party")] List<PartyMember> partyMembers)
        {
            PartyMembers = partyMembers;
        }
    }
}