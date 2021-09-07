using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStateMachine : MonoBehaviour
{
    #region fields
    [SerializeField] private GameObject _loadScreen;        // get load screen
    [SerializeField] private Slider _loadingScreenSlider;   // get load screen slider

    private AsyncOperation _async;

    #endregion

    #region methods
    /// <summary>
    /// Method that switch to gameplay scene
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(LoadingScreen());
    }

    /// <summary>
    /// Method that close game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }


    IEnumerator LoadingScreen()                                     // realizing loading screen 
    {
        _loadScreen.SetActive(true);
        _async = SceneManager.LoadSceneAsync("GameplayScene");
        _async.allowSceneActivation = false;
        while (_async.isDone == false)
        {
            _loadingScreenSlider.value = _async.progress;
            if (_async.progress == 0.9f)
            {
                _loadingScreenSlider.value = 1f;
                _async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    #endregion
}
