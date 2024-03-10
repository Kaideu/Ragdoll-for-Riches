using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kaideu.UI
{
    public class UIHandler : Utils.SingletonPattern<UIHandler>
    {
        [Serializable]
        public struct UI
        {
            public string name;
            public Canvas canvasObject;
            [Min(0)][Tooltip("Lower Number = Higher Priority. Prevents show previous from going back to lower priority UI")]
            public int priorityLevel;
        }

        [SerializeField] List<UI> myUIList = new List<UI>();
        
        private List<UI> previousUI = new List<UI>();
        private List<UI> currentUI = new List<UI>();

        protected override void Awake()
        {
            base.Awake();

            foreach (UI ui in myUIList)
                ui.canvasObject.enabled = false;
        }

        public void ShowUI(params string[] name)
        {
            currentUI.Clear();
            previousUI.Clear();
            bool uiFound = false;

            foreach (UI ui in myUIList)
            {
                if (Array.Exists(name, x => x == ui.name))
                {
                    ui.canvasObject.enabled = true;
                    name[Array.IndexOf(name, ui.name)] = null;
                    uiFound = true;
                    currentUI.Add(ui);
                    continue;
                }
                else
                {
                    if (ui.canvasObject.enabled)
                        previousUI.Add(ui);
                    ui.canvasObject.enabled = false;
                }
            }

            foreach (string n in name)
            {
                if (n == null)
                    continue;
                Debug.LogError(n + " not found in UI names");
            }

            if (!uiFound)
                ForcePrevious();

            Debug.LogWarning($"UI Shown: {name}");

        }

        public void TryShowPrevious()
        {
            if (previousUI.Count < 1)
                return;

            int highestPriority = 0;
            List<UI> temp = new List<UI>();

            foreach (UI ui in currentUI)
            {
                if (highestPriority > ui.priorityLevel)
                    highestPriority = ui.priorityLevel;
            }

            foreach (UI ui in previousUI)
            {
                if (ui.priorityLevel <= highestPriority)
                {
                    temp.Add(ui);
                }
                else
                    Debug.LogError($"Priority issue with {ui.name} UI");
            }

            if (temp.Count == previousUI.Count)
            {
                foreach (UI ui in previousUI)
                    ui.canvasObject.enabled = true;
                foreach (UI cui in currentUI)
                    cui.canvasObject.enabled = false;

                temp = previousUI;
                previousUI = currentUI;
                currentUI = temp;
            }
        }

        private void ForcePrevious()
        {
            List<UI> temp = new List<UI>();

            foreach (UI ui in previousUI)
                ui.canvasObject.enabled = true;
            foreach (UI cui in currentUI)
                cui.canvasObject.enabled = false;

            temp = previousUI;
            previousUI = currentUI;
            currentUI = temp;
        }
    }
}
