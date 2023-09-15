using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Scripts.ConfigBase
{
    [CreateAssetMenu(menuName = "Akashic/Config/New Config File")]
    public sealed class ConfigBaseData : ScriptableObject
    {
        public List<string> saveSlotFileNames = new List<string>(3);
    }
}