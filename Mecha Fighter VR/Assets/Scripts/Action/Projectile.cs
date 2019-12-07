using Stats;
using UnityEngine;

namespace Action
{
    public class Projectile : DamageHitbox
    {
		[SerializeField] private float velocity = 0f;
        [SerializeField] private GameObject impactVfxPrefab = null;
		
		public override bool Apply(PlayerStats other) {
			
			Instantiate(impactVfxPrefab, transform.position, transform.rotation);
			Destroy(gameObject);
			
			return base.Apply(other);
		}

        private void Update()
        {
            transform.Translate(Time.deltaTime * velocity * Vector3.forward);
        }
    }
}
