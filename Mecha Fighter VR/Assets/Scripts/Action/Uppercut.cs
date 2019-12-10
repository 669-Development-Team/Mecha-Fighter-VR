using Stats;
using UnityEngine;

namespace Action
{
    public class Uppercut : DamageHitbox
    {

        private Transform hand;

        public override bool Apply(PlayerStats other)
        {
            return base.Apply(other);
        }

        private void Update()
        {
            transform.position = hand.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
            {
                base.Apply(other.transform.root.GetComponent<PlayerStats>());
            }
        }

        public void setFollow(Transform hand)
        {
            this.hand = hand;
        }
    }
}
