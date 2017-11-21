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
    public Dictionary<Units, Resource> Resources { get; protected set; }

#if DEBUG
    public bool DollarBuys = false; //DEBUG
#endif

    public long TickCount { get; protected set; }

    public UpgradeManager UpgradeManager    { get; protected set; }
    public TimeModel Time                   { get; protected set; }
    public LifeModel Life                   { get; protected set; }
    public JobModel Job                     { get; protected set; }
    public MacGuffinQuest MacGuffinQuest    { get; protected set; }
    public InfluencerModel Influencer       { get; protected set; }

    //Short-hand
    //Life

    //Job
    public BigNum Money             { get { return Resources[Units.Money].Amount; } }
    public BigNum JobProgress       { get { return Resources[Units.JobProgress].Amount; } }
    public BigNum JobsCompleted     { get { return Resources[Units.JobCompleted].Amount; } }

    //MacGuffin Quest
    public BigNum GrindProgress     { get { return Resources[Units.GrindProgress].Amount; } }
    public BigNum GrindsCompleted   { get { return Resources[Units.GrindCompleted].Amount; } }
    public BigNum LootBoxes         { get { return Resources[Units.LootBox].Amount; } }
    public BigNum LootBoxesOpened   { get { return Resources[Units.LootBoxOpened].Amount; } }
    public BigNum TrashItems        { get { return Resources[Units.TrashItem].Amount; } }
    public BigNum TrashItemsSold    { get { return Resources[Units.TrashItemSold].Amount; } }
    public BigNum NumBotAccounts    { get { return Resources[Units.BotAccount].Amount; } }

    //Influencer
    public BigNum VideoContent      { get { return Resources[Units.VideoContent].Amount; } }
    public BigNum VideoProgress     { get { return Resources[Units.VideoProgress].Amount; } }
    public BigNum Followers         { get { return Resources[Units.Follower].Amount; } }
    public BigNum PublishedVideos   { get { return Resources[Units.PublishedVideo].Amount; } }
   

    private void Awake()
    {
        List<Units> unitTypes = Enum.GetValues(typeof(Units)).Cast<Units>().ToList();

        Resources = new Dictionary<Units, Resource>();
        for (int i = 0; i < unitTypes.Count; i++)
        {
            Units type = unitTypes[i];
            Resources.Add(type, new Resource(type, 0));
        }

        UpgradeManager = new UpgradeManager(this);
        Time = new TimeModel(this);

        Life = new LifeModel(this);
        Life.IsActive = true;

        Job = new JobModel(this);
        Job.IsActive = true;

        MacGuffinQuest = new MacGuffinQuest(this);
        MacGuffinQuest.IsActive = false;

        Influencer = new InfluencerModel(this);
        Influencer.IsActive = false;

        SetInitialState();
    }

    protected void SetInitialState()
    {
        Resources[Units.GrindProgress].MaxValue = 100;
        Resources[Units.VideoProgress].MaxValue = 100;
    }

    private void SetResource(Units type, BigNum value)
    {
        Resources[type].Amount = value;

        if (Resources[type].MaxValue > 0)
        {
            Resources[type].Amount = Mathf.Min(Resources[type].Amount, Resources[type].MaxValue);
        }
    }

    public bool Consume(Resource resource)
    {
        return Consume(resource.Type, resource.Amount);
    }

    public bool Consume(Units type, BigNum amount)
    {
        if (DollarBuys) { amount = 1; }

        if (Resources[type].Amount >= amount)
        {
            Resources[type].Amount -= amount;

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
            if (Resources[cost.Type].Amount < cost.Amount)
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
        Resources[type].Amount = Resources[type].Amount + amount;

        if (Resources[type].MaxValue > 0)
        {
            Resources[type].Amount = Mathf.Min(Resources[type].Amount, Resources[type].MaxValue);
        }
    }

    protected void FixedUpdate()
    {
        Tick();
    }

    protected void Tick()
    {
        Time.Tick();
        Life.Tick();
        Job.Tick();
        MacGuffinQuest.Tick();
        Influencer.Tick();
        UpgradeManager.Tick();

        TickCount++;
    }
}
