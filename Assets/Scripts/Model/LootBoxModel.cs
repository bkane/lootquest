using Assets.Scripts.Model;
using UnityEngine;

/// <summary>
/// Game logic/model
/// </summary>
public class LootBoxModel : MonoBehaviour
{
    public BigNum Money { get; protected set; }
    public BigNum MoneyPerClick { get; protected set; }

    public void AddMoney(BigNum amount)
    {
        Money += amount;
    }

    public void AddMoneyPerClick(BigNum amount)
    {
        MoneyPerClick += amount;
    }
}
