using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Debug view controller that will generate labels and buttons for all unit types
    /// </summary>
    public class DebugViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        public GameObject LayoutParent;
        public TextMeshProUGUI TextPrefab;
        public Button ButtonPrefab;

        protected Dictionary<Units, TextMeshProUGUI> labels;

        private void Awake()
        {
            GenerateUI();
        }

        protected void GenerateUI()
        {
            List<Units> unitTypes = Enum.GetValues(typeof(Units)).Cast<Units>().ToList();

            //Labels
            labels = new Dictionary<Units, TextMeshProUGUI>();
            for (int i = 0; i < unitTypes.Count; i++)
            {
                Units type = unitTypes[i];
                TextMeshProUGUI label = Instantiate(TextPrefab, LayoutParent.transform);
                label.name = "label_" + type;
                label.text = string.Format("{0}: {1}", type, Model.Resources[type].value);
                labels.Add(type, label);
            }

            //Buttons
            for (int i = 0; i < unitTypes.Count; i++)
            {
                Units type = unitTypes[i];
                Button button = Instantiate(ButtonPrefab, LayoutParent.transform);
                button.name = "button_" + type;
                button.GetComponentInChildren<Text>().text = string.Format("Add {0}", type);
                button.onClick.AddListener(() => Model.Add(type, 1));
            }
        }

        private void Update()
        {
            foreach(var kvp in labels)
            {
                Units type = kvp.Key;
                kvp.Value.text = string.Format("{0}: {1}", type, Model.Resources[type].value);
            }
        }
    }
}
