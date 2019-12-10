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

        private void OnTriggerEnter(Collider other)
        {
            print("hello");
            if (!other.transform.root.CompareTag("Player"))
            {
                base.Apply(other.transform.root.GetComponent<PlayerStats>());
            }
        }

    }
}
