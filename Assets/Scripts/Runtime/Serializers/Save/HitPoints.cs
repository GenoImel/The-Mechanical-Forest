using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class HitPoints
    {
        [JsonProperty("currentHitPoints")]
        public int CurrentHitPoints { private set; get; }
        
        [JsonProperty("maxHitPoints")]
        public int MaxHitPoints { private set; get; }
        
        [JsonIgnore]
        public int BufferHitPoints { get; set; }

        [JsonConstructor]
        public HitPoints(
            [JsonProperty("hitPoints")] int hitPoints,
            [JsonProperty("maxHitPoints")] int maxHitPoints
            )
        {
            CurrentHitPoints = hitPoints;
            MaxHitPoints = maxHitPoints;
        }
    }
}