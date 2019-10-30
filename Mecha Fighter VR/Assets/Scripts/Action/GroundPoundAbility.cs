using Stats;
using UnityEngine;

namespace Action
{
    public class GroundPoundAbility : SpecialAbility
    {
        [SerializeField] private GroundPound groundPoundPrefab = null;
        [SerializeField] private Transform effectSpawnPoint = null;

        public override void ActivateAbility(Health opponent)
        {
            Debug.Log("Ground Pound gesture performed!");
            m_animator.SetBool("GroundPound", true);

            if (cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            GroundPound groundPound = Instantiate(groundPoundPrefab, effectSpawnPoint.position, Quaternion.identity);
            groundPound.SetTarget(opponent, gameObject, m_damageStat.GetSpecialDamage() + bonusDamage);
            cooldownTimer = 0f;
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
