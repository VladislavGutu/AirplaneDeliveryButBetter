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
     void Start()
    {
        countDown = baseInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            countDown = baseInterval;
            currentFuel -= 1f;
            fuelDisplay.value = currentFuel / 1000f;
        }

    }
}
