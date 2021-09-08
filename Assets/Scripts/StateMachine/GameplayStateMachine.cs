using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayStateMachine : MonoBehaviour
{
    #region fields
    [SerializeField] 
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _lossPanel;

    [SerializeField] 
    private GameObject _player;

    [SerializeField] 
    private Image _heart1;
    [SerializeField] 
    private Image _heart2;
    [SerializeField] 
    private Image _heart3;

    [SerializeField] 
    private Text _scoreText;
    [SerializeField] 
    private Text _highScoreText;
    [SerializeField] 
    private Text _ammoCountText;
    [SerializeField]
    private Text _finScoreText;
    #endregion

    #region methods
    private void Start()
    {
        _highScoreText.text = GameData.instance.highScore.ToString();
    }

    private void Update()
    {
        switch (_player.GetComponent<Character>().CurrentHealth)
        {
            case 3:
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(true);
                _heart3.gameObject.SetActive(true);
                break;
            case 2: 
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(true);
                _heart3.gameObject.SetActive(false);
                break;
            case 1: 
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(false);
                _heart3.gameObject.SetActive(false);
                break;
            default:
                _gamePanel.GetComponent<Image>().color = new Color(1, .3f, .3f, .1f);
                _heart1.gameObject.SetActive(false);
                _heart2.gameObject.SetActive(false);
                _heart3.gameObject.SetActive(false);
                _lossPanel.SetActive(true);
                _finScoreText.text = GameData.instance.currentScore.ToString();
                break;
        }
        _scoreText.text = GameData.instance.currentScore.ToString();
        _ammoCountText.text = GameData.instance.ammoCount.ToString();
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