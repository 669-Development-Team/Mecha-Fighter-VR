using Stats;
using UnityEngine;

namespace Action
{
    public class UppercutAbility : SpecialAbility
    {
        public override void ActivateAbility(Health opponent)
        {
            Debug.Log("Uppercut gesture performed!");
        }
    }
}
