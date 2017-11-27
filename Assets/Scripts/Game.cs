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

            Util.Load();

            if (LootBoxModel.Instance.UpgradeManager.UpgradeStates[Upgrade.EUpgradeType.PurchaseMacGuffinQuest] == Upgrade.EState.Hidden)
            {
                StartCoroutine(NewGameRoutine());
            }
        }

        protected IEnumerator NewGameRoutine()
        {
            float delay = 3;

#if DEBUG
            //delay = 0.01f;
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

        public void OpenOptionsPanel()
        {
            ViewManager.OptionsPanel.SetActive(true);
            UnityEngine.Time.timeScale = 0;
        }

        public void CloseOptionsPanel()
        {
            ViewManager.OptionsPanel.SetActive(false);
            UnityEngine.Time.timeScale = 1;
        }

        public void SaveAndQuit()
        {
            Debug.Log("SaveAndQuit");
            Util.Save();
            Quit();
        }

        public void Quit()
        { 
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void ResetProgress()
        {
            Debug.Log("ResetProgress");
            //TODO: confirmation?
            new LootBoxModel();
            Logger.Instance.Clear();
            StartCoroutine(NewGameRoutine());
        }

        public void OnAllEarthMonetized()
        {
            if (LootBoxModel.Instance.UpgradeManager.UpgradeStates[Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode] == Upgrade.EState.Hidden)
            {
                StartCoroutine(OnAllEarthMonetizedRoutine());
            }
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

        public void OnMacGuffinQuestSourcePurchase()
        {
            LootBoxModel.Instance.Life.IsActive = false;
            LootBoxModel.Instance.Job.IsActive = false;
            LootBoxModel.Instance.Influencer.IsActive = false;
            LootBoxModel.Instance.Studio.IsActive = false;
            LootBoxModel.Instance.Public.IsActive = false;
            LootBoxModel.Instance.UpgradeManager.IsActive = false;

            ViewManager.OptionsUI.SetActive(false);

            LootBoxModel.Instance.MacGuffinQuest.DoEndGame();
        }

        public void OnMacGuffinQuestFinished()
        {
            StartCoroutine(OnGameCompleteRoutine());
        }

        protected IEnumerator OnGameCompleteRoutine()
        {
            float delay = 5f;

            LootBoxModel.Instance.MacGuffinQuest.IsActive = false;

            yield return new WaitForSeconds(delay);

            Logger.Log("I'd give it a 7/10.");
            yield return new WaitForSeconds(delay);

            Logger.Log("...");
            yield return new WaitForSeconds(delay);

            Logger.Log("I wonder when the next one comes out.");
            yield return new WaitForSeconds(delay);

            Logger.Log("Oh right, the entire industry collapsed.");
            yield return new WaitForSeconds(delay);

            Logger.Log("....");
            yield return new WaitForSeconds(delay);

            Logger.Log("Maybe I should go outside.");
            yield return new WaitForSeconds(delay * 2);

            Logger.Instance.gameObject.SetActive(false);
            ViewManager.EndGameView.SetActive(true);
        }

        public void EndGame()
        {
            Quit();
        }
    }
}
