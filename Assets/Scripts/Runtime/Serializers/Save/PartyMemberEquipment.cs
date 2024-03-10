using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class PartyMemberEquipment
    {
        [JsonProperty("weaponSlot")]
        public WeaponItem WeaponSlot { private set; get; }
        
        [JsonProperty("armorSlot")]
        public ArmorItem ArmorSlot { private set; get; }

        [JsonProperty("relicSlot")]
        public RelicItem RelicSlot { private set; get; }
        
        [JsonProperty("accessorySlots")]
        public List<AccessoryItem> AccessorySlots { private set; get; }

        [JsonConstructor]
        public PartyMemberEquipment(
            [JsonProperty("weaponSlot")] WeaponItem weaponSlot,
            [JsonProperty("armorSlot")] ArmorItem armorSlot,
            [JsonProperty("relic")] RelicItem relicSlot,
            [JsonProperty("accessory")] List<AccessoryItem> accessories 
        )
        {
            WeaponSlot = weaponSlot;
            ArmorSlot = armorSlot;
            RelicSlot = relicSlot;
            AccessorySlots = accessories;
        }
    }
}