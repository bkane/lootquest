﻿using Assets.Scripts.Model;
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
            Debug.LogFormat("Application Version: {0}", Application.version);
            Debug.LogFormat("Resolution: {0}", Screen.currentResolution);
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
                SteamManager.CheckAchievements();
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
            Logger.Instance.gameObject.SetActive(true);
            ViewManager.EndGameView.SetActive(false);

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
            //Remove the title screen
            ViewManager.TitleScreen.SetActive(false);

            //If we're not resuming a game that at least got to the Job phase, start up the new game sequence
            if (LootBoxModel.Instance.UpgradeManager.UpgradeStates[Upgrade.EUpgradeType.GetJob] == Upgrade.EState.Hidden)
            {
                StartCoroutine(NewGameRoutine());
            }

            //If the game has already been finished, shortcut to the end-game "Go Outside" state
            if (LootBoxModel.Instance.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode))
            {
                LootBoxModel.Instance.Life.IsActive = false;
                LootBoxModel.Instance.Job.IsActive = false;
                LootBoxModel.Instance.Influencer.IsActive = false;
                LootBoxModel.Instance.Studio.IsActive = false;
                LootBoxModel.Instance.Public.IsActive = false;
                LootBoxModel.Instance.UpgradeManager.IsActive = false;
                Logger.Instance.gameObject.SetActive(false);
                ViewManager.EndGameView.SetActive(true);
                SteamManager.UnlockAchievement(SteamManager.ACH_ACCOMPLISHMENT);
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

            //ViewManager.OptionsUI.SetActive(false);

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

            SteamManager.UnlockAchievement(SteamManager.ACH_ACCOMPLISHMENT);

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
            SaveAndQuit();
        }
    }
}
