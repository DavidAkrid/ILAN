/*
Yarn Spinner is licensed to you under the terms found in the file LICENSE.md.
*/

using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Markup;
using Yarn.Unity.Attributes;

using TMPro;

#nullable enable

namespace Yarn.Unity
{
    public class CustomLinePresenterButtonHandler : ActionMarkupHandler
    {
        [SerializeField] DialogueRunner? dialogueRunner;
        [SerializeField] GameObject? arrowIndicator;
        [SerializeField] private float typingSoundCooldown = 2f;

        private bool lineComplete = false;
        private bool isShowingLine = false;
        private float lastTypingSoundTime = -1f;

        void Update()
        {
            if (!isShowingLine) return;
            if (!Input.GetMouseButtonDown(0)) return;

            if (!lineComplete)
            {
                dialogueRunner?.RequestHurryUpLine();
            }
            else
            {
                AudioManager.PlaySound("NextMessage", Random.Range(0.95f, 1.05f), Random.Range(0.8f, 1f));
                dialogueRunner?.RequestNextLine();
            }
        }

        public override void OnPrepareForLine(MarkupParseResult line, TMP_Text text)
        {
            lineComplete = false;
            isShowingLine = true;
            if (arrowIndicator != null) arrowIndicator.SetActive(false);
        }

        public override void OnLineDisplayBegin(MarkupParseResult line, TMP_Text text)
        {
            return;
        }

        public override YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken)
        {
            if (Time.time - lastTypingSoundTime >= typingSoundCooldown)
            {
                AudioManager.PlaySound("KeyPressText", Random.Range(0.85f, 1.15f), Random.Range(0.7f, 1f));
                lastTypingSoundTime = Time.time;
            }
            return YarnTask.CompletedTask;
        }

        public override void OnLineDisplayComplete()
        {
            lineComplete = true;
            if (arrowIndicator != null) arrowIndicator.SetActive(true);
        }

        public override void OnLineWillDismiss()
        {
            isShowingLine = false;
            if (arrowIndicator != null) arrowIndicator.SetActive(false);
        }
    }
}
