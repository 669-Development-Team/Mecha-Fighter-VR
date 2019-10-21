using UnityEngine;

namespace Action
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject impactVfxPrefab = null;
        [SerializeField] private float maxLifetime = 10f;

        // These values should be passed in from ProjectileAbility
        private GameObject target = null;
        private float baseDamage = 0f;
        private float velocity = 0f;

        private void Start()
        {
            if (target != null)
            {
                // Rotate y-axis to direction of target
                Vector3 lookDirection = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                transform.LookAt(lookDirection);
            }

            Destroy(gameObject, maxLifetime);
        }

        private void Update()
        {
            // Projectile travels forward
            transform.Translate(Time.deltaTime * velocity * Vector3.forward);
        }

        // This may be removed as I'm sure there's a better way
        public void SetTarget(GameObject opponent)
        {
            target = opponent;
        }

        // This may be removed as I'm sure there's a better way
        public void SetVelocity(float value)
        {
            velocity = value;
        }

        // This may be removed as I'm sure there's a better way
        public void SetBaseDamage(float value)
        {
            baseDamage = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            // TODO: deal damage to other

            if (other != target.GetComponent<Collider>())
            {
                return;
            }

            // Create impact effect and destroy
            GameObject impactVfx = Instantiate(impactVfxPrefab, transform.position, Quaternion.identity);
            Destroy(impactVfx, 1f);
            Destroy(gameObject);
        }
    }
}
