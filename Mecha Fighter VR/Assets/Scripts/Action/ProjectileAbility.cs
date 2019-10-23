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
        [SerializeField] private float motionDistance = 0.2f;
        [SerializeField] private float motionTimeThreshold = 0.5f;

        // Time since the projectile was last fired
        private float cooldownTimer = Mathf.Infinity;
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
        public void StartMotionTime()
        {
            motionStartTime = Time.time;
        }

        // Check if the time from releasing both grips since the start of the motion is within the threshold
        public bool IsWithinMotionTimeThreshold()
        {
            return Time.time - motionStartTime < motionTimeThreshold;
        }

        /// <summary>
        /// Detect if the motion covers enough distance and is in the direction of the target.
        /// </summary>
        /// <param name="leftStartPos">The initial position of the left controller when the grip is first pressed</param>
        /// <param name="leftEndPos">The final position of the left controller when the grip is released</param>
        /// <param name="rightStartPos">The initial position of the right controller when the grip is first pressed</param>
        /// <param name="rightEndPos">The final position of the right controller when the grip is released</param>
        /// <param name="targetPos">The target position for the motion to be acting towards, like the opponent's position</param>
        /// <returns></returns>
        public bool IsForwardMotion(Vector3 leftStartPos, Vector3 leftEndPos, Vector3 rightStartPos, Vector3 rightEndPos, Vector3 targetPos)
        {
            bool checkDistance = Vector3.Distance(leftStartPos, leftEndPos) > motionDistance &&
                                 Vector3.Distance(rightStartPos, rightEndPos) > motionDistance;

            bool checkTowardsTarget = Vector3.Distance(leftEndPos, targetPos) < Vector3.Distance(leftStartPos, targetPos) &&
                                      Vector3.Distance(rightEndPos, targetPos) < Vector3.Distance(rightStartPos, targetPos);

            // TODO: check if the motion is within a narrow range (ie. more straight and parallel as opposed to a big hug)

            return checkDistance && checkTowardsTarget;
        }
    }
}
