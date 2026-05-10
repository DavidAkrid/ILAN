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

        private bool lineComplete = false;
        private bool isShowingLine = false;

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
