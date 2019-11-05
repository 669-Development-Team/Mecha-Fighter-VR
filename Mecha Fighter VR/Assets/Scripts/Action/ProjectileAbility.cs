using Stats;
using UnityEngine;

namespace Action
{
    [RequireComponent(typeof(DamageStat))]
    public class ProjectileAbility : MonoBehaviour
    {
        [SerializeField] private float bonusDamage = 10f;
        [SerializeField] private float energyCost = 100f;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private Transform shootPoint = null;
        [SerializeField] private float velocity = 8f;

        // Time since the projectile was last fired
        private float m_cooldownTimer = Mathf.Infinity;

        private Animator m_animator;
        private DamageStat m_damageStat;
        private Energy m_energy;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_damageStat = GetComponent<DamageStat>();
            m_energy = GetComponent<Energy>();
        }

        private void Update()
        {
            m_cooldownTimer += Time.deltaTime;
        }

        public void ActivateAbility()
        {
            Debug.Log("Projectile gesture performed!");

            if (m_cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            m_animator.SetTrigger("Projectile");

        }

        public void InstantiateProjectileFx()
        {
            // !! Collision checks are now performed with LAYERS !!
            // !! May need to reconfigure layers when multiplayer is introduced !!
            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
            projectile.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            projectile.SetValues(gameObject, velocity, m_damageStat.GetSpecialDamage() + bonusDamage);
            m_cooldownTimer = 0f;
        }
    }
}
