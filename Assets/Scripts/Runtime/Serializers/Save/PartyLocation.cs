using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class PartyLocation
    {
        [JsonProperty("roomId")]
        public string RoomId { private set; get; }
		
        [JsonProperty("spawnPointId")]
        public string SpawnPointId { private set; get; }
        
        [JsonConstructor]
        public PartyLocation(
            [JsonProperty("roomId")] string roomId,
            [JsonProperty("spawnPointId")] string spawnPointId
            )
        {
            RoomId = roomId;
            SpawnPointId = spawnPointId;
        }
    }
}