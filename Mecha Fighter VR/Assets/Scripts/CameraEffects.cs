using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class CameraEffects : MonoBehaviour
{
    [SerializeField] private float vignetteHitIntensity = 0.6f;

    private PostProcessVolume volume = null;
    private Vignette vignetteLayer = null;
    private float originalVignetteIntensity;

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
        originalVignetteIntensity = vignetteLayer.intensity.value;
    }

    public void TakeDamageEffect()
    {
        StartCoroutine(RestoreVignette());
    }

    private IEnumerator RestoreVignette()
    {
        vignetteLayer.intensity.value = Mathf.Lerp(originalVignetteIntensity, vignetteHitIntensity, 0.3f);
        yield return new WaitForSeconds(0.3f);
        vignetteLayer.intensity.value = Mathf.Lerp(vignetteLayer.intensity.value, originalVignetteIntensity, 1f);
    }
}
