using EHT.Narrative;
using Ink.UnityIntegration;
using UnityEditor;
using UnityEngine;

namespace EHT
{
    [CustomEditor(typeof(NarrativeSystem))]
    public class NarrativeSystemEditor : Editor
    {
        private const bool focusStoryWindow = false;
        
        private bool storyExpanded;
        
        static NarrativeSystemEditor()
        {
            NarrativeSystem.OnCreateStory += story =>
            {
                var window = InkPlayerWindow.GetWindow(focusStoryWindow);
                if(!window) { return; }
                InkPlayerWindow.Attach(story);
            };
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var story = ((NarrativeSystem)target).Story;
            InkPlayerWindow.DrawStoryPropertyField(story, ref storyExpanded, new GUIContent("Story"));
        }
    }
}
