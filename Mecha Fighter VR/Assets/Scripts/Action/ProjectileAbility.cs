using UnityEngine;

namespace Action
{
    public class ProjectileAbility : MonoBehaviour
    {
        [SerializeField] private float energyCost = 100f;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private Transform shootPoint = null;
        [SerializeField] private float velocity = 8f;

        // Time since the projectile was last fired
        private float m_cooldownTimer = Mathf.Infinity;

        private Animator m_animator;
        private PlayerStats m_playerStats;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_playerStats = GetComponent<PlayerStats>();
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
            if (!m_playerStats.DepleteEnergy(energyCost))
            {
                return;
            }

            m_animator.SetTrigger("Projectile");
            Debug.Log("Projectile gesture performed!");
        }

        // Animation event, called at a certain point in the animation
        public void InstantiateFX()
        {
            Debug.Log("instantiating");

            // !! Collision checks are now performed with LAYERS !!
            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
            projectile.gameObject.layer = gameObject.layer;
//            projectile.SetValues(gameObject, velocity, m_damageStat.GetSpecialDamage() + bonusDamage);
            m_cooldownTimer = 0f;
        }
    }
}
