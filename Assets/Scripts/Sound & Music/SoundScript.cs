using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    #region fields
    [SerializeField] 
    private AudioSource[] _sounds;                                  // get all sounds on object
    #endregion

    #region methods
    private void Update()
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            _sounds[i].volume = PlayerPrefs.GetFloat("Sound");  // // get the pref of sound volume
        }
    }
    #endregion
}
