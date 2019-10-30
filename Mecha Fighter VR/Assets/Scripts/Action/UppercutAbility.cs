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
            Debug.Log("Uppercut gesture performed!");

            if (cooldownTimer < cooldown)
            {
                return;
            }

            // Check sufficient energy
            if (!m_energy.Deplete(energyCost))
            {
                return;
            }

            m_animator.SetBool("Uppercut", true);
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
