using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
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
            LootBoxModel.Instance.Job.IsActive = true;
        }

        protected void FixedUpdate()
        {
            LootBoxModel.Instance.Tick();
        }
    }
}
