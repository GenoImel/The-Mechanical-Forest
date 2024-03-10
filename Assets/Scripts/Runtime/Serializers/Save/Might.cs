using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    internal sealed class Might
    {
        [JsonProperty("baseMight")]
        public int BaseMight { private set; get; }
        
        [JsonProperty("calculatedMight")]
        public int CalculatedMight { private set; get; }
        
        [JsonConstructor]
        public Might(
            [JsonProperty("baseMight")] int baseMight,
            [JsonProperty("calculatedMight")] int calculatedMight
            )
        {
            BaseMight = baseMight;
            CalculatedMight = calculatedMight;
        }
        
        public void SetBaseMight(int baseMight)
        {
            BaseMight = baseMight;
        }
        
        public void SetCalculatedMight(int calculatedMight)
        {
            CalculatedMight = calculatedMight;
        }
    }
}