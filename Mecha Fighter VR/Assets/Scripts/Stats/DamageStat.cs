using UnityEngine;

namespace Stats
{
    public class DamageStat : MonoBehaviour
    {
        [SerializeField] private float baseAttackDamage = 6f;
        [SerializeField] private float baseSpecialDamage = 10f;

        // TODO: add buffs like flat/percentage modifiers

        public float GetAttackDamage()
        {
            return baseAttackDamage;
        }

        public float GetSpecialDamage()
        {
            return baseSpecialDamage;
        }
    }
}
