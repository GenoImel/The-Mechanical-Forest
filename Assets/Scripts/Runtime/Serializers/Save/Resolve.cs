using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class Resolve
    {
        [JsonProperty("baseResolve")]
        public int BaseResolve { private set; get; }
        
        [JsonProperty("calculatedResolve")]
        public int CalculatedResolve { private set; get; }
        
        [JsonConstructor]
        public Resolve(
            [JsonProperty("baseResolve")] int baseResolve,
            [JsonProperty("calculatedResolve")] int calculatedResolve
            )
        {
            BaseResolve = baseResolve;
            CalculatedResolve = calculatedResolve;
        }
    }
}