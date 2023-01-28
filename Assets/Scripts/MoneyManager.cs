using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public static MoneyManager instance;
    public UnityEvent onSpendMoney;
    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            moneyText.text = "" + _money;
            if (value < 0)
            {
                onSpendMoney.Invoke();
            }
        }
    }
    private int _money;

    public void Start()
    {
        instance = this;
        moneyText.text = "" + _money;
        money = 1000;
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
    }

    public void GainMoney(int amount)
    {
        money += amount;
    }
}
