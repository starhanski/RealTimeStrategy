using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
   public int startingMoney = 1000;  // Початкова кількість грошей
    private int currentMoney;         // Поточна кількість грошей

    public Text moneyText;           // Text UI для відображення грошей на екрані

    void Start()
    {
        currentMoney = startingMoney;
        UpdateMoneyText();
        StartCoroutine(GetMoreMoney());
    }
  
    public bool CanAfford(int cost)
    {
        return currentMoney >= cost;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyText();
    }

    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            currentMoney -= amount;
            UpdateMoneyText();
        }
        else
        {
            Debug.LogWarning("Not enough money!");
        }
    }

    void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "Your Money: " + currentMoney;
        }
    }



    IEnumerator GetMoreMoney()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            AddMoney(1);
        }
    }
}
