using System.Collections;
using TMPro;
using UnityEngine;

namespace EHT.Narrative.Reader
{
    [RequireComponent(typeof(TMP_Text))]
    public class Typewriter : MonoBehaviour
    {
        private TMP_Text textMesh;
        private string currentText;

        [SerializeField] private string cursor = "|";
        [SerializeField] private bool showCursor;

        [SerializeField] private bool blinkCursor;
        private bool blinkCursorState;
        [SerializeField] private float cursorBlinkInterval = 1.0f;
        private float cursorBlinkTimer = 0.0f;

        [SerializeField] private WritingProfile writingProfile;

        public string CurrentText
        {
            get => currentText;
            set
            {
                currentText = value;
                UpdateTextMesh();
            }
        }

        public bool Busy { get; private set; }

        public string Cursor => ShowCursor ? cursor : "";

        public bool ShowCursor
        {
            get => (!BlinkCursor && showCursor) || (BlinkCursor && blinkCursorState);
            set
            {
                showCursor = value;
                UpdateTextMesh();
            }
        }

        public bool BlinkCursor
        {
            get => blinkCursor;
            set
            {
                blinkCursor = value;
                blinkCursorState = true;
                cursorBlinkTimer = 0.0f;
                UpdateTextMesh();
            }
        }

        private void Awake()
        {
            textMesh = GetComponent<TMP_Text>();
            CurrentText = "";
        }

        private void Update()
        {
            if(!BlinkCursor) { return; }

            cursorBlinkTimer += Time.deltaTime;
            if(cursorBlinkTimer < cursorBlinkInterval) { return; }
            cursorBlinkTimer -= cursorBlinkInterval;

            blinkCursorState = !blinkCursorState;
            UpdateTextMesh();
        }

        public void WriteLine(string line, bool instant = false)
        {
            Busy = true;
            if(!instant && writingProfile && writingProfile.UseIntervals)
            {
                StartCoroutine(TypewriteLine(line));
                return;
            }
            CurrentText += line;
            Busy = false;
        }

        public void NewLine()
        {
            if(CurrentText.Length > 0) { CurrentText += "\n"; }
        }

        public void ClearLines()
        {
            CurrentText = "";
        }

        private IEnumerator TypewriteLine(string line)
        {
            var text = CurrentText;

            var wasSpace = false;
            var isCommand = false;
            var wasCommand = false;
            for(var i = 0; i < line.Length; i++)
            {
                if (line[i] == '<')
                {
                    isCommand = true;
                }
                else if (line[i] == '>')
                {
                    isCommand = false;
                }

                var interval = 0.0f;
                if (!(isCommand || wasCommand))
                {
                    interval = writingProfile.UseLetterInterval ? writingProfile.LetterInterval : 0.0f;
                    if(line[i] == ' ' && writingProfile.UseWordInterval)
                    {
                        interval = wasSpace ? 0.0f : writingProfile.WordInterval;
                        wasSpace = true;
                    }
                    else
                    {
                        if(char.IsPunctuation(line[i]) && writingProfile.UsePunctuationInterval)
                        {
                            interval = writingProfile.PunctuationInterval;
                        }
                        wasSpace = false;
                    }
                }
                wasCommand = isCommand;
                
                if (interval < 0.001f) { continue; }
                
                CurrentText = $"{text}{line[..i]}<alpha=#00>{line[i..]}";
                yield return new WaitForSeconds(interval);
            }

            CurrentText = text + line;
            Busy = false;
        }

        private void UpdateTextMesh()
        {
            textMesh.text = $"{CurrentText}{Cursor}";
        }
    }
}
