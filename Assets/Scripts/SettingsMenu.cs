using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsMenu : MonoBehaviour
{
    public UnityEngine.UI.Slider BrightnessSlider;
    public UnityEngine.UI.Slider SensitivitySlider;
    public Volume Volume;
    VolumeProfile Profile;

    public UnityEngine.UI.Toggle ChromaticToggle;
    public UnityEngine.UI.Toggle GrainToggle;
    public UnityEngine.UI.Toggle MotionBlurToggle;

    void Start()
    {
        Profile = Volume.sharedProfile;

        BrightnessSlider.value = GameManager.Instance.Brightness;
        SensitivitySlider.value = GameManager.Instance.MouseSensitivity;

        ChromaticToggle.isOn = GameManager.Instance.ChromaticAberrationActivity;
        GrainToggle.isOn = GameManager.Instance.FilmGrainActivity;
        MotionBlurToggle.isOn = GameManager.Instance.MotionBlurActivity;


    }

    void Update()
    {

    }

    public void UpdateBrightness()
    {
        #region Brightness

        GameManager.Instance.Brightness = BrightnessSlider.value;

        if (!Profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj = Profile.Add<ColorAdjustments>(false);
        }

        colorAdj.postExposure.value = GameManager.Instance.Brightness;
        #endregion
    }

    public void ResetBrightness()
    {
        BrightnessSlider.value = 1;
    }

    public void UpdateChromaticAberration()
    {
        #region Chromatic Aberration

        if (!Profile.TryGet<ChromaticAberration>(out var chromaticAbr))
        {
            chromaticAbr = Profile.Add<ChromaticAberration>(false);
        }

        GameManager.Instance.ChromaticAberrationActivity = ChromaticToggle.isOn;
        chromaticAbr.active = ChromaticToggle.isOn;

        #endregion
    }

    public void UpdateFilmGrain()
    {
        #region Film Grain

        if (!Profile.TryGet<FilmGrain>(out var filmGrain))
        {
            filmGrain = Profile.Add<FilmGrain>(false);
        }

        GameManager.Instance.FilmGrainActivity = GrainToggle.isOn;
        filmGrain.active = GrainToggle.isOn;

        #endregion
    }

    public void UpdateMotionBlur()
    {
        #region Motion Blur

        if (!Profile.TryGet<MotionBlur>(out var motionBlur))
        {
            motionBlur = Profile.Add<MotionBlur>(false);
        }

        GameManager.Instance.MotionBlurActivity = MotionBlurToggle.isOn;
        motionBlur.active = MotionBlurToggle.isOn;

        #endregion
    }

    public void UpdateMouseSensitivity()
    {
        #region Mouse Sensitivity

        GameManager.Instance.MouseSensitivity = SensitivitySlider.value;

        #endregion
    }

    public void ResetSensitivity()
    {
        SensitivitySlider.value = 1.5f;
        UpdateMouseSensitivity();
    }

    public void ResetSettings()
    {
        #region Reset All

        BrightnessSlider.value = 1;
        UpdateBrightness();

        ChromaticToggle.SetIsOnWithoutNotify(true);
        UpdateChromaticAberration();

        GrainToggle.SetIsOnWithoutNotify(true);
        UpdateFilmGrain();

        MotionBlurToggle.SetIsOnWithoutNotify(true);
        UpdateMotionBlur();

        SensitivitySlider.value = 1.5f;
        UpdateMouseSensitivity();

        #endregion
    }
}
