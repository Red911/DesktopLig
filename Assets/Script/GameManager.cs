using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int teamOneScore;
    public int teamTwoScore;
    
    public TextMeshProUGUI teamOneScoreTxt;
    public TextMeshProUGUI teamTwoScoreTxt;

    public Timer timer;
    public TransitionTimer transitionTimer;

    private TextMeshProUGUI _teamOneUI;
    private TextMeshProUGUI _teamTwoUI;
    private TextMeshProUGUI resultText;

    private string _getLevelName;
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Trying to create another instance of GameManager");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "End")
        {
            _teamOneUI = GameObject.Find("Team1Score").GetComponentInChildren<TextMeshProUGUI>();
            _teamTwoUI = GameObject.Find("Team2Score").GetComponentInChildren<TextMeshProUGUI>();
        
            timer = GameObject.Find("Timer").GetComponentInChildren<Timer>();
            transitionTimer = GameObject.Find("TransitionTimer").GetComponentInChildren<TransitionTimer>();
        
            resultText = GameObject.Find("Result").GetComponentInChildren<TextMeshProUGUI>();
        
            _teamOneUI.text = teamOneScore.ToString();
            _teamTwoUI.text = teamTwoScore.ToString();
        }
        
            
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "End")
        {
            if (_teamOneUI == null && _teamTwoUI == null)
            {
                _teamOneUI = GameObject.Find("Team1Score").GetComponentInChildren<TextMeshProUGUI>();
                _teamTwoUI = GameObject.Find("Team2Score").GetComponentInChildren<TextMeshProUGUI>();
            
                _teamOneUI.text = teamOneScore.ToString();
                _teamTwoUI.text = teamTwoScore.ToString();
            }
        
            if (timer == null && transitionTimer == null)
            {
                timer = GameObject.Find("Timer").GetComponentInChildren<Timer>();
                transitionTimer = GameObject.Find("TransitionTimer").GetComponentInChildren<TransitionTimer>();
            }
        
            if (resultText == null)
            {
                resultText = GameObject.Find("Result").GetComponentInChildren<TextMeshProUGUI>();
            }
            
            NextScene();
            Result();
        }
        else
        {
            teamOneScoreTxt = GameObject.Find("Team1Score").GetComponentInChildren<TextMeshProUGUI>();
            teamTwoScoreTxt = GameObject.Find("Team2Score").GetComponentInChildren<TextMeshProUGUI>();

            teamOneScoreTxt.text = teamOneScore.ToString();
            teamTwoScoreTxt.text = teamTwoScore.ToString();
        }
        
        
    }

    public void UpdateScore(int team)
    {
        switch (team)
        {
            case 1:
                teamOneScore += 50;
                _teamOneUI.text = teamOneScore.ToString();
                break;
            case 2:
                teamTwoScore += 50;
                _teamTwoUI.text = teamTwoScore.ToString();
                break;
        }
    }

    private void NextScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Dojo" : 
                _getLevelName = "Dojo2";
                break;
            
            case "Dojo2" : 
                _getLevelName = "Dojo3";
                break;
            
            case "Dojo3" : 
                _getLevelName = "End";
                break;
        }
    }

    private void Result()
    {
        if (!timer.TimerOn)
        {
            transitionTimer.TimerOn = true;
            if (teamOneScore > teamTwoScore)
            {
                resultText.text = "Team 1 Win !";
                if (transitionTimer.TimeLeft == 0)
                {
                    print("finito1");
                    SceneManager.LoadScene(_getLevelName);
                }
            }
            else if (teamOneScore < teamTwoScore)
            {
                resultText.text = "Team 2 Win !";
                if (transitionTimer.TimeLeft == 0)
                {
                    print("finito2");
                    SceneManager.LoadScene(_getLevelName);
                }
            }
            else if(teamOneScore == teamTwoScore)
            {
                resultText.text = "Draw";
                if (transitionTimer.TimeLeft == 0)
                {
                    print("finitoD");
                    SceneManager.LoadScene(_getLevelName);
                }
            }
            
        }
    }
}
