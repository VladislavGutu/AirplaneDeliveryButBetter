using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public float  currentFuel = 100f; 
    public Slider fuelDisplay;
    float countDown;
    public float baseInterval = 1f;

    private bool _finishBenzin = false;
     void Start()
     {
         fuelDisplay.maxValue = currentFuel;
        countDown = baseInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.CurrentState == GameState.Playing)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                countDown = baseInterval;
                currentFuel -= 1f;
                fuelDisplay.value = currentFuel;
            }

            if (currentFuel < 0)
            {
                _finishBenzin = true;
            }
            
            if (_finishBenzin)
            {
                UIManager._instance.player.StatsMenu.OpenMenu();
                _finishBenzin = false;
            }
        }
        
    }
}
