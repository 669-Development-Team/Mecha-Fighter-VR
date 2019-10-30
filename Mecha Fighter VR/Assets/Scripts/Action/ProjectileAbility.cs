using Stats;
using UnityEngine;

namespace Action
{
    public class ProjectileAbility : SpecialAbility
    {
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private Transform shootPoint = null;
        [SerializeField] private float velocity = 8f;

        public override void ActivateAbility(Health opponent)
        {
            Debug.Log("Projectile gesture performed!");

            if (cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.SetTarget(opponent, gameObject, velocity, m_damageStat.GetSpecialDamage() + bonusDamage);
            cooldownTimer = 0f;
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
