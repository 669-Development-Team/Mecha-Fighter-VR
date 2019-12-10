using Stats;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Displays useful information to the player
    /// </summary>
    public class PlayerHudDisplay : MonoBehaviour
    {
        // Reference the game objects in the inspector
        public TextMeshProUGUI healthPercentageText;
        public TextMeshProUGUI energyPercentageText;
        public TextMeshProUGUI shieldLPercentageText;
        public TextMeshProUGUI shieldRPercentageText;
        public RectTransform HealthBar;
        public RectTransform EnergyBar;
        public RectTransform LShieldBar;
        public RectTransform RShieldBar;

        // The health displayed on the Mech
        public TextMeshProUGUI mechHealthPercentageText;
        public RectTransform mechHealthBar;

        [SerializeField] private PlayerStats stats = null;
        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;

        private void Update()
        {
            /*
             * The question marks "?" is a shorthand way of checking each component for null before calling a function on them
             */
            healthPercentageText?.SetText($"{stats.GetHealthPercentage():0}%");
            energyPercentageText?.SetText($"{stats.GetEnergyPercentage():0}%");
            shieldLPercentageText?.SetText($"{leftShield.GetDurabilityPercentage():0}%");
            shieldRPercentageText?.SetText($"{rightShield.GetDurabilityPercentage():0}%");
            HealthBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.GetHealthPercentage());
            EnergyBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.GetEnergyPercentage());
            LShieldBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, leftShield.GetDurabilityPercentage());
            RShieldBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rightShield.GetDurabilityPercentage());

            mechHealthPercentageText?.SetText($"{stats.GetHealthPercentage():0}%");
            mechHealthBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stats.GetHealthPercentage());
        }
    }
}
