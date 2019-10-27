using UnityEngine;

namespace Action
{
    public class GroundPound : MonoBehaviour
    {
        [SerializeField] private float maxLifetime = 4f;

        private GameObject target = null;

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

        // This may be removed as I'm sure there's a better way
        public void SetTarget(GameObject opponent)
        {
            target = opponent;
        }
    }
}
