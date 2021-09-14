using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    #region Constants
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
    #endregion

    public static GameData Instance;

    public int currentScore = 0;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    public int highScore;

    public float musicVolume = 0.5f;

    public float soundVolume = 0.5f;

    public int enemyCount = 0;

    public int waveCount = 0;

    public int ammoCount = 50;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SaveData()
    {
        if(currentScore > highScore)
        {
            PlayerPrefs.SetInt("Score", currentScore);
        }
    }

    public void LoadData()
    {
        highScore = PlayerPrefs.GetInt("Score");
    }

    public void GetMusicVolume()
    {
        musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("Music", musicVolume);
    }

    public void GetSoundVolume()
    {
        soundVolume = soundSlider.value;
        PlayerPrefs.SetFloat("Sound", soundVolume);
    }
}
