using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Akashic.Runtime.Utilities.Canvas
{
    /// <summary>
    /// 'CanvasUtilities' handles utility functions for loading curtains
    /// ie fading in and out
    /// </summary>
    public static class CanvasUtilities 
    {
        /// <summary>
        /// Hides or shows a canvas without animation.
        /// </summary>
        public static void HideCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        
        public static void ShowCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        /// <summary>
        /// Instructs a canvas group to be shown
        /// </summary>
        public static async Task FadeInCanvasAsync(CanvasGroup canvasGroup, float seconds)
        {
            await FadeRoutineAsync(1f, canvasGroup, seconds, () =>
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            });
        }

        /// <summary>
        /// Instructs a canvas group to be Hidden
        /// </summary>
        public static async Task FadeOutCanvasAsync(CanvasGroup canvasGroup, float seconds)
        {
            await FadeRoutineAsync(0f, canvasGroup, seconds, () =>
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            });
        }

        private static async Task FadeRoutineAsync(float to, CanvasGroup canvasGroup, float seconds, Action onComplete)
        {
            float realTime = 0;
            float startAlpha = canvasGroup.alpha;

            while (realTime <= seconds) 
            {
                realTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, to, realTime / seconds);
                await Task.Yield();
            }

            canvasGroup.alpha = to;
            onComplete.Invoke();
        }
    }
}
