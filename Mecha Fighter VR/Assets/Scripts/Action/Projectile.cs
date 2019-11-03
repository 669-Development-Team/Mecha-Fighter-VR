using Stats;
using UnityEngine;

namespace Action
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject impactVfxPrefab = null;
        [SerializeField] private float maxLifetime = 10f;

        // These values should be passed in
        private GameObject m_instigator = null;
        private float m_baseDamage = 0f;
        private float m_velocity = 0f;

        private Collider m_collider = null;

        private void Awake()
        {
            m_collider = GetComponent<Collider>();
        }

        private void Start()
        {
            // Projectiles eventually die if they do not collide with anything
            Destroy(gameObject, maxLifetime);
        }

        private void Update()
        {
            // Projectile travels forward
            transform.Translate(Time.deltaTime * m_velocity * Vector3.forward);
        }

        // This may be removed as I'm sure there's a better way
        public void SetValues(GameObject instigator, float velocity, float damage)
        {
            m_instigator = instigator;
            m_velocity = velocity;
            m_baseDamage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            // !! Collision checks are now performed with LAYERS !!
            // !! May need to reconfigure layers when multiplayer is introduced !!

            // Damages Health OR Shield
            other.GetComponent<IDamageable>()?.TakeDamage(m_baseDamage);

            // Create impact effect and destroy
            Instantiate(impactVfxPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
