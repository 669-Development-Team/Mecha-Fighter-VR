
using UnityEngine;

namespace Action
{
    public class ShieldSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private AudioClip[] shieldDamageSfx;

        public void PlaySfx()
        {
            audioSource.PlayOneShot(shieldDamageSfx[Random.Range(0, shieldDamageSfx.Length - 1)], 1f);
            audioSource.Play();
        }
    }
}
