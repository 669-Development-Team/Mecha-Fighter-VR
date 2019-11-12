using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Stats;

public class FightReactions : MonoBehaviour
{
    public HitReactionVRIK hitReaction;
    [SerializeField] float hitForce = 60f;

    private Health m_health;

    // Start is called before the first frame update
    void Awake()
    {
        m_health = GetComponent<Health>();
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
            hitReaction.Hit(gameObject.GetComponent<Collider>(), dir * hitForce, collision.GetContact(0).point);

            m_health.PlaySfx();
        }
        else if (collision.collider.gameObject == gameObject)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider, true);
        }
    }
    public void ProjectileHit()
    {
        Animator animator = GetComponent<Animator>();
      
            animator.SetTrigger("HitH");
        
    }
}
