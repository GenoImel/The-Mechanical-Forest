using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Akashic.Runtime.Controllers.LoadingCurtain
{
    internal sealed class LoadingCurtainController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Tween Settings")] 
        [SerializeField] private float fadeDurationSeconds;

        private void Start()
        {
            StartCoroutine(HideCurtain());
        }

        public IEnumerator ShowCurtain()
        {
            return FadeRoutine(1f, () =>
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            });
        }

        public IEnumerator HideCurtain()
        {
            return FadeRoutine(0f, () =>
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            });
        }

        private IEnumerator FadeRoutine(float to, Action onComplete)
        {
            yield return canvasGroup
                .DOFade(to, fadeDurationSeconds)
                .OnComplete(onComplete.Invoke)
                .SetUpdate(true)
                .WaitForCompletion();
        }
    }
}