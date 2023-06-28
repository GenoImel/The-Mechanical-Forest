using System.Threading.Tasks;
using Akashic.Runtime.Utilities;
using UnityEngine;

namespace Akashic.Runtime.Controllers.LoadingCurtain
{
    internal class CurtainController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] protected CanvasGroup canvasGroup;

        [Header("Tween Settings")] 
        [SerializeField] protected float fadeDurationSeconds;

        protected virtual void Start()
        {
            CanvasUtilities.HideCanvas(canvasGroup);
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