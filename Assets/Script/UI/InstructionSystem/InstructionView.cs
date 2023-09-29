using System.Collections;
using TMPro;
using UnityEngine;

namespace InstructionSystem
{
    public class InstructionView : MonoBehaviour
    {
        [SerializeField] private InstructionScriptableObject interactableItemInstruction;
        [SerializeField] private InstructionScriptableObject interactableAreaInstruction;

        [Header("Instruction Popup")]
        [SerializeField] private GameObject instructionPopup;
        [SerializeField] private TextMeshProUGUI instructionsText;

        private Coroutine instructionCoroutine;

        private void Awake()
        {
            instructionPopup.SetActive(false);
        }
        private void Start()
        {
            hideInstructionPopup();
        }
        public void ShowInstruction(InstructionType type, GameObject interactedObject)
        {
            stopCoroutine(instructionCoroutine);
            switch (type)
            {
                case InstructionType.InteractableItem:
                    instructionCoroutine = StartCoroutine(setInstructions(interactableItemInstruction, interactedObject));
                    break;
                case InstructionType.InteractableArea:
                    instructionCoroutine = StartCoroutine(setInstructions(interactableAreaInstruction, interactedObject));
                    break;
            }
        }

        public void HideInstruction() => hideInstructionPopup();

        private IEnumerator setInstructions(InstructionScriptableObject instruction, GameObject interactedObject)
        {
            if(interactedObject != null)
            {
                showInstructionPopup(instruction, interactedObject);
            }
            

            yield return new WaitForSeconds(instruction.DisplayDuration);
            hideInstructionPopup();
        }

        private void hideInstructionPopup()
        {
            instructionsText.SetText(string.Empty);
            if(instructionPopup != null)
                instructionPopup.SetActive(false);
            stopCoroutine(instructionCoroutine);
        }

        private void showInstructionPopup(InstructionScriptableObject instruction, GameObject interactedObject)
        {
            instructionsText.SetText(instruction.StartInstruction + " " + interactedObject.name + " " + instruction.EndInstruction);
            instructionPopup.SetActive(true);
        }

        private void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}