using Stats;
using UnityEngine;
using RootMotion.FinalIK;

namespace Action
{
    [RequireComponent(typeof(DamageStat))]
    public class UppercutAbility : MonoBehaviour
    {
        [SerializeField] private float bonusDamage = 10f;
        [SerializeField] private float energyCost = 100f;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private AudioClip activationSfx;

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
            Debug.Log("Uppercut gesture performed!");

            if (m_cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            m_animator.SetTrigger("Uppercut");
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
