using UnityEngine;

namespace InstructionSystem
{
    [CreateAssetMenu(fileName = "InstructionSciprtableObject", menuName = "ScriptableObject/InstructionSciprtableObject")]
    public class InstructionScriptableObject : ScriptableObject
    {
        public InstructionType InstructionType;
        public string StartInstruction;
        public string EndInstruction;
        public int DisplayDuration;
    }
}