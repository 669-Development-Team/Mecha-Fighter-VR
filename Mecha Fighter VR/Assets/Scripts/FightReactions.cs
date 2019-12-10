using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using Stats;
using Action;

// handles the receiving of collisions from other player's abilities
public class FightReactions : MonoBehaviour
{
    public HitReactionVRIK hitReaction;
    [SerializeField] float hitForce = 1f;
    private PlayerStats player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision) {
		
		GameObject other = collision.collider.gameObject;
		HandleCollision(other);
		
		Debug.Log("Collided with " + other);
		
		/*var hit = other.GetComponentInParent(typeof(AbilityHitbox)) as AbilityHitbox;
		
		if (hit != null) {
			hit.Apply(player);
		}*/
		
        /*if (collision.collider.gameObject != gameObject)
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            hitReaction.Hit(gameObject.GetComponent<Collider>(), dir * hitForce, collision.GetContact(0).point);

            m_health.PlaySfx();
        }
        else if (collision.collider.gameObject == gameObject)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider, true);
        }*/
    }
	
	// handles collision from ability
	private void HandleCollision(GameObject other) {
		
		var hit = other.GetComponentInParent(typeof(AbilityHitbox)) as AbilityHitbox;
		
		if (hit != null) {
			Debug.Log(player.InHitStun());
			Debug.Log("Handling collision from " + other.name);
			if (hit.Apply(player)) {
				ProjectileHit();
			}
		}
	}
	
	void OnParticleCollision(GameObject other) {
        print(other);
        GameObject o = findParent(gameObject);    
        print(o.GetComponent<PlayerStats>());
        other.GetComponent<GroundPound>().Apply(o.GetComponent<PlayerStats>());
	}
	
    public void ProjectileHit()
    {
        Animator animator = GetComponent<Animator>();
      
            animator.SetTrigger("HitH");
        
    }
    private GameObject findParent(GameObject o){
        Transform parent = o.transform;
        
        while(parent.parent != null){
            parent = parent.parent;
        }
        return parent.gameObject;
    }
}
