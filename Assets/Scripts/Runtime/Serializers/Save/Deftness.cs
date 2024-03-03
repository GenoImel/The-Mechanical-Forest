using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class Deftness
    {
        [JsonProperty("baseDeftness")]
        public int BaseDeftness { private set; get; }
        
        [JsonProperty("calculatedDeftness")]
        public int CalculatedDeftness { private set; get; }
        
        [JsonConstructor]
        public Deftness(
            [JsonProperty("baseDeftness")] int baseDeftness,
            [JsonProperty("calculatedDeftness")] int calculatedDeftness
            )
        {
            BaseDeftness = baseDeftness;
            CalculatedDeftness = calculatedDeftness;
        }
    }
}