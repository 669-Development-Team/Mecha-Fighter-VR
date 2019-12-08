using UnityEngine;
using UnityEngine.Events;

namespace Action
{
    public class ShieldAbility : MonoBehaviour
    {
        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;
        [SerializeField] private float maxDurability = 200f;
        [SerializeField] private float regenerationRate = 4f;
        [SerializeField] private float shieldRecoveryTime = 5f;
        [SerializeField] private UnityEvent onActivate = null;
        [SerializeField] private UnityEvent onDeactivate = null;

        void Awake()
        {
            // Awake is called before Start
            leftShield.SetMaxDurability(maxDurability);
            rightShield.SetMaxDurability(maxDurability);
            leftShield.SetShieldRecoveryTime(shieldRecoveryTime);
            rightShield.SetShieldRecoveryTime(shieldRecoveryTime);
        }

        void Update()
        {
            leftShield.RegenerateDurability(regenerationRate);
            rightShield.RegenerateDurability(regenerationRate);
        }

        public void ToggleLeftShield(bool shieldTriggered)
        {
            leftShield.ToggleShield(shieldTriggered);

            if (shieldTriggered)
            {
                onActivate.Invoke();
            }
            else
            {
                onDeactivate.Invoke();
            }
        }

        public void ToggleRightShield(bool shieldTriggered)
        {
            rightShield.ToggleShield(shieldTriggered);

            if (shieldTriggered)
            {
                onActivate.Invoke();
            }
            else
            {
                onDeactivate.Invoke();
            }
        }
    }
}
