using Stats;
using UnityEngine;

namespace Action
{
    public class GroundPound : MonoBehaviour
    {
        [SerializeField] private float maxLifetime = 4f;

        private Health m_target = null;
        private GameObject m_instigator = null;
        private float m_baseDamage = 0f;

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

        public void SetTarget(Health opponent, GameObject instigator, float damage)
        {
            m_target = opponent;
            m_instigator = instigator;
            m_baseDamage = damage;
        }
    }
}
