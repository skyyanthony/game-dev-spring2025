using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraGlitchEffect : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;  // Reference to the PostProcessVolume component
    public ChromaticAberration chromaticAberration;  // For controlling Chromatic Aberration
    public LensDistortion lensDistortion;  // For controlling Lens Distortion

    [Header("Glitch Settings")]
    public float glitchIntensityMin = 0.2f;
    public float glitchIntensityMax = 1.0f;
    public float distortionMin = -50f;
    public float distortionMax = 50f;
    public float glitchFrequency = 0.1f;  // Frequency of the glitch effect

   public float glitchTimer = 0f;  // Timer to track glitch trigger events

    void Start()
    {
        // Ensure the PostProcessVolume is assigned and retrieve the settings
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            postProcessVolume.profile.TryGetSettings(out chromaticAberration);
            postProcessVolume.profile.TryGetSettings(out lensDistortion);
        }
        else
        {
            Debug.LogError("PostProcessVolume or its Profile is missing!");
        }
    }

    void Update()
    {
        // Timer to trigger glitch effect randomly
        glitchTimer += Time.deltaTime;

        // Randomly trigger glitch effects based on frequency
        if (glitchTimer >= glitchFrequency)
        {
            glitchTimer = 0f;  // Reset the timer

            // Randomize the glitch intensity and distortion
            if (Random.value > 0.8f)  // Adjust the probability (higher value means more frequent glitches)
            {
                // Randomize the chromatic aberration intensity (color shift)
                if (chromaticAberration != null)
                    chromaticAberration.intensity.value = Random.Range(glitchIntensityMin, glitchIntensityMax);

                // Randomize the lens distortion (image distortion)
                if (lensDistortion != null)
                    lensDistortion.intensity.value = Random.Range(distortionMin, distortionMax);
            }
            else
            {
                // If no glitch occurs, reset to default state
                if (chromaticAberration != null)
                    chromaticAberration.intensity.value = 0f;

                if (lensDistortion != null)
                    lensDistortion.intensity.value = 0f;
            }
        }
    }
}
