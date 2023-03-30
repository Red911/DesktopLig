using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int _playeIndex;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button readyButton;

    private float _ignoreInputTime = 1.5f;
    private bool _inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        _playeIndex = pi;
        titleText.SetText("Player " + (pi + 1));
        _ignoreInputTime = Time.time + _ignoreInputTime;
    }
    void Update()
    {
        if (Time.time > _ignoreInputTime)
        {
            _inputEnabled = true;
        }
    }

    public void SetTeam(int whichTeam)
    {
        if(!_inputEnabled) {return;}
        
        PlayerConfigurationManager.Instance.SetPlayerTeam(_playeIndex, whichTeam);
    }
    public void SetColor(Material color)
    {
        if(!_inputEnabled) {return;}
        
        PlayerConfigurationManager.Instance.SetPlayerColor(_playeIndex, color);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if(!_inputEnabled) {return;}
        
        PlayerConfigurationManager.Instance.ReadyPlayer(_playeIndex);
        readyButton.gameObject.SetActive(false);    
    }
}
