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
    public bool UnlockAllViews = false; //DEBUG
    public float StartWithCash = 0;
#endif

    public long TickCount { get; protected set; }

    public UpgradeManager UpgradeManager    { get; protected set; }
    public TimeModel Time                   { get; protected set; }
    public LifeModel Life                   { get; protected set; }
    public JobModel Job                     { get; protected set; }
    public MacGuffinQuest MacGuffinQuest    { get; protected set; }
    public InfluencerModel Influencer       { get; protected set; }
    public StudioModel Studio               { get; protected set; }
    public PublicModel Public               { get; protected set; }

    //Short-hand
    //Life
    public BigNum Money             { get { return Resources[Units.Money].Amount; } }
    public BigNum TotalMoneyEarned  { get { return Resources[Units.TotalMoneyEarned].Amount; } }

    //Job

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


    //Studio
    public BigNum Developers        { get { return Resources[Units.Developer].Amount; } }
    public BigNum DevHours          { get { return Resources[Units.DevHour].Amount; } }
    public BigNum ReleasedGames     { get { return Resources[Units.ReleasedGame].Amount; } }
    public BigNum Hype              { get { return Resources[Units.Hype].Amount; } }
    public BigNum CopiesSold        { get { return Resources[Units.CopySold].Amount; } }
    public BigNum DataAnalysts      { get { return Resources[Units.DataAnalyst].Amount; } }
    public BigNum CustomerData      { get { return Resources[Units.CustomerData].Amount; } }
    public BigNum ActivePlayers     { get { return Resources[Units.ActivePlayer].Amount; } }


    //Public
    public BigNum Customers         { get { return Resources[Units.Customer].Amount; } }
    public BigNum Marketers         { get { return Resources[Units.Marketer].Amount; } }
    public BigNum Lobbyists         { get { return Resources[Units.Lobbyist].Amount; } }
    public BigNum Favor             { get { return Resources[Units.Favor].Amount; } }
    public BigNum CPUs              { get { return Resources[Units.CPU].Amount; } }
    public BigNum Cycles            { get { return Resources[Units.Cycle].Amount; } }
    public BigNum Bioengineers      { get { return Resources[Units.Bioengineer].Amount; } }
    public BigNum GenomeData        { get { return Resources[Units.GenomeData].Amount; } }


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

        Studio = new StudioModel(this);
        Studio.IsActive = false;

        Public = new PublicModel(this);
        Public.IsActive = false;

        SetInitialState();

        if (UnlockAllViews)
        {
            Life.IsActive = true;
            Job.IsActive = true;
            MacGuffinQuest.IsActive = true;
            Influencer.IsActive = true;
            Studio.IsActive = true;
            Public.IsActive = true;
        }
    }

    protected void SetInitialState()
    {
        Resources[Units.GrindProgress].MaxValue = 100;
        Resources[Units.VideoProgress].MaxValue = 100;

#if DEBUG
        Resources[Units.Money].Amount = StartWithCash;
#endif
    }

    private void SetResource(Units type, BigNum value)
    {
        Resources[type].Amount = value;

        if (Resources[type].MaxValue > 0)
        {
            Resources[type].Amount = Mathf.Min(Resources[type].Amount, Resources[type].MaxValue);
        }
    }

    public bool ConsumeExactly(Resource resource)
    {
        return ConsumeExactly(resource.Type, resource.Amount);
    }

    /// <summary>
    /// Consume as much of this unit as possible, up to amount.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public BigNum ConsumeUpTo(Units type, BigNum amount)
    {
        BigNum amountConsumed = Mathf.Min(Resources[type].Amount, amount);

        Resources[type].Amount -= amountConsumed;

        return amountConsumed;
    }

    public bool ConsumeExactly(Units type, BigNum amount)
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

    public bool ConsumeExactly(List<Resource> costs)
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
                ConsumeExactly(costs[i]);
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

        if (type == Units.Money)
        {
            Resources[Units.TotalMoneyEarned].Amount += amount; //Keep Track of the total cash
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
        Studio.Tick();
        Public.Tick();
        UpgradeManager.Tick();

        TickCount++;
    }
}
