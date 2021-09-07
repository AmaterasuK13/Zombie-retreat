using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    #region fields
    private GameObject[] _spawnPoint;               // points where enemies can spawn

    [SerializeField]
    private GameObject _zombieRegular;              // regular zombie prefab
    [SerializeField]
    private GameObject _zombieFast;                 // fast zombie prefab
    [SerializeField]
    private GameObject _zombieSlow;                 // slow zombie prefab
    [SerializeField]
    private GameObject _zombieBoss;                 // boss zombie prefab

    [SerializeField]
    private GameObject _waveClearText;              // text that show that wave is clear

    [SerializeField]
    private int _nextWaveRegularEnemyCount;         // amount of regular zombies next wave
    [SerializeField]
    private int _nextWaveExtraRegularEnemyCount;    // amount of regular zombies that will be added after next wave

    [SerializeField]
    private int _nextWaveFastEnemyCount;            // amount of fast zombies next wave
    [SerializeField]
    private int _nextWaveExtraFastEnemyCount;       // amount of fast zombies that will be added after next wave

    [SerializeField]
    private int _nextWaveSlowEnemyCount;            // amount of slow zombies next wave
    [SerializeField]
    private int _nextWaveExtraSlowEnemyCount;       // amount of slow zombies that will be added after next wave

    [SerializeField]
    private int _nextWaveBossEnemyCount;            // amount of boss zombies next wave
    [SerializeField]
    private int _nextWaveExtraBossEnemyCount;       // amount of boss zombies that will be added after next wave

    [SerializeField]
    private Text _waveCountText;                    // text that shows current wave number
    #endregion

    #region properties
    /// <summary>
    /// Shows current wave number
    /// </summary>
    public int WaveCount { get; private set; }
    #endregion

    #region methods
    private void Awake()
    {
        WaveCount = 0;                                                      // making current wave first
        _waveCountText.text = WaveCount.ToString();                         // showing current wave
        _spawnPoint = GameObject.FindGameObjectsWithTag("EnemySpawner");    // find all enemies spawn points
    }

    private void Update()
    {
        if (GameData.instance.enemyCount <= 0)                              // check if there are no enemies in scene
        {
            StartCoroutine(WaitForWave());                                  // show after mave panel
            Spawn();                                                        // spawn new enemies
        }
    }

    /// <summary>
    /// Method that realize enemies spawning
    /// </summary>
    private void Spawn()
    {
        for (int i = 0; i < _nextWaveRegularEnemyCount; i++)    // realize regular zombies spawn
        {
            Instantiate(_zombieRegular, _spawnPoint[Random.Range(0, _spawnPoint.Length)].transform.position, Quaternion.identity);
            GameData.instance.enemyCount++;
        }

        for (int i = 0; i < _nextWaveFastEnemyCount; i++)       // realize fast zombies spawn
        {
            Instantiate(_zombieFast, _spawnPoint[Random.Range(0, _spawnPoint.Length)].transform.position, Quaternion.identity);
            GameData.instance.enemyCount++;
        }

        if (WaveCount % 3 == 0 && WaveCount != 0)               // check if its wave where slow zombies should appear
        {
            for (int i = 0; i < _nextWaveSlowEnemyCount; i++)   // realize slow zombies spawn
            {
                Instantiate(_zombieSlow, _spawnPoint[Random.Range(0, _spawnPoint.Length)].transform.position, Quaternion.identity);
                GameData.instance.enemyCount++;
            }
            _nextWaveSlowEnemyCount += _nextWaveExtraSlowEnemyCount;
        }

        if (WaveCount % 5 == 0 && WaveCount != 0)               // check if its wave where boss zombies should appear
        {
            for (int i = 0; i < _nextWaveBossEnemyCount; i++)   // realize boss zombies spawn
            {
                Instantiate(_zombieBoss, _spawnPoint[Random.Range(0, _spawnPoint.Length)].transform.position, Quaternion.identity);
                GameData.instance.enemyCount++;
            }
            _nextWaveBossEnemyCount += _nextWaveExtraBossEnemyCount;
        }

        _nextWaveFastEnemyCount += _nextWaveExtraFastEnemyCount;            // add extra slow enemies to next wave
        _nextWaveRegularEnemyCount += _nextWaveExtraRegularEnemyCount;      // add extra regular enemies to next wave
        WaveCount++;                                                        // add wave count
        _waveCountText.text = WaveCount.ToString();                         // show current wave
    }

    IEnumerator WaitForWave()
    {
        if (WaveCount != 0)
        {
            _waveClearText.SetActive(true);
            yield return new WaitForSeconds(2);
            _waveClearText.SetActive(false);
        }
    }
    #endregion
}
