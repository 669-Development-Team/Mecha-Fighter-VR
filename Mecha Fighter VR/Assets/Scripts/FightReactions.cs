using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class FightReactions : MonoBehaviour
{
    public HitReactionVRIK hitReaction;
    [SerializeField] float hitForce = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject != gameObject)
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            hitReaction.Hit(collision.collider, dir * hitForce, collision.GetContact(0).point);
        }
        else if (collision.collider.gameObject == gameObject)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider, true);
        }
    }
    public void ProjectileHit()
    {
        gameObject.GetComponent<Animator>().SetTrigger("HitH");
    }
}
