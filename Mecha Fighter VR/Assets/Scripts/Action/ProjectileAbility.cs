using Stats;
using UnityEngine;

namespace Action
{
    public class ProjectileAbility : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private Transform shootPoint = null;
        [SerializeField] private float baseDamage = 10f;
        [SerializeField] private float energyCost = 100f;
        [SerializeField] private float velocity = 8f;
        [SerializeField] private float cooldown = 1f;

        private Energy energy;
        // Time since the projectile was last fired
        private float cooldownTimer = Mathf.Infinity;

        private void Awake()
        {
            energy = GetComponent<Energy>();
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        public void ShootProjectile(GameObject opponent)
        {
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
            projectile.SetTarget(opponent);
            projectile.SetBaseDamage(baseDamage);
            projectile.SetVelocity(velocity);
            cooldownTimer = 0f;
        }
    }
}
