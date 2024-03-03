using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class PartyMemberEquipment
    {
        [JsonProperty("weaponSlot")]
        public string WeaponSlot { private set; get; }
        
        [JsonProperty("armorSlot")]
        public string ArmorSlot { private set; get; }

        [JsonProperty("relicSlot")]
        public string RelicSlot { private set; get; }
        
        [JsonProperty("accessorySlots")]
        public List<string> AccessorySlots { private set; get; }

        [JsonConstructor]
        public PartyMemberEquipment(
            [JsonProperty("weaponSlot")] string weaponSlot,
            [JsonProperty("armorSlot")] string armorSlot,
            [JsonProperty("relic")] string relicSlot,
            [JsonProperty("accessory")] List<string> accessories 
        )
        {
            WeaponSlot = weaponSlot;
            ArmorSlot = armorSlot;
            RelicSlot = relicSlot;
            AccessorySlots = accessories;
        }
    }
}