using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Upgrade
    {
        public enum EUpgradeType
        {
            EnergyDrinks,
            SleepApp,
            AutoGrinder,
            AutoSellTrashItems,
            //pc upgrades, water cooling, gpu sli, etc
        }

        public enum EState
        {
            Hidden,
            Visible,
            Purchased
        }

        public EUpgradeType Type;
        public string Name;
        public string Description;
        public List<Resource> Costs;
        public EState State;
    }
}
