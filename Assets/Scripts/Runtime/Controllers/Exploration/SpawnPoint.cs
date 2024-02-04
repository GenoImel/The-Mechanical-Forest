using UnityEngine;

namespace Akashic.Runtime.Controllers.Exploration
{
    internal sealed class SpawnPoint : MonoBehaviour
    {
        public string spawnPointName;
        public string spawnPointDescription;
        public string spawnPointId;
        public Transform initialCameraPlacement;
    }
}