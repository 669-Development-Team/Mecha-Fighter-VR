using Stats;
using UnityEngine;
using RootMotion.FinalIK;

namespace Action
{

    public class UppercutAbility : SpecialAbility
    {
        public VRIK ik;
        public override void ActivateAbility(Health opponent)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("Uppercut", true);

            Debug.Log("Uppercut gesture performed!");
        }
    }
}
