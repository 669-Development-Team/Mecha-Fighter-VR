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

        [SerializeField] private Health health = null;
        [SerializeField] private Energy energy = null;

        private void Update()
        {
            healthDisplayText.text = $"Health: {health.GetHealthPercentage():0}%";
            energyDisplayText.text = $"Energy: {energy.GetEnergyPercentage():0}%";
        }
    }
}
