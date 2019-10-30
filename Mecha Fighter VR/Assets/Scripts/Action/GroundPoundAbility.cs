using Stats;
using UnityEngine;

namespace Action
{
    public class GroundPoundAbility : SpecialAbility
    {
        [SerializeField] private GroundPound groundPoundPrefab = null;
        [SerializeField] private Transform effectSpawnPoint = null;
        private Health opponentHealth;

        public override void ActivateAbility(Health opponent)
        {
            opponentHealth = opponent;

            if (cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            Debug.Log("Ground Pound gesture performed!");
            m_animator.SetBool("GroundPound", true);
        }

        public void instantiateGroundPoundFX()
        {
            GroundPound groundPound = Instantiate(groundPoundPrefab, effectSpawnPoint.position, Quaternion.identity);
            groundPound.SetTarget(opponentHealth, gameObject, m_damageStat.GetSpecialDamage() + bonusDamage);
            cooldownTimer = 0f;
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
