using UnityEngine;

namespace Action
{
    public class UppercutAbility : SpecialAbility
    {
        public override void ActivateAbility(GameObject opponent)
        {
            Debug.Log("Uppercut gesture performed!");
        }
    }
}
