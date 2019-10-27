using UnityEngine;

namespace Action
{
    public class GroundPoundAbility : SpecialAbility
    {
        public override void ActivateAbility(GameObject opponent)
        {
            Debug.Log("Ground Pound gesture performed!");
        }
    }
}
