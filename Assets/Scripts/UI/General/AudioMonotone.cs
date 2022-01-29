using UnityEngine;
using UnityEngine.UI;

public class AudioMonotone : MonoBehaviour
{
    [SerializeField] 
    private string settingName;

    [SerializeField] 
    private Toggle toggle;

    private string playerPrefKey;

    private void Awake()
    {
        toggle.isOn = LoadSetting();
        toggle.onValueChanged.AddListener(HandleToggleChange);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveAllListeners();
    }

    private void HandleToggleChange(bool toggled)
    {
        PlayerPrefs.SetInt(playerPrefKey, (toggled ? 1 : 0));
        SetSpeakerMode(toggled);
    }

    private bool LoadSetting()
    {
        playerPrefKey = $"vol_{settingName}";

        bool toggled;
        if (PlayerPrefs.HasKey(playerPrefKey)) {
            toggled = PlayerPrefs.GetInt(playerPrefKey) != 0;
            SetSpeakerMode(toggled);
        } else {
            toggled = false;
        }

        return toggled;
    }

    private void SetSpeakerMode(bool mono)
    {
        var audioConfig = AudioSettings.GetConfiguration();
        audioConfig.speakerMode = mono ? AudioSpeakerMode.Mono : AudioSpeakerMode.Stereo;

        AudioSettings.Reset(audioConfig);
    }
}