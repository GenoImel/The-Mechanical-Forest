using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class PartyMemberStats
    {
        [JsonProperty("level")]
        public int Level { private set; get; }

        [JsonProperty("hitPoints")]
        public HitPoints HitPoints { private set; get; }
        
        [JsonProperty("baseAbilityPoints")]
        public int BaseAbilityPoints { private set; get; }

        [JsonProperty("might")]
        public Might Might { private set; get; }
        
        [JsonProperty("deftness")]
        public Deftness Deftness { private set; get; }
        
        [JsonProperty("tenacity")]
        public Tenacity Tenacity { private set; get; }
        
        [JsonProperty("resolve")]
        public Resolve Resolve { private set; get; }
        
        [JsonConstructor]
        public PartyMemberStats(
            [JsonProperty("level")] int level,
            [JsonProperty("hitPoints")] HitPoints hitPoints,
            [JsonProperty("baseAbilityPoints")] int baseAbilityPoints,
            [JsonProperty("might")] Might might,
            [JsonProperty("deftness")] Deftness deftness,
            [JsonProperty("tenacity")] Tenacity tenacity,
            [JsonProperty("resolve")] Resolve resolve
            )
        {
            Level = level;
            HitPoints = hitPoints;
            BaseAbilityPoints = baseAbilityPoints;
            Might = might;
            Deftness = deftness;
            Tenacity = tenacity;
            Resolve = resolve;
        }
    }
}