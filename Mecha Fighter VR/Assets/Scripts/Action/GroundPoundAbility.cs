using Stats;
using UnityEngine;

namespace Action
{
    [RequireComponent(typeof(DamageStat))]
    public class GroundPoundAbility : MonoBehaviour
    {
        [SerializeField] private float bonusDamage = 10f;
        [SerializeField] private float energyCost = 100f;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private AudioClip activationSfx;
        [SerializeField] private GroundPound groundPoundPrefab = null;
        [SerializeField] private Transform effectSpawnPoint = null;

        // Time since the projectile was last fired
        private float m_cooldownTimer = Mathf.Infinity;
        private Health m_opponent = null;

        private Animator m_animator;
        private AudioSource m_audioSource;
        private DamageStat m_damageStat;
        private Energy m_energy;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_audioSource = GetComponent<AudioSource>();
            m_damageStat = GetComponent<DamageStat>();
            m_energy = GetComponent<Energy>();
        }

        private void Update()
        {
            m_cooldownTimer += Time.deltaTime;
        }

        public void ActivateAbility(Health opponent)
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

            Debug.Log("Ground Pound gesture performed!");
            m_opponent = opponent;
            m_animator.SetTrigger("GroundPound");
        }

        // Animation event
        public void InstantiateGroundPoundFx()
        {
            GroundPound groundPound = Instantiate(groundPoundPrefab, effectSpawnPoint.position, Quaternion.identity);
            groundPound.SetTarget(m_opponent, gameObject, m_damageStat.GetSpecialDamage() + bonusDamage);
            m_cooldownTimer = 0f;
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
