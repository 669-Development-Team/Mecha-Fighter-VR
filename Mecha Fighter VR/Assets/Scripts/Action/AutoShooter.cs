using UnityEngine;

namespace Action
{
    /// <summary>
    /// Used by placeholder opponent for testing
    /// </summary>
    public class AutoShooter : MonoBehaviour
    {
        [SerializeField] private Projectile bulletPrefab = null;
        [SerializeField] private Transform shootPoint = null;

        void Start()
        {
            InvokeRepeating(nameof(ShootBullet), 1f, 1f);
        }

        private void ShootBullet()
        {
            Projectile bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.SetTarget(null, gameObject, 10f, 20f);
        }
    }
}
