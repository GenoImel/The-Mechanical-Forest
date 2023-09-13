using UnityEngine;
using System.Threading.Tasks;
using Akashic.Runtime.Common;
using Akashic.Runtime.Utilities.Canvas;


namespace Akashic.Runtime.Controllers.LoadingCurtain
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