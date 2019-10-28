using Stats;
using UnityEngine;

namespace Action
{
    public class GroundPoundAbility : SpecialAbility
    {
        [SerializeField] private GroundPound groundPoundPrefab = null;
        [SerializeField] private Transform effectSpawnPoint = null;

        private Energy energy;

        private void Awake()
        {
            energy = GetComponent<Energy>();
        }

        public override void ActivateAbility(Health opponent)
        {
            Debug.Log("Ground Pound gesture performed!");

            if (cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!energy.Deplete(energyCost))
            {
                return;
            }

            GroundPound groundPound = Instantiate(groundPoundPrefab, effectSpawnPoint.position, Quaternion.identity);
            groundPound.SetTarget(opponent, gameObject, baseDamage);
            cooldownTimer = 0f;
        }
    }
}
