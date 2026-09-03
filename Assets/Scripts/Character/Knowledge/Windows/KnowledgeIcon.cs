using System;
using EHT.Knowledge.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace EHT
{
    public class KnowledgeIcon : MonoBehaviour
    {
        private KnowledgeWindows windows;

        private Button button;

        private int targetIndex;
        private KnowledgeWindow targetWindow;

        [SerializeField] private Image icon;
        [SerializeField] private Color openColor = Color.white;
        [SerializeField] private Color minimisedColor = Color.gray;
        [SerializeField] private Color closedColor = Color.black;

        public int TargetIndex
        {
            get => targetIndex;
            set => targetIndex = value;
        }

        private void Awake()
        {
            windows = GetComponentInParent<KnowledgeWindows>();

            button = GetComponent<Button>();

            windows.OnWindowStateChanged.AddListener(UpdateTargetWindow);

            button.onClick.AddListener(Clicked);
        }

        private void Start()
        {
            UpdateTargetWindow();
        }

        private void UpdateTargetWindow()
        {
            targetWindow?.OnShown.RemoveListener(TargetWindowShown);
            
            targetWindow = null;
            if (targetIndex >= windows.Windows.Count)
            {
                icon.color = closedColor;
                return;
            }

            targetWindow = windows.Windows[targetIndex];

            TargetWindowShown(targetWindow.IsShown);
            targetWindow?.OnShown.AddListener(TargetWindowShown);
        }

        private void TargetWindowShown(bool shown)
        {
            icon.color = shown ? openColor : minimisedColor;
        }

        private void Clicked()
        {
            if(!targetWindow) { return; }
            targetWindow.IsShown = true;
        }
    }
}
