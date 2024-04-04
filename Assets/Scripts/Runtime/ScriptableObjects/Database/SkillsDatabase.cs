using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Skills Database")]
    internal sealed class SkillsDatabase : ScriptableObject
    {
        public List<SkillData> skills = new List<SkillData>();
    }
}