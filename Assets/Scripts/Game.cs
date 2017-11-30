using Assets.Scripts.Model;
using Assets.Scripts.ViewControllers;
using DarkTonic.MasterAudio;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public static Game Instance;

#if DEBUG
        public bool UnlockAll;
#endif

        public SteamManager SteamManager;
        public ViewManager ViewManager;
        public PlayerSettings Settings;

        private void Awake()
        {
            if (Instance != null) { Debug.LogError("Tried to instantiate a second Game!"); }
            Instance = this;
            new LootBoxModel();
        }

        private void Start()
        {
            Time.fixedDeltaTime = 1 / 30f;

            Settings = new PlayerSettings();
            bool foundSettings = Util.LoadIntoObject(Util.OPTIONS_FILENAME, Settings);

            if (!foundSettings)
            {
                SaveSettings();
            }

            UpdateMusicSettings();

            bool foundSave = Util.LoadIntoObject(Util.SAVE_FILENAME, LootBoxModel.Instance);

            if (foundSave)
            {
                ViewManager.TitleScreen.GetComponent<TitleScreen>().StartGame.GetComponentInChildren<TextMeshProUGUI>().text = "Resume Game";
            }

            StartCoroutine(AutoSaveRoutine());

            if (!foundSave)
            {
                StartCoroutine(GGGameDevRoutine());
            }

#if DEBUG
            if (UnlockAll)
            {
                foreach(var kvp in LootBoxModel.Instance.UpgradeManager.Upgrades)
                {
                    LootBoxModel.Instance.UpgradeManager.Unlock(kvp.Key);
                }
            }
#endif
        }

        public void UpdateMusicSettings()
        {
            if (Settings.UseSteamMusic && SteamManager.Client.IsValid)
            {
                SteamManager.Client.Music.Play();
                MasterAudio.OnlyPlaylistController.StopPlaylist();
            }
            else
            {
                if (SteamManager.Client.IsValid)
                {
                    SteamManager.Client.Music.Pause();
                }

                MasterAudio.OnlyPlaylistController.StartPlaylist("Default");
            }
        }

        protected IEnumerator GGGameDevRoutine()
        {
            float time = 60 * 90;

#if DEBUG
            time = 10;
#endif
            yield return new WaitForSecondsRealtime(time);

            Logger.Log("Hey, it's been about 90 mins since you started playing. If you wanted a refund, you might still be eligible. If you're having fun, that's great! Thanks and enjoy! -Ben");
        }

        protected IEnumerator AutoSaveRoutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(30);

                if (Settings.AutoSave)
                {
                    Debug.Log("Autosaving");
                    Util.Save(Util.SAVE_FILENAME, LootBoxModel.Instance);
                }
            }
        }

        protected void SaveSettings()
        {
            Util.Save(Util.OPTIONS_FILENAME, Settings);
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
            if (!ViewManager.TitleScreen.activeSelf)
            {
                LootBoxModel.Instance.Tick();
            }
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
            SaveSettings();
        }

        public void OpenRestartPopup()
        {
            ViewManager.ConfirmRestartPopup.SetActive(true);
        }

        public void CloseRestartPopup()
        {
            ViewManager.ConfirmRestartPopup.SetActive(false);
        }

        public void SaveAndQuit()
        {
            Debug.Log("SaveAndQuit");
            Util.Save(Util.SAVE_FILENAME, LootBoxModel.Instance);
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

        public void StartGameClicked()
        {
            ViewManager.TitleScreen.SetActive(false);

            if (LootBoxModel.Instance.UpgradeManager.UpgradeStates[Upgrade.EUpgradeType.GetJob] == Upgrade.EState.Hidden)
            {
                StartCoroutine(NewGameRoutine());
            }
        }

        public void QuitGameClicked()
        {
            Quit();
        }

        public void ResetProgress()
        {
            Debug.Log("ResetProgress");
            CloseRestartPopup();
            CloseOptionsPanel();
            new LootBoxModel();
            Logger.Instance.Clear();
            ViewManager.UpgradeView.GetComponent<UpgradeViewController>().Reset();
            ViewManager.TitleScreen.SetActive(true);
            ViewManager.TitleScreen.GetComponent<TitleScreen>().StartGame.GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
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
            SteamManager.UnlockAchievement(SteamManager.ACH_ACCOMPLISHMENT);

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
