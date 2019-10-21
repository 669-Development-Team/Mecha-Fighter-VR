using UnityEngine;

namespace Action
{
    public class ProjectileAbility : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab = null;
        [SerializeField] private Transform shootPoint = null;
        [SerializeField] private float baseDamage = 10f;
        [SerializeField] private float velocity = 8f;
        [SerializeField] private float cooldown = 1f;
        [SerializeField] private float motionTotalTime = 0.5f;

        // Time since the projectile was last fired
        private float cooldownTimer = 2f;
        private float motionStartTime = 0f;

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        public void ShootProjectile(GameObject opponent)
        {
            if (cooldownTimer < cooldown)
            {
                return;
            }

            Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.SetTarget(opponent);
            projectile.SetBaseDamage(baseDamage);
            projectile.SetVelocity(velocity);
            cooldownTimer = 0f;
        }

        // Record the time at the start of having both grips held down
        public void StartMotion()
        {
            motionStartTime = Time.time;
        }

        // Check if the time from releasing both grips since the start of the motion is within the threshold
        public bool MotionTimeSuccess()
        {
            return Time.time - motionStartTime < motionTotalTime;
        }
    }
}
