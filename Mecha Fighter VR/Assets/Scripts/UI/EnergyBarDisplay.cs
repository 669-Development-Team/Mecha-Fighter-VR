using Stats;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EnergyBarDisplay : MonoBehaviour
    {
        /*
         * Energy is not shown to the opponent from the mech's head, therefore we only need to attach one of these scripts to the player root to update the energy bar from the HUD only
         */
//        [TextArea]
//        [Tooltip("Use two of these scripts on the player root to update the energy bar and percentage from both the HUD and the mech, one script for each")]
//        public string notes = "Energy bar from the _ (HUD or mech?)";

        // The energy displayed on the HUD or the Mech
        public TextMeshProUGUI energyPercentageText;
        public RectTransform energyBar;

        [SerializeField] private PlayerStats energy = null;

        void Update()
        {
            /*
             * The question marks "?" is a shorthand way of checking each component for null before calling a function on them
             */
            energyPercentageText?.SetText($"{energy.GetEnergyPercentage():0}%");
            energyBar?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, energy.GetEnergyPercentage());
        }
    }
}
