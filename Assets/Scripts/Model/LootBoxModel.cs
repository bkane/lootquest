using Assets.Scripts.Model;
using UnityEngine;

/// <summary>
/// Game logic/model
/// </summary>
public class LootBoxModel : MonoBehaviour
{
    public BigNum Money { get; protected set; }
    public BigNum MoneyPerClick { get; protected set; }


    public BigNum AutoClickers { get; protected set; }
    public int TicksPerAutoClick = 30;
    protected int ticksTilAutoClick;

    public void AddMoney(BigNum amount)
    {
        Money += amount;
    }

    public void AddMoneyPerClick(BigNum amount)
    {
        MoneyPerClick += amount;
    }

    protected void FixedUpdate()
    {
        Tick();
    }

    protected void Tick()
    {
        if (AutoClickers > 0)
        {
            ticksTilAutoClick--;

            if (ticksTilAutoClick <= 0)
            {
                AddMoney(MoneyPerClick * AutoClickers);
                ticksTilAutoClick = TicksPerAutoClick;
            }
        }
    }
}
