using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseLevel : MonoBehaviour
{
    public Transform[] teamOnePlayerSpawns;
    public Transform[] teamTwoPlayerSpawns;
    [SerializeField] private GameObject playerPrefabs;

    private void Awake()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            if (playerConfigs[i].PlayerTeam == 1)
            {
                var player = Instantiate(playerPrefabs, teamOnePlayerSpawns[Mathf.Abs(i - (teamOnePlayerSpawns.Length - 1))].position, teamOnePlayerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
                
                GetPlayers.Instance.AddPlayerTarget(player.transform, playerConfigs[i].PlayerIndex);
            }
            else if (playerConfigs[i].PlayerTeam == 2)
            {
                var player = Instantiate(playerPrefabs, teamTwoPlayerSpawns[Mathf.Abs(i - (teamTwoPlayerSpawns.Length - 1))].position, teamTwoPlayerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            
                GetPlayers.Instance.AddPlayerTarget(player.transform, playerConfigs[i].PlayerIndex);
            }
            
        }
    }
}
