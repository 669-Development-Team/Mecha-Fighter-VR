using Stats;
using UnityEngine;

namespace Action
{
    [RequireComponent(typeof(DamageStat))]
    public abstract class SpecialAbility : MonoBehaviour
    {
        [SerializeField] protected float bonusDamage = 10f;
        [SerializeField] protected float energyCost = 100f;
        [SerializeField] protected float cooldown = 1f;
        [SerializeField] protected AudioClip activationSfx;

        protected Animator m_animator;
        protected AudioSource m_audioSource;
        protected DamageStat m_damageStat;
        protected Energy m_energy;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_audioSource = GetComponent<AudioSource>();
            m_damageStat = GetComponent<DamageStat>();
            m_energy = GetComponent<Energy>();
        }

        // Time since the projectile was last fired
        protected float cooldownTimer = Mathf.Infinity;

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        public abstract void ActivateAbility(Health opponent);
    }
}
