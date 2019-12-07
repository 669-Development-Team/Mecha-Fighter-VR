using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class CameraEffects : MonoBehaviour
{
    [SerializeField] private float effectSpeed = 5f;
    [SerializeField] private float restoreSpeed = 2f;
    [SerializeField] private float delayInSeconds = 1f;
    [SerializeField, Range(0f, 1f)] private float vignetteHitIntensity = 0.5f;
    [SerializeField, Range(0, 5)] private int chromaticAberrationHitIntensity = 3;
    [SerializeField, Range(-100, 100)] private int saturationHitIntensity = -60;

    private PostProcessVolume volume = null;
    private Vignette vignetteLayer = null;
    private ChromaticAberration chromaticAberrationLayer = null;
    private ColorGrading colorGradingLayer = null;
    private float originalVignetteIntensity;
    private float originalSaturationIntensity;
    private float originalChromaticAberrationIntensity;

    private bool isEffectStarted = false;
    private float timer = 0f;

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
        volume.profile.TryGetSettings(out chromaticAberrationLayer);
        volume.profile.TryGetSettings(out colorGradingLayer);
        originalVignetteIntensity = vignetteLayer.intensity.value;
        originalChromaticAberrationIntensity = chromaticAberrationLayer.intensity.value;
        originalSaturationIntensity = colorGradingLayer.saturation.value;
    }

    private void Update()
    {
        if (isEffectStarted && timer < 1f)
        {
            // Increase intensity of vignette, chromatic aberration, and saturation
            vignetteLayer.intensity.value = Mathf.Lerp(originalVignetteIntensity, vignetteHitIntensity, timer);
            chromaticAberrationLayer.intensity.value = Mathf.Lerp(originalChromaticAberrationIntensity, chromaticAberrationHitIntensity, timer);
            colorGradingLayer.saturation.value = Mathf.Lerp(originalSaturationIntensity, saturationHitIntensity, timer);
            timer += Time.deltaTime * effectSpeed;
        }
        else if (!isEffectStarted && timer < 1f)
        {
            // Return effects back to normal
            if (vignetteLayer.intensity.value > originalVignetteIntensity)
            {
                vignetteLayer.intensity.value = Mathf.Lerp(vignetteHitIntensity, originalVignetteIntensity, timer);
            }

            if (chromaticAberrationLayer.intensity.value > originalChromaticAberrationIntensity)
            {
                chromaticAberrationLayer.intensity.value = Mathf.Lerp(chromaticAberrationHitIntensity, originalChromaticAberrationIntensity, timer);
            }

            if (colorGradingLayer.saturation.value < originalSaturationIntensity)
            {
                colorGradingLayer.saturation.value = Mathf.Lerp(saturationHitIntensity, originalSaturationIntensity, timer);
            }
            timer += Time.deltaTime * restoreSpeed;
        }
    }

    public void TakeDamageEffect()
    {
        StartCoroutine(EffectSequence());
    }

    private IEnumerator EffectSequence()
    {
        timer = 0f;
        isEffectStarted = true;
        // Wait for seconds before returning to normal
        yield return new WaitForSeconds(delayInSeconds);
        timer = 0f;
        isEffectStarted = false;
    }
}
