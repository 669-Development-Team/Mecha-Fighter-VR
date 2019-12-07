using Stats;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private float defense = 5f;

    private float maxDurability;
    private float currentDurability;

    private void Start()
    {
        // Shield is inactive at start
        ToggleShield(false);
        currentDurability = maxDurability;
    }

    public float GetDurabilityPercentage()
    {
        return 100f * (currentDurability / maxDurability);
    }

    public void SetMaxDurability(float durability)
    {
        maxDurability = durability;
    }

    public void TakeDamage(float damageDealt)
    {
        currentDurability -= Mathf.Max(1, damageDealt - defense);
    }

    public void ToggleShield(bool triggered)
    {
        GetComponent<Collider>().enabled = triggered;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(triggered);
        }
    }

    public void RegenerateDurability(float regenerationRate)
    {
        currentDurability = Mathf.Min(maxDurability, currentDurability + regenerationRate * Time.deltaTime);
    }
}
