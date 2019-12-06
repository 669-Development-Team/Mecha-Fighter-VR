using UnityEngine;
using UnityEngine.Events;

namespace Stats
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 500f;
        [SerializeField] private float defense = 0f;
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private AudioClip[] hitImpactSfx;
        [SerializeField] private UnityEvent onTakeDamage = null;

        private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        // Percentage of current / max, used for HUD
        public float GetHealthPercentage()
        {
            return 100f * (currentHealth / maxHealth);
        }

        public void TakeDamage(float damageDealt)
        {
            currentHealth -= Mathf.Max(1, damageDealt - defense);
            PlaySfx();

            if (currentHealth <= 0f)
            {
                FindObjectOfType<GameStateManager>().GameOver(transform.root.gameObject.name);
            }

            onTakeDamage.Invoke();
        }

        public void Heal(float healthToRestore)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth = Mathf.Min(currentHealth + healthToRestore, maxHealth);
            }
        }

        public void PlaySfx()
        {
            audioSource.PlayOneShot(hitImpactSfx[Random.Range(0, hitImpactSfx.Length - 1)]);
            audioSource.Play();
        }
    }
}
