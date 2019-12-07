using UnityEngine;
using UnityEngine.Events;

namespace Stats
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] private float maxEnergy = 500f;
        [SerializeField] private GameObject replenishEffect = null;
        [Tooltip("Transform of the mech so that the effect spawns at the mech's feet")]
        [SerializeField] private Transform mechRoot = null;

        [SerializeField] private UnityEvent onReplenish = null;

        private float currentEnergy;

        private void Start()
        {
            currentEnergy = maxEnergy;
        }

        void Update()
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy = Mathf.Min(currentEnergy + 10f * Time.deltaTime, maxEnergy);
            }

            currentEnergy = maxEnergy;
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

        public void Replenish(float energyToRestore)
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy = Mathf.Min(currentEnergy + energyToRestore, maxEnergy);
            }

            GameObject effect = Instantiate(replenishEffect, mechRoot);
            Destroy(effect, 1f);
            onReplenish.Invoke();
        }
    }
}
