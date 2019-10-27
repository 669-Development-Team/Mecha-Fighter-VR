using UnityEngine;

namespace Stats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 500f;

        private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        // Percentage of current / max, used for HUD
        public float GetHealthPercentage()
        {
            return 100f * (currentHealth / maxHealth);
        }
    }
}