using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Game logic/model
/// </summary>
public class LootBoxModel : MonoBehaviour
{
    public Dictionary<Units, BigNum> Resources { get; protected set; }

    public MacGuffinQuest MacGuffinQuest { get; protected set; }

    //Short-hand
    public BigNum Money             { get { return Resources[Units.Money]; }            protected set { Resources[Units.Money] = value; } }
    public BigNum MoneyPerClick     { get { return Resources[Units.MoneyPerClick]; }    protected set { Resources[Units.MoneyPerClick] = value; } }
    public BigNum AutoClickers      { get { return Resources[Units.AutoClicker]; }      protected set { Resources[Units.AutoClicker] = value; } }

    //MacGuffin Quest
    public BigNum LootBoxes         { get { return Resources[Units.LootBox]; } protected set { Resources[Units.LootBox] = value; } }


    public int TicksPerAutoClick = 30;
    protected int ticksTilAutoClick;


    private void Awake()
    {
        List<Units> unitTypes = Enum.GetValues(typeof(Units)).Cast<Units>().ToList();

        Resources = new Dictionary<Units, BigNum>();
        for (int i = 0; i < unitTypes.Count; i++)
        {
            Units type = unitTypes[i];
            Resources.Add(type, 0);
        }

        MacGuffinQuest = new MacGuffinQuest(this);

        SetInitialState();
    }

    protected void SetInitialState()
    {
        MoneyPerClick = 1;
    }

    public bool Consume(Units type, BigNum amount)
    {
        if (Resources[type] >= amount)
        {
            Resources[type] -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Add(Units type, BigNum amount)
    {
        Resources[type] = Resources[type] + amount;
    }

    public void Click(BigNum numClicks)
    {
        Add(Units.Money, MoneyPerClick * numClicks);
    }

    protected void FixedUpdate()
    {
        Tick();
    }

    protected void Tick()
    {
        if (AutoClickers.value > 0)
        {
            ticksTilAutoClick--;

            if (ticksTilAutoClick <= 0)
            {
                Click(AutoClickers);
                ticksTilAutoClick = TicksPerAutoClick;
            }
        }
    }
}
