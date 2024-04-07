using UnityEngine.UI;
using UnityEngine;

namespace Akashic.Runtime.Controllers.BattleHud
{
    internal sealed class PartyMemberHealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBarFill;

        public void SetBarFillByPercent(float percentToFill)
        {
            healthBarFill.transform.localScale = new Vector3(percentToFill, 1, 1);
        }
    }
}