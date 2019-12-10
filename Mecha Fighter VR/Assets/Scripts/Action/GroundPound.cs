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

        public override bool ApplyShield(Shield other)
        {
            Debug.Log("Shield collide with ground pound");
            return base.ApplyShield(other);
        }

        private void Update()
        {

        }


    }
}
