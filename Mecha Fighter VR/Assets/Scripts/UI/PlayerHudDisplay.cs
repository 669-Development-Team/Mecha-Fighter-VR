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
        public TextMeshProUGUI healthDisplayText;
        public TextMeshProUGUI energyDisplayText;
        public TextMeshProUGUI leftShieldDisplayText;
        public TextMeshProUGUI rightShieldDisplayText;
        public RectTransform HealthBar;
        public RectTransform EnergyBar;
        public RectTransform LShieldBar;
        public RectTransform RShieldBar;

        [SerializeField] private Health health = null;
        [SerializeField] private Energy energy = null;
        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;

        private void Update()
        {
            healthDisplayText?.SetText($"Health: {health.GetHealthPercentage():0}%");
            energyDisplayText?.SetText($"Energy: {energy.GetEnergyPercentage():0}%");
            leftShieldDisplayText?.SetText($"L Shield: {leftShield.GetDurabilityPercentage():0}%");
            rightShieldDisplayText?.SetText($"R Shield: {rightShield.GetDurabilityPercentage():0}%");
            HealthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health.GetHealthPercentage());
            EnergyBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, energy.GetEnergyPercentage());
            LShieldBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, leftShield.GetDurabilityPercentage());
            RShieldBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rightShield.GetDurabilityPercentage());
        }
    }
}
