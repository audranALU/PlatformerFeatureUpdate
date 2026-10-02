using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    private float volume;
    private bool muted;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume", 1f);
        muted = PlayerPrefs.GetInt("Muted", 0) == 1;

        volumeSlider.SetValueWithoutNotify(volume);
        muteToggle.SetIsOnWithoutNotify(muted);

        ApplyAudio();
    }

    public void SetVolume(float value)
    {
        volume = value;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        ApplyAudio();
    }

    public void SetMute(bool value)
    {
        muted = value;
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyAudio();
    }

    void ApplyAudio()
    {
        AudioListener.volume = muted ? 0f : volume;
    }
}