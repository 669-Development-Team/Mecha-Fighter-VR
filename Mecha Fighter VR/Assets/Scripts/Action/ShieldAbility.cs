using UnityEngine;

namespace Action
{
    public class ShieldAbility : MonoBehaviour
    {
        [SerializeField] private Shield leftShield;
        [SerializeField] private Shield rightShield;

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
