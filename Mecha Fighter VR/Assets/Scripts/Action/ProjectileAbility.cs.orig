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
            Debug.Log("Projectile gesture performed!");
        }

        public void InstantiateProjectileFx()
        {
            Debug.Log("instantiating");

            // !! Collision checks are now performed with LAYERS !!
            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
            projectile.gameObject.layer = gameObject.layer;
            projectile.SetValues(gameObject, velocity, m_damageStat.GetSpecialDamage() + bonusDamage);
            m_cooldownTimer = 0f;
        }
    }
}
