using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Transform qualityLevelContainer;
    public Slider audioSlider;
    public AudioMixer mixer;
    public Color qualityNotSelectedColor;
    public Color qualitySelectedColor;


    private void OnEnable()
    {
        RefreshView();
    }

    public void OnAudioValueChange()
    {
        mixer.SetFloat("volume", audioSlider.value);
    }

    private void RefreshView()
    {
        if (qualityLevelContainer == null) return;

        int currQuality = QualitySettings.GetQualityLevel();
        for (int i = 0; i < qualityLevelContainer.childCount; i++)
        {
            Image img = qualityLevelContainer.GetChild(i).GetComponent<Image>();
            if (img != null)
            {
                if (i == currQuality)
                {
                    img.color = qualitySelectedColor;
                }
                else
                {
                    img.color = qualityNotSelectedColor;
                }
            }
        }
    }

    public void SetQualityLevel(int level)
    {
        QualitySettings.SetQualityLevel(level);
        RefreshView();
    }

}
