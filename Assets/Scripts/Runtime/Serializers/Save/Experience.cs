using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class Experience
    {
        [JsonProperty("currentExperience")]
        public int CurrentExperience { private set; get; }

        [JsonProperty("maxExperience")]
        public int MaxExperience { private set; get; }
        
        [JsonConstructor]
        public Experience(
            [JsonProperty("currentExperience")] int currentExperience,
            [JsonProperty("maxExperience")] int maxExperience
            )
        {
            CurrentExperience = currentExperience;
            MaxExperience = maxExperience;
        }
    }
}