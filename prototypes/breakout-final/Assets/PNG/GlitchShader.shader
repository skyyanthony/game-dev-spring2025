using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlitchEffect : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    
    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out lensDistortion);
    }

    void Update()
    {
        if (Random.value > 0.9f)  // Random glitch trigger
        {
            chromaticAberration.intensity.value = Random.Range(0.2f, 1.0f);
            lensDistortion.intensity.value = Random.Range(-50f, 50f);
        }
        else
        {
            chromaticAberration.intensity.value = 0;
            lensDistortion.intensity.value = 0;
        }
    }
}
