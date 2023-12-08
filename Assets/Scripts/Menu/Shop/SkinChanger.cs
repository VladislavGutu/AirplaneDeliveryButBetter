using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    public Skin[] info;
    private bool[] StockCheck;

    public Button buyBttn;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI fuelText;
    public Transform player;
    public int index;
    public Fuel fuelSlider;
    public GameObject ShopCanvas;

    public int coins;

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("coins");
        index = PlayerPrefs.GetInt("chosenSkin");
        coinsText.text = coins.ToString();
        fuelText.text = "Fuel: " + info[index].fuel.ToString();
        fuelSlider.currentFuel = info[index].fuel;

        StockCheck = new bool[53];
        if (PlayerPrefs.HasKey("StockArray"))
            StockCheck = PlayerPrefsX.GetBoolArray("StockArray");

        else
            StockCheck[0] = true;

        info[index].isChosen = true;

        for (int i = 0; i < info.Length; i++)
        {
            info[i].inStock = StockCheck[i];
            if (i == index)
                player.GetChild(i).gameObject.SetActive(true);
            else
                player.GetChild(i).gameObject.SetActive(false);
        }

        priceText.text = "CHOSEN";
        buyBttn.interactable = false;
    }

    public void Back()
    {
        if (PlayerPrefs.HasKey("StockArray"))
            StockCheck = PlayerPrefsX.GetBoolArray("StockArray");

        else
            StockCheck[0] = true;

        //info[index].isChosen = true;

        for (int i = 0; i < info.Length; i++)
        {
            info[i].inStock = StockCheck[i];
            if (i == index)
                player.GetChild(i).gameObject.SetActive(true);
            else
                player.GetChild(i).gameObject.SetActive(false);
        }

    }
    public void Save()
    {
        PlayerPrefsX.SetBoolArray("StockArray",StockCheck);
    }
    public void GameStart()
    {
        if (info[index].inStock)
        {
            GameController.StartGame();
            ShopCanvas.SetActive(false);

        }
    }

    public void ScrollRight()
    {
        if (index < player.childCount)
        {
            index++;

            if (info[index].inStock && info[index].isChosen)
            {
                priceText.text = "CHOOSEN";
                fuelText.text = "Fuel: " + info[index].fuel.ToString();
                fuelSlider.currentFuel = info[index].fuel;
                buyBttn.interactable = true;
             
            }
            else if (!info[index].inStock)
            {
                priceText.text = info[index].cost.ToString();
                fuelText.text = "Fuel: " + info[index].fuel.ToString();
                fuelSlider.currentFuel = info[index].fuel;
                buyBttn.interactable = true;
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                priceText.text = "CHOOSE";
                fuelText.text = "Fuel: " + info[index].fuel.ToString();
                fuelSlider.currentFuel = info[index].fuel;
                buyBttn.interactable = true;
            }

            for (int i = 0; i < player.childCount; i++)
                player.GetChild(i).gameObject.SetActive(false);
            // Можно записать так: player.GetChild(index-1).gameObject.SetActive(false);

            player.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void ScrollLeft()
    {
        if (index > 0)
        {
            index--;

            if (info[index].inStock && info[index].isChosen)
            {
                priceText.text = "CHOSEN";
                buyBttn.interactable = false;
                fuelText.text = "Fuel: " + info[index].fuel.ToString();
                fuelSlider.currentFuel = info[index].fuel;
            }
            else if (!info[index].inStock)
            {
                priceText.text = info[index].cost.ToString();
                buyBttn.interactable = true;
                fuelText.text = "Fuel: "+ info[index].fuel.ToString();
                fuelSlider.currentFuel = info[index].fuel;
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                priceText.text = "CHOOSE";
                buyBttn.interactable = true;
                
                fuelText.text = "Fuel: " + info[index].fuel.ToString();
                fuelSlider.currentFuel = info[index].fuel;
            }

            for (int i = 0; i < player.childCount; i++)
                player.GetChild(i).gameObject.SetActive(false);

            player.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void BuyButtonAction()
    {
        if (buyBttn.interactable && !info[index].inStock)
        {
            if (coins > int.Parse(priceText.text))
            {
                coins -= int.Parse(priceText.text);
                coinsText.text = coins.ToString();
                PlayerPrefs.SetInt("coins", coins);
                StockCheck[index] = true;
                info[index].inStock = true;
                priceText.text = "CHOOSE";
                Save();
            }
        } 

        if (buyBttn.interactable && !info[index].isChosen && info[index].inStock) 
        {
            PlayerPrefs.SetInt("chosenSkin", index);
            buyBttn.interactable = false;
            priceText.text = "CHOSEN";
        }
    }
}


[System.Serializable]
public class Skin
{
    public int cost;
    public int fuel;
    public bool inStock;
    public bool isChosen;
}