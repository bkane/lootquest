using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public static Game Instance;

        public ViewManager ViewManager;

        private void Awake()
        {
            if (Instance != null) { Debug.LogError("Tried to instantiate a second Game!"); }
            Instance = this;
            new LootBoxModel();
        }

        private void Start()
        {
            Time.fixedDeltaTime = 1 / 30f;

            //TODO: auto-load saved game
            StartCoroutine(NewGameRoutine());
        }

        protected IEnumerator NewGameRoutine()
        {
            float delay = 3;

#if DEBUG
            delay = 0.01f;
#endif

            yield return new WaitForSeconds(delay);
            Logger.Log("FOR IMMEDIATE RELEASE - <i>MacGuffin Quest 2: The Quest for MacGuffin</i> ships next week!");
            yield return new WaitForSeconds(delay);
            Logger.Log("Pre-orders for the hotly anticipated <i>MacGuffin Quest 2: The Quest for MacGuffin</i> are now available!");
            LootBoxModel.Instance.UpgradeManager.Unlock(Model.Upgrade.EUpgradeType.PurchaseMacGuffinQuest);
            yield return new WaitForSeconds(delay);
            Logger.Log("I NEED this game. Time to get a job.");
            yield return new WaitForSeconds(delay);
            LootBoxModel.Instance.UpgradeManager.Unlock(Upgrade.EUpgradeType.GetJob);
        }

        protected void FixedUpdate()
        {
            LootBoxModel.Instance.Tick();
        }

        public void OnAllEarthMonetized()
        {
            StartCoroutine(OnAllEarthMonetizedRoutine());
        }

        protected IEnumerator OnAllEarthMonetizedRoutine()
        {
            float delay = 5f;
            Logger.Log("All people of Earth are now monetized.");
            yield return new WaitForSeconds(delay);

            Logger.Log("There are no more wars.");
            yield return new WaitForSeconds(delay);

            Logger.Log("There is no more happiness.");
            yield return new WaitForSeconds(delay);

            Logger.Log("There are only loot boxes.");
            yield return new WaitForSeconds(delay);

            Logger.Log("The <i>MacGuffin Quest 2</i> developers have long since ceased operations.");
            yield return new WaitForSeconds(delay);

            Logger.Log("You send an expedition team to the frozen wasteland of Canada and with remarkable luck, they find a USB key in the ruins.");
            yield return new WaitForSeconds(delay);

            Logger.Log("Your long journey may now come to a close.");
            yield return new WaitForSeconds(delay);

            LootBoxModel.Instance.UpgradeManager.Unlock(Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode);
        }
    }
}
