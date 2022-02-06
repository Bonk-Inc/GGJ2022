using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] 
    private string settingName;

    [SerializeField] 
    private Slider slider;
    
    [SerializeField] 
    private AudioMixer mixer;

    [SerializeField]
    private float minDB = -80f, maxDB = 0f;

    private string playerPrefKey;

    private float LogToLinear(float log) => Mathf.Pow(10, (log / 10));

    private float LinearToLog(float linear) => Mathf.Log10(linear) * 20;

    private void Awake()
    {
        slider.maxValue = LogToLinear(maxDB);
        slider.minValue = LogToLinear(minDB);
        
        slider.value = LogToLinear(LoadSetting());
        
        slider.onValueChanged.AddListener((val) => SetVolume());
    }

    private void Start() {
        LoadSetting();
    }
    
    private void OnDestroy()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    private void SetVolume()
    {
        print(mixer.name + "   " + LinearToLog(slider.value));
        mixer.SetFloat("volume", LinearToLog(slider.value));
        PlayerPrefs.SetFloat(playerPrefKey, LinearToLog(slider.value));
    }

    private float LoadSetting()
    {
        playerPrefKey = $"vol_{settingName}";

        float startValue;
        if (PlayerPrefs.HasKey(playerPrefKey)) {
            startValue = PlayerPrefs.GetFloat(playerPrefKey);
            mixer.SetFloat("volume", startValue);
        } else {
            mixer.GetFloat("volume", out startValue);
        }
        return startValue;
    }
}