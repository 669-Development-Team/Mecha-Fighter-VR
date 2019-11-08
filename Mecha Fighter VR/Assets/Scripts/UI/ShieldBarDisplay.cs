using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Displays useful information to the player
    /// </summary>
    public class ShieldBarDisplay : MonoBehaviour
    {
        // Reference the game objects in the inspector
        public TextMeshProUGUI shieldLPercentageText;
        public TextMeshProUGUI shieldRPercentageText;
        public RectTransform LShieldBar;
        public RectTransform RShieldBar;

        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;

        private void Update()
        {
            /*
             * The question marks "?" is a shorthand way of checking each component for null before calling a function on them
             */
            shieldLPercentageText?.SetText($"{leftShield.GetDurabilityPercentage():0}%");
            shieldRPercentageText?.SetText($"{rightShield.GetDurabilityPercentage():0}%");
            LShieldBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, leftShield.GetDurabilityPercentage());
            RShieldBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rightShield.GetDurabilityPercentage());
        }
    }
}
