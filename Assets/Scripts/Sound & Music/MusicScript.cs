using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    #region fields
    [SerializeField] 
    private AudioSource _music;            // get audio sourse
    #endregion

    #region methods
    private void Start()
    {
        _music.volume = PlayerPrefs.GetFloat("Music");      // get the pref of music volume
    }
    #endregion
}
