using UnityEngine;

namespace Action
{
    public class ShieldAbility : MonoBehaviour
    {
        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;
        [SerializeField] private float maxDurability = 200f;
        [SerializeField] private float regenerationRate = 4f;

        void Awake()
        {
            // Awake is called before Start
            leftShield.SetMaxDurability(maxDurability);
            rightShield.SetMaxDurability(maxDurability);
        }

        void Update()
        {
            leftShield.RegenerateDurability(regenerationRate);
            rightShield.RegenerateDurability(regenerationRate);
        }

        public void ToggleLeftShield(bool shieldTriggered)
        {
            leftShield.ToggleShield(shieldTriggered);
        }

        public void ToggleRightShield(bool shieldTriggered)
        {
            rightShield.ToggleShield(shieldTriggered);
        }
    }
}
