using System.Collections;
using UnityEngine;

namespace Stats
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private float healthToRestore = 0f;
        [SerializeField] private float energyToRestore = 0f;
        [SerializeField] private float respawnTime = 10f;

        private Animation animation = null;

        private void Awake()
        {
            animation = GetComponent<Animation>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Player picks up the pickup
            if (other.transform.root.CompareTag("Player"))
            {
                GrantPickup(other.transform.root.gameObject);
            }
        }

        private void GrantPickup(GameObject subject)
        {
            if (healthToRestore > 0f)
            {
                subject.GetComponent<PlayerStats>().Heal(healthToRestore);
            }

            if (energyToRestore > 0f)
            {
                subject.GetComponent<PlayerStats>().Replenish(energyToRestore);
            }

            // Weapon pickup disappears for some time and respawns after
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
            animation.Play();
        }

        private void ShowPickup(bool shouldShow)
        {
            // Disable collider
            GetComponent<Collider>().enabled = shouldShow;
            // Disable all children
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}
