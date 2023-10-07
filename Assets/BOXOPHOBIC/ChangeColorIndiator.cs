using System;
using System.Collections;
using System.Collections.Generic;
using SickscoreGames.HUDNavigationSystem;
using UnityEngine;

public class ChangeColorIndiator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter" );
        if (other.gameObject.CompareTag("Package"))
        {
            GameObject o;
            (o = gameObject).GetComponent<HUDNavigationElement>().enabled = false;
            Destroy(o,2);
        }
    }
}
