using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private static readonly string KEY_HIGH_SCORE = "HighScore";

    [HideInInspector] public bool isGameOver { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource musicPlayer;

    [SerializeField] private AudioSource gameOverSFX;

    [Header("Score")]
    [SerializeField] private float score;
    [SerializeField] private int highScore;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
        }

        score = 0;
        highScore = PlayerPrefs.GetInt(KEY_HIGH_SCORE);
    }

    private void Update()
    {
        if(!isGameOver)
        {
            score += Time.deltaTime;

            if(GetScore() > GetHighScore())
            {
                highScore = GetScore();
            }
        }
    }

    public int GetScore()
    {
        return (int)Mathf.Floor(score);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void EndGame()
    {
        if (isGameOver) return;

        isGameOver = true;

        musicPlayer.Stop();

        gameOverSFX.Play();

        PlayerPrefs.SetInt(KEY_HIGH_SCORE, GetHighScore());

        StartCoroutine(ReloadScene(6f));
    }

    private IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);

    }
}
