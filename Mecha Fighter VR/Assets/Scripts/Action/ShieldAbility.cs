using UnityEngine;

namespace Action
{
    public class ShieldAbility : MonoBehaviour
    {
        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;

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
