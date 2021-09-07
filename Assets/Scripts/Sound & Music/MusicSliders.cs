using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliders : MonoBehaviour
{
    #region fields
    [SerializeField] 
    private Slider _music;                              // get music slider 

    [SerializeField] 
    private Slider _sound;                              // get sound slider
    #endregion

    #region methods
    private void Start()
    {
        _music.value = PlayerPrefs.GetFloat("Music");   // get the pref of music volume
        _sound.value = PlayerPrefs.GetFloat("Sound");   // get the pref of sound volume
    }
    #endregion
}
