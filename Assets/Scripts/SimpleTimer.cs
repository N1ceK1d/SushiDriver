using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Text timerText;

    public GameObject endScreen;
    public Text ScoreText1;
    public Text endScoreText; 
 
    private float _timeLeft = 0f;
 
    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
        if(_timeLeft == 0)
        {
            endScreen.SetActive(true);
            Time.timeScale = 0f;
            int resultString;
            int.TryParse(Regex.Match(ScoreText1.text, @"\d+").Value, out resultString);
            endScoreText.text = "ИГРА ОКОНЧЕНА\nВаш счёт: " + resultString.ToString();
        }
    }
 
    private void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }
 
    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;
 
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
