using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    public string levelName;
    
    private List<PlayerConfiguration> _playerConfigs;

    [SerializeField]private int _maxPlayer = 4;
    public static PlayerConfigurationManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Trying to create another instance of PlayerConfigurationManager ");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            _playerConfigs = new List<PlayerConfiguration>();
        }

        if (SceneManager.GetActiveScene().name == "End")
        {
            gameObject.SetActive(false);
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return _playerConfigs;
    }

    public void SetPlayerSkins(int index, int whichSkin)
    {
        _playerConfigs[index].PlayerSkins = whichSkin;
    }
    
    public void SetPlayerTeam(int index, int whichTeam)
    {
        _playerConfigs[index].PlayerTeam = whichTeam;
    }

    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count > 1 && _playerConfigs.Count <= _maxPlayer && _playerConfigs.All(p => p.IsReady))
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (!_playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    
    public PlayerInput Input {get; set;}
    public int PlayerIndex {get; set;}
    
    public int PlayerTeam {get; set;}
    public bool IsReady {get; set;}
    public int PlayerSkins {get; set;}
}
