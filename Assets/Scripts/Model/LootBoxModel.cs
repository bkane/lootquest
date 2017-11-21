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

    public UpgradeManager UpgradeManager    { get; protected set; }
    public TimeModel Time                   { get; protected set; }
    public MacGuffinQuest MacGuffinQuest    { get; protected set; }

    //Short-hand
    //Life
    public BigNum Energy            { get { return Resources[Units.Energy]; }           protected set { Resources[Units.Energy] = value; } }

    //Job
    public BigNum Money             { get { return Resources[Units.Money]; }            protected set { Resources[Units.Money] = value; } }
    public BigNum MoneyPerClick     { get { return Resources[Units.MoneyPerClick]; }    protected set { Resources[Units.MoneyPerClick] = value; } }
    public BigNum AutoClickers      { get { return Resources[Units.AutoClicker]; }      protected set { Resources[Units.AutoClicker] = value; } }

    //MacGuffin Quest
    public BigNum LootBoxes         { get { return Resources[Units.LootBox]; }          protected set { Resources[Units.LootBox] = value; } }


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

        UpgradeManager = new UpgradeManager(this);
        Time = new TimeModel(this);
        MacGuffinQuest = new MacGuffinQuest(this);

        SetInitialState();
    }

    protected void SetInitialState()
    {
        MoneyPerClick = 1;
        Energy = 16;
    }

    public bool Consume(Resource resource)
    {
        return Consume(resource.Type, resource.Amount);
    }

    public bool Consume(Units type, BigNum amount)
    {
        if (Resources[type] >= amount)
        {
            Resources[type] -= amount;

            switch(type)
            {
                case Units.Energy:
                    {
                        Tick((int)amount * 30);
                    }
                    break;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Consume(List<Resource> costs)
    {
        bool canAfford = true;
        for (int i = 0; i < costs.Count; i++)
        {
            Resource cost = costs[i];
            if (Resources[cost.Type] < cost.Amount)
            {
                canAfford = false;
                break;
            }
        }

        if (canAfford)
        {
            //All necessary costs can be paid, so consume them all now
            for (int i = 0; i < costs.Count; i++)
            {
                Consume(costs[i]);
            }

            return true;
        }
        else
        {
            //Not enough resources to fulfill all requirements
            return false;
        }
    }

    public bool Convert(List<Resource> costs, List<Resource> products)
    {
        if (Consume(costs))
        { 
            //Then produce all the products
            for(int i = 0; i < products.Count; i++)
            {
                Add(products[i]);
            }

            return true;
        }
        else
        {
            //Not enough resources to fulfill all requirements
            return false;
        }
    }

    public void Add(Resource resource)
    {
        Add(resource.Type, resource.Amount);
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
        Tick(1);
    }

    protected void Tick(int tickCount)
    {
        Time.Tick(tickCount);

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
