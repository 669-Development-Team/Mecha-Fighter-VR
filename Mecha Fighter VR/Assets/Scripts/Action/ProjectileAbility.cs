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
        private bool m_inAnimation = false;
        private AnimationListener m_projectileListener;

        private Animator m_animator;
        private DamageStat m_damageStat;
        private Energy m_energy;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_damageStat = GetComponent<DamageStat>();
            m_energy = GetComponent<Energy>();

            m_projectileListener = new AnimationListener(this, "Projectile", null, OnAnimationExit);
        }

        private void Update()
        {
            m_cooldownTimer += Time.deltaTime;
        }

        public void ActivateAbility()
        {
            if (m_inAnimation)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            m_inAnimation = true;

            m_animator.SetTrigger("Projectile");
			Debug.Log("Projectile gesture performed!");
        }

        private void OnAnimationExit() { m_inAnimation = false;  }

        public void InstantiateProjectileFx()
        {
			Debug.Log("instantiating");
			
            // !! Collision checks are now performed with LAYERS !!
            // !! May need to reconfigure layers when multiplayer is introduced !!
            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);

            if (gameObject.layer == 12)
            {
                projectile.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            }
            else if (gameObject.layer == 13)
            {
                projectile.gameObject.layer = LayerMask.NameToLayer("OpponentProjectile");
            }
			
            projectile.SetValues(gameObject, velocity, m_damageStat.GetSpecialDamage() + bonusDamage);
            m_cooldownTimer = 0f;
        }
    }
}
