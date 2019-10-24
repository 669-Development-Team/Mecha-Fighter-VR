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

        private Health health;
        private Energy energy;

        private void Awake()
        {
            // TODO: There may be a better way of doing this considering we will have multiplayer but this works for now
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            energy = GameObject.FindWithTag("Player").GetComponent<Energy>();
        }

        private void Update()
        {
            healthDisplayText.text = $"Health: {health.GetHealthPercentage():0}%";
            energyDisplayText.text = $"Energy: {energy.GetEnergyPercentage():0}%";
        }
    }
}
