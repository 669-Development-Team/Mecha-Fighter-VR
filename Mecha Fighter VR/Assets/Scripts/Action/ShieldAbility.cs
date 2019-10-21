using UnityEngine;

namespace Action
{
    public class ShieldAbility : MonoBehaviour
    {
        [SerializeField] private Shield leftShield = null;
        [SerializeField] private Shield rightShield = null;

        public void ToggleLeftShield(bool shieldTriggered)
        {
            leftShield.gameObject.SetActive(shieldTriggered);
        }

        public void ToggleRightShield(bool shieldTriggered)
        {
            rightShield.gameObject.SetActive(shieldTriggered);
        }
    }
}
