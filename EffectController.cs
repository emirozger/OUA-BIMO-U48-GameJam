using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

using DG.Tweening;

public class EffectController : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;

    void Start()
    {

        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        if (vignette == null)
        {
            Debug.LogError("Vignette effect not found");
        }
        vignette.center.value = new Vector2(vignette.center.value.x, 1.3f);
        vignette.intensity.value = 1f;
        ChangeVignetteIntensity();
    }

    public void ChangeVignetteIntensity()
    {
        if (vignette != null)
        {
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 8f);

            DOTween.To(() => vignette.center.value.y, x => vignette.center.value = new Vector2(vignette.center.value.x, x), 0.5f, 4f);

        }
    }
}
