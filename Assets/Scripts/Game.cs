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
        }

        protected void FixedUpdate()
        {
            LootBoxModel.Instance.Tick();
        }
    }
}
