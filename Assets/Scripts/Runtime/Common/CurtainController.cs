using System.Threading.Tasks;
using Akashic.Runtime.Utilities.Canvas;
using UnityEngine;

namespace Akashic.Runtime.Common
{
    internal class CurtainController : OverlayController
    {
        [Header("Tween Settings")] 
        [SerializeField] protected float fadeDurationSeconds;

        protected virtual void Start()
        {
            Hide();
        }

        public virtual async Task ShowCurtain()
        {
            await CanvasUtilities.FadeInCanvasAsync(canvasGroup, fadeDurationSeconds);
        }

        public virtual async Task HideCurtain()
        {
            await CanvasUtilities.FadeOutCanvasAsync(canvasGroup, fadeDurationSeconds);
        }
    }
}