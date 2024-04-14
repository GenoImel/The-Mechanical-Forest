using UnityEngine;

namespace Akashic.Runtime.Utilities.Sprites
{
    internal sealed class SpriteBillboard : MonoBehaviour
    {
        public Transform lookTarget;

        private Transform halo;
        private Camera cam;


        private void Start()
        {
            cam = Camera.main;
            halo = transform.GetChild(0); ;
        }

        private void LateUpdate()
        {
            if (!lookTarget)
            {
                lookTarget = cam.transform; 
            }

            transform.LookAt(lookTarget);

            //TO DO: introduce scale later
            //var dist = Vector3.Distance(halo.transform.position, cam.transform.position);
            //var scale = .3f / dist * (39 - dist);

            //scale = Mathf.Clamp(scale, 0, 0.33f);
            //var targetScale = new Vector3(1 + scale, 1 + scale, 1 + scale);

            //halo.localScale = Vector3.Lerp(halo.localScale, targetScale, Time.deltaTime * 1000);
            halo.localScale = Vector3.Lerp(halo.localScale, halo.localScale, Time.deltaTime * 1000);

        }
    }
}