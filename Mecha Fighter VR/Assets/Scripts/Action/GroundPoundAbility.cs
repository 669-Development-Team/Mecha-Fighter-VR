using Stats;
using UnityEngine;

namespace Action
{
    public class GroundPoundAbility : SpecialAbility
    {
        [SerializeField] private GroundPound groundPoundPrefab = null;
        [SerializeField] private Transform effectSpawnPoint = null;
        private Health m_opponent = null;

        public override void ActivateAbility(Health opponent)
        {
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
            m_opponent = opponent;
            m_animator.SetTrigger("GroundPound");
        }

        // Animation event
        public void InstantiateGroundPoundFx()
        {
            GroundPound groundPound = Instantiate(groundPoundPrefab, effectSpawnPoint.position, Quaternion.identity);
            groundPound.SetTarget(m_opponent, gameObject, m_damageStat.GetSpecialDamage() + bonusDamage);
            cooldownTimer = 0f;
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
