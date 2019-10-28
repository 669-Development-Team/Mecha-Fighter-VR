using Stats;
using UnityEngine;

namespace Action
{
    public class ProjectileAbility : SpecialAbility
    {
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private Transform shootPoint = null;
        [SerializeField] private float velocity = 8f;

        private Energy energy;

        private void Awake()
        {
            energy = GetComponent<Energy>();
        }

        public override void ActivateAbility(Health opponent)
        {
            Debug.Log("Projectile gesture performed!");

            if (cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!energy.Deplete(energyCost))
            {
                return;
            }

            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.SetTarget(opponent, gameObject, velocity, baseDamage);
            cooldownTimer = 0f;
        }
    }
}
