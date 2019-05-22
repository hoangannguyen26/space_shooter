using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    [Header("UI settings")]
    public Text m_scoreText;
    public Text m_resetText;
    public Text m_gameOverText;

    private int m_score;
    private bool m_reset;
    private bool m_gameOver;
    // Start is called before the first frame update
    void Start()
    {
        m_score = 0;
        m_reset = false;
        m_gameOver = false;
        m_scoreText.text = "";
        m_resetText.text = "";
        m_gameOverText.text = "";
        UpdateScoreUI();
        StartCoroutine(SpawnWaves());

    }

    void Update()
    {
        if (m_reset)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawsPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawsPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
                if (m_gameOver)
                {
                    m_resetText.text = "Press 'R' for reset";
                    m_reset = true;
                    break;
                }
            }
            yield return new WaitForSeconds(waveWait);

        }
    }
    public void AddScore(int scoreValue)
    {
        m_score += scoreValue;
        UpdateScoreUI();
    }
    public void GameOver()
    {
        m_gameOverText.text = "Game Over!";
        m_gameOver = true;
    }
    private void UpdateScoreUI()
    {
        m_scoreText.text = "Score: " + m_score;
    }
}
