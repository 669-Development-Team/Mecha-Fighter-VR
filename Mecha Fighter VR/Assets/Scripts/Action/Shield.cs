using System.Collections;
using Stats;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private float defense = 5f;
    [SerializeField] private UnityEvent onShieldDamage = null;
    [SerializeField] private UnityEvent onShieldBreak = null;
    [SerializeField] private UnityEvent onShieldReady = null;

    private float maxDurability;
    private float currentDurability;
    private bool isBroken = false;
    private float shieldRecoveryTime;

    private void Start()
    {
        // Shield is inactive at start
        ToggleShield(false);
        currentDurability = maxDurability;
    }

    private void OnTriggerEnter(Collider o) {
        Debug.Log(o);
		GameObject other = o.gameObject;
		var hit = other.GetComponentInParent(typeof(AbilityHitbox)) as AbilityHitbox;
		
		if (hit != null) {
			Debug.Log("Shield collision with " + other.name);
            hit.ApplyShield(this);
		}
    }

    public float GetDurabilityPercentage()
    {
        return 100f * (currentDurability / maxDurability);
    }

    public void SetMaxDurability(float durability)
    {
        maxDurability = durability;
    }

    public void SetShieldRecoveryTime(float time)
    {
        shieldRecoveryTime = time;
    }

    public void TakeDamage(float damageDealt)
    {
        Debug.Log("Shield took damage");
        currentDurability -= Mathf.Max(1, damageDealt - defense);
        if (!isBroken && currentDurability <= 0f)
        {
            StartCoroutine(BreakShield());
        }
        else
        {
            onShieldDamage.Invoke();
        }
    }

    public void ToggleShield(bool triggered)
    {
        if (isBroken && triggered)
        {
            return;
        }

        GetComponent<Collider>().enabled = triggered;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(triggered);
        }
    }

    public void RegenerateDurability(float regenerationRate)
    {
        if (!isBroken)
        {
            currentDurability = Mathf.Min(maxDurability, currentDurability + regenerationRate * Time.deltaTime);
        }
    }

    private IEnumerator BreakShield()
    {
        isBroken = true;
        ToggleShield(false);
        onShieldBreak.Invoke();
        yield return new WaitForSeconds(shieldRecoveryTime);
        currentDurability += 1f;
        isBroken = false;
        onShieldReady.Invoke();
    }
}
