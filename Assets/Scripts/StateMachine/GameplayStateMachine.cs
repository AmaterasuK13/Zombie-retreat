using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayStateMachine : MonoBehaviour
{
    #region fields
    [SerializeField] 
    private GameObject _pausePanel;         // get pause panel
    [SerializeField] 
    private GameObject _player;             // get player on scene

    [SerializeField] 
    private Image _heart1;                  // heart that shown first amount of hp
    [SerializeField] 
    private Image _heart2;                  // heart that shown second amount of hp
    [SerializeField] 
    private Image _heart3;                  // heart that shown third amount of hp

    [SerializeField] 
    private Text _scoreText;                // text showing current score
    [SerializeField] 
    private Text _highScoreText;            // text showing current high score
    [SerializeField] 
    private Text _ammoCountText;            // text showing current amount of bullets
    #endregion

    #region methods
    private void Start()
    {
        _highScoreText.text = GameData.instance.highScore.ToString();       // show current high score
    }

    private void Update()
    {
        switch (_player.GetComponent<Character>().CurrentHealth)            // get character current health
        {
            case 3:                                                         // show three hearts if players hp is three
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(true);
                _heart3.gameObject.SetActive(true);
                break;
            case 2:                                                         // show two hearts if players hp is two
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(true);
                _heart3.gameObject.SetActive(false);
                break;
            case 1:                                                         // show one hearts if players hp is one
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(false);
                _heart3.gameObject.SetActive(false);
                break;
            default:                                                        // show zero hearts if players hp is zero
                _heart1.gameObject.SetActive(false);
                _heart2.gameObject.SetActive(false);
                _heart3.gameObject.SetActive(false);
                break;
        }
        _scoreText.text = GameData.instance.currentScore.ToString();        // show current score 
        _ammoCountText.text = GameData.instance.ammoCount.ToString();       // show current amount of ammo
    }

    /// <summary>
    /// Method that alows to pause o runpause game
    /// </summary>
    public void PauseOrUnpause()
    {
        if (_pausePanel.activeSelf == true)
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _player.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _player.GetComponent<PlayerInput>().enabled = false;
        }
    }

    /// <summary>
    /// Method that starts game again
    /// </summary>
    public void StartAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Method that getting player back to main menu scene
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    #endregion
}