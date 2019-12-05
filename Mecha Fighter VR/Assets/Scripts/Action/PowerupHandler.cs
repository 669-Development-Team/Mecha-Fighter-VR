using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Powerup") {
            Powerup powerup = other.GetComponent<Powerup>();
            powerup.Apply(stats);
            Debug.Log("Deleting " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
