using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Valve.VR;

// This script is attached to the post-process volume object
[RequireComponent(typeof(PostProcessVolume))]
public class CameraEffects : MonoBehaviour
{
    // Effect sequence parameters
    [SerializeField] private float effectSpeed = 5f;
    [SerializeField] private float restoreSpeed = 2f;
    [SerializeField] private float delayInSeconds = 1f;
    [SerializeField] private float vibrationDuration = 0.5f;

    // Post-processing effects
    [SerializeField, Range(0f, 1f)] private float vignetteHitIntensity = 0.5f;
    [SerializeField, Range(0, 5)] private int chromaticAberrationHitIntensity = 3;
    [SerializeField, Range(-100, 100)] private int saturationHitIntensity = -60;
    [SerializeField, Range(0.1f, 20f)] private float depthOfFieldHitDistance = 4f;

    [Tooltip("VFX prefab that will spawn in front of the camera")]
    [SerializeField] private GameObject impactEffectVfx = null;

    [SerializeField] private SteamVR_Action_Vibration haptics;

    private SteamVR_Input_Sources controller;

    // Post-processing volume and layers
    private PostProcessVolume volume = null;
    private Vignette vignetteLayer = null;
    private ChromaticAberration chromaticAberrationLayer = null;
    private ColorGrading colorGradingLayer = null;
    private DepthOfField depthOfFieldLayer = null;

    // Original post-processing values
    private float originalVignetteIntensity;
    private float originalChromaticAberrationIntensity;
    private float originalSaturationIntensity;
    private float originalDepthOfFieldDistance;

    // Internal properties
    private bool isEffectStarted = false;
    private float timer = 0f;

    private void Awake()
    {
        // Initialize
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
        volume.profile.TryGetSettings(out chromaticAberrationLayer);
        volume.profile.TryGetSettings(out colorGradingLayer);
        volume.profile.TryGetSettings(out depthOfFieldLayer);

        // Cache original values
        originalVignetteIntensity = vignetteLayer.intensity.value;
        originalChromaticAberrationIntensity = chromaticAberrationLayer.intensity.value;
        originalSaturationIntensity = colorGradingLayer.saturation.value;
        originalDepthOfFieldDistance = depthOfFieldLayer.focusDistance.value;
    }

    private void Start()
    {
        controller = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
    }

    private void Update()
    {
        if (isEffectStarted)
        {
            timer = Mathf.Min(timer + Time.deltaTime * effectSpeed, 1f);
            depthOfFieldLayer.enabled.value = true;
        }
        else
        {
            timer = Mathf.Max(timer - Time.deltaTime * restoreSpeed, 0f);
            depthOfFieldLayer.enabled.value = false;
        }

        // Increase or decrease intensity of vignette, chromatic aberration, saturation, and depth of field
        vignetteLayer.intensity.value = Mathf.Lerp(originalVignetteIntensity, vignetteHitIntensity, timer);
        chromaticAberrationLayer.intensity.value = Mathf.Lerp(originalChromaticAberrationIntensity, chromaticAberrationHitIntensity, timer);
        colorGradingLayer.saturation.value = Mathf.Lerp(originalSaturationIntensity, saturationHitIntensity, timer);
        depthOfFieldLayer.focusDistance.value = Mathf.Lerp(originalDepthOfFieldDistance, depthOfFieldHitDistance, timer);
    }

    // Called from a Unity Event when the player takes damage
    public void TakeDamageEffect(Camera cameraPos)
    {
        StartCoroutine(EffectSequence());
        Instantiate(impactEffectVfx, cameraPos.transform.position, cameraPos.transform.rotation);
        haptics.Execute(0, vibrationDuration, 100f, 0.6f, controller);
    }

    private IEnumerator EffectSequence()
    {
        timer = 0f;
        isEffectStarted = true;
        // Wait for seconds before returning to normal
        yield return new WaitForSeconds(delayInSeconds);
        isEffectStarted = false;
    }
}
