using UnityEngine;

namespace Action
{
    public abstract class SpecialAbility : MonoBehaviour
    {
        [SerializeField] protected float baseDamage = 10f;
        [SerializeField] protected float energyCost = 100f;
        [SerializeField] protected float cooldown = 1f;

        // Time since the projectile was last fired
        protected float cooldownTimer = Mathf.Infinity;

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        public abstract void ActivateAbility(GameObject opponent);
    }
}
