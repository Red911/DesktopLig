using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSpawnKill : MonoBehaviour
{
    public int teamZone;
    private void OnTriggerStay(Collider other)
    {
        if (teamZone == 1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerInputHandler>().whichTeam == 2)
                {
                    other.gameObject.GetComponent<PlayerMovement>().canShoot = false;
                }
            } 
        }
        else if (teamZone == 2)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerInputHandler>().whichTeam == 1)
                {
                    other.gameObject.GetComponent<PlayerMovement>().canShoot = false;
                }
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shuriken"))
        {
            var getPlayerTeam = other.gameObject.GetComponent<Boomerang>().playerOwner.GetComponent<PlayerInputHandler>().whichTeam;
            if (teamZone == 1)
            {
                if (getPlayerTeam == 2)
                {
                    Destroy(other.gameObject);
                }
            }
            else if (teamZone == 2)
            {
                if ( getPlayerTeam == 1)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shuriken"))
        {
            var getPlayerTeam = other.gameObject.GetComponent<Boomerang>().playerOwner
                .GetComponent<PlayerInputHandler>().whichTeam;

            if (teamZone == 1)
            {
                if (getPlayerTeam == 1)
                {
                    Destroy(other.gameObject);
                }
            }
            else if (teamZone == 2)
            {
                if (getPlayerTeam == 2)
                {
                    Destroy(other.gameObject);
                }
            }
        }

    }
}
