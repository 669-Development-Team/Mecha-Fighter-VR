using Stats;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PointsDisplay : MonoBehaviour
    {

        public TextMeshProUGUI pointsText;

        void Update()
        {
            /*
             * The question marks "?" is a shorthand way of checking each component for null before calling a function on them
             */
            pointsText?.SetText($"{FindObjectOfType<GameStateManager>().getPlayerPoints(true):0}");
        }


    }
}
