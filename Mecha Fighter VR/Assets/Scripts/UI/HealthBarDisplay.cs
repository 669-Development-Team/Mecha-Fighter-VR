using Stats;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthBarDisplay : MonoBehaviour
    {
        [TextArea]
        [Tooltip("Use two of these scripts on the player root to update the health bar and percentage from both the HUD and the mech, one script for each")]
        public string notes = "Health bar from the _ (HUD or mech?)";

        // The health displayed on the HUD or the Mech
        public TextMeshProUGUI healthPercentageText;
        public RectTransform healthBar;

        [SerializeField] private Health health = null;

        void Update()
        {
            /*
             * The question marks "?" is a shorthand way of checking each component for null before calling a function on them
             */
            healthPercentageText?.SetText($"{health.GetHealthPercentage():0}%");
            healthBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health.GetHealthPercentage());
        }
    }
}
