using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScript : MonoBehaviour
{
    public GameObject[] platforms;

    // time player has to move onto the correct platform
    public float timeToChoose;
    private float _timer;

    // time player has before next round starts
    public float timeBetweenRounds;
    private float _roundTimer;

    //buffer time - time player stands on single platform before awarding points and moving to next round
    public float bufferTime;
    private float _bufferTimer;
    private bool _inBufferPeriod;

    private GameObject _correctPlatform;
    private bool _roundInProgress;
    private int _score;

    private bool _gameOver;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", Color.white);
        _timer = timeToChoose;
        _roundTimer = timeBetweenRounds;
        _bufferTimer = bufferTime;
        _roundInProgress = false;
        _score = 0;
        _inBufferPeriod = false;
        _gameOver = false;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameOver)
        {
            if (!_inBufferPeriod)
            {
                if (!_roundInProgress)
                {
                    _roundTimer -= Time.deltaTime;
                    UpdateText("Get Ready!\n" + _roundTimer.ToString("n2"));
                    if (_roundTimer <= 0f)
                    {
                        _roundInProgress = true;
                        SelectPlatform();
                    }
                }
                else
                {
                    _timer -= Time.deltaTime;
                    UpdateText("GO!\n" + _timer.ToString("n2"));

                    if (_timer <= 0f)
                    {
                        DeactivePlatforms();
                        _inBufferPeriod = true;
                    }
                }
            }
            else if (_inBufferPeriod)
            {
                _bufferTimer -= Time.deltaTime;
                UpdateText("Round Over!");
                if (_bufferTimer <= 0f)
                {
                    if (GetComponent<Player>().IsDead())
                    {
                        _gameOver = true;
                    }
                    else
                    {
                        UpdateScore(1);
                        ResetRound();
                    }
                }
            }
        }
        else
        {
            UpdateText("GAME OVER");
            SetColors(Color.gray);
        }
    }

    private GameObject SelectPlatform()
    {
        _correctPlatform = platforms[Random.Range(0, platforms.Length)];
        Color color = _correctPlatform.GetComponent<Renderer>().material.GetColor("_Color");
        SetColors(color);
        return _correctPlatform;
    }

    private void DeactivePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            if (platform != _correctPlatform)
                platform.GetComponent<PlatformController>().SetActive(false);
        }
    }

    private void ResetRound()
    {
        _timer = timeToChoose;
        _roundTimer = timeBetweenRounds;
        _bufferTimer = bufferTime;
        _roundInProgress = false;
        _inBufferPeriod = false;
        foreach (GameObject platform in platforms)
        {
            platform.GetComponent<PlatformController>().SetActive(true);
        }
        SetColors(Color.white);
    }

    private void UpdateText(string text)
    {
        var countdowns = GameObject.FindGameObjectsWithTag("Countdown");
        foreach (var countdown in countdowns)
        {
            countdown.GetComponent<TextMeshPro>().text = text;
        }
    }

    private void UpdateScore(int points)
    {
        _score += points;
        var scores = GameObject.FindGameObjectsWithTag("Score");
        foreach (var score in scores)
        {
            score.GetComponent<TextMeshPro>().text = "Score: " + _score.ToString();
        }
    }

    private void SetColors(Color color)
    {
        RenderSettings.skybox.SetColor("_Tint", color);
        foreach (var countdown in GameObject.FindGameObjectsWithTag("Countdown"))
        {
            countdown.GetComponent<TextMeshPro>().color = color;
        }
        foreach (var score in GameObject.FindGameObjectsWithTag("Score"))
        {
            score.GetComponent<TextMeshPro>().color = color;
        }
    }
}
