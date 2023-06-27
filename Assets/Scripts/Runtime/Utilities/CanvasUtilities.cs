using UnityEngine;
using System;
using System.Threading.Tasks;

namespace Akashic.Runtime.Utilities
{
    /// <summary>
    /// 'CanvasUtilities' handles utility functions for loading curtains
    /// ie fading in and out
    /// </summary>
    public static class CanvasUtilities 
    {
        /// <summary>
        /// Instructs a canvas group to be shown
        /// </summary>
        public static async Task ShowCurtain(CanvasGroup canvasGroup, float seconds)
        {
            await FadeRoutine(1f, canvasGroup, seconds, () =>
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            });
        }

        /// <summary>
        /// Instructs a canvas group to be Hidden
        /// </summary>
        public static async Task HideCurtain(CanvasGroup canvasGroup, float seconds)
        {
            await FadeRoutine(0f, canvasGroup, seconds, () =>
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            });
        }

        private static async Task FadeRoutine(float to, CanvasGroup canvasGroup, float seconds, Action onComplete)
        {
            float endTime = Time.time + seconds;
            float startAlpha = canvasGroup.alpha;

            while (endTime > Time.time) 
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, to, Time.time / endTime);
                await Task.Yield();
            }

            onComplete.Invoke();
        }
    }
}
