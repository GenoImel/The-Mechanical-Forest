using UnityEngine;
using Akashic.Runtime.Utilities.Canvas;

namespace Akashic.Runtime.Common
{
    /// <summary>
    /// Contains common functionality for interacting with a CanvasGroup object.
    /// </summary>
    internal abstract class OverlayController : MonoBehaviour
    {
        [Header("Overlay CanvasGroup")]
        [SerializeField] protected CanvasGroup canvasGroup;

        public virtual void Hide()
        {
            CanvasUtilities.HideCanvas(canvasGroup);
        }

        public virtual void Show()
        {
            CanvasUtilities.ShowCanvas(canvasGroup);
        }
    }
}
