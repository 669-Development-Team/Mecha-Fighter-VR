using UnityEngine;

namespace Action
{
    public abstract class SpecialAbility : MonoBehaviour
    {
        [SerializeField] protected float baseDamage = 10f;
        [SerializeField] protected float energyCost = 100f;
        [SerializeField] protected float cooldown = 1f;

        public abstract void ActivateAbility(GameObject opponent);
    }
}
