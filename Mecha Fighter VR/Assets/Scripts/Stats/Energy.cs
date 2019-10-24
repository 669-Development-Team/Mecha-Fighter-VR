using UnityEngine;

namespace Stats
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] private float maxEnergy = 500f;

        private float currentEnergy;

        private void Start()
        {
            currentEnergy = maxEnergy;
        }

        void Update()
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += 10f * Time.deltaTime;
            }
        }

        // Percentage of current / max, used for HUD
        public float GetEnergyPercentage()
        {
            return 100f * (currentEnergy / maxEnergy);
        }

        // Abilities activated by gestures consume energy
        public bool Deplete(float energyCost)
        {
            if (currentEnergy < energyCost)
            {
                return false;
            }

            currentEnergy -= energyCost;
            return true;
        }
    }
}
