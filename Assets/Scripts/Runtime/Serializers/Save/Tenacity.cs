using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class Tenacity
    {
        [JsonProperty("baseTenacity")]
        public int BaseTenacity { private set; get; }
        
        [JsonProperty("calculatedTenacity")]
        public int CalculatedTenacity { private set; get; }
        
        [JsonConstructor]
        public Tenacity(
            [JsonProperty("baseTenacity")] int baseTenacity,
            [JsonProperty("calculatedTenacity")] int calculatedTenacity
            )
        {
            BaseTenacity = baseTenacity;
            CalculatedTenacity = calculatedTenacity;
        }
    }
}