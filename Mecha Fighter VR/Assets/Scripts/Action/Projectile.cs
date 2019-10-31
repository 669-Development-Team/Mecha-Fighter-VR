using Stats;
using UnityEngine;

namespace Action
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject impactVfxPrefab = null;
        [SerializeField] private float maxLifetime = 10f;

        // These values should be passed in
        private Health m_target = null;
        private GameObject m_instigator = null;
        private float m_baseDamage = 0f;
        private float m_velocity = 0f;

        private void Start()
        {
            if (m_target != null)
            {
                // Rotate y-axis to direction of target
                Vector3 lookDirection = new Vector3(m_target.transform.position.x, transform.position.y, m_target.transform.position.z);
                transform.LookAt(lookDirection);
            }

            Destroy(gameObject, maxLifetime);
        }

        private void Update()
        {
            // Projectile travels forward
            transform.Translate(Time.deltaTime * m_velocity * Vector3.forward);
        }

        // This may be removed as I'm sure there's a better way
        public void SetTarget(Health target, GameObject instigator, float velocity, float damage)
        {
            m_target = target;
            m_instigator = instigator;
            m_velocity = velocity;
            m_baseDamage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            // TODO: deal damage to other

            if (other == m_instigator.GetComponent<Collider>())
            {
                return;
            }

            other.GetComponent<IDamageable>()?.TakeDamage(m_baseDamage);

            // Create impact effect and destroy
            GameObject impactVfx = Instantiate(impactVfxPrefab, transform.position, Quaternion.identity);
            Destroy(impactVfx, 1.5f);
            Destroy(gameObject);
        }
    }
}
