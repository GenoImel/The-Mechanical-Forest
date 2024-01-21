using UnityEngine;

namespace Akashic.Runtime.Utilities.Sprites
{
    internal sealed class SpriteBillboard : MonoBehaviour
    {
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(mainCamera.transform);
        }
    }
}