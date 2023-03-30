using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GetPlayers : MonoBehaviour
{
    [SerializeField] private float radiusPlayer = 2;
    [SerializeField] private CinemachineTargetGroup targetAllPlayer;
    public static GetPlayers Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        targetAllPlayer.m_Targets = new CinemachineTargetGroup.Target[4];

    }

    private CinemachineTargetGroup.Target CreateNewTarget(Transform t, float weight, float radius)
    {
        CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
        target.radius = radius;
        target.target = t;
        target.weight = weight;
        return target;
    }

    public void AddPlayerTarget(Transform playerTransform, int id)
    {
       targetAllPlayer.m_Targets[id] = CreateNewTarget(playerTransform, 1, radiusPlayer);
    }

    public void RemovePlayerTarget(int id)
    {
        targetAllPlayer.m_Targets[id] = new CinemachineTargetGroup.Target();

        for (int i = 1; i < 5; i++)
        {
            if (targetAllPlayer.m_Targets[i].weight != 0)
                return;
        }
        
    }
    
}
