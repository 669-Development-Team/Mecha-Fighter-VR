using Stats;
using UnityEngine;
using RootMotion.FinalIK;

namespace Action
{

    public class UppercutAbility : SpecialAbility
    {

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

            m_animator.SetTrigger("Uppercut");
            m_audioSource.PlayOneShot(activationSfx);
        }
    }
}
