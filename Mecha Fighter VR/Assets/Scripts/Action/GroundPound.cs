using Stats;
using UnityEngine;

namespace Action
{
  public class GroundPound : DamageHitbox
    {

        public override bool Apply(PlayerStats other)
        {
            return base.Apply(other);
        }

        private void Update()
        {

        }


    }
}
