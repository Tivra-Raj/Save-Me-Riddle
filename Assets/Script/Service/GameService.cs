using Utilities;
using UnityEngine;
using UI;
using Sound;
using InstructionSystem;

namespace Service
{
    public class GameService : MonoSingletonGeneric<GameService>
    {
        [SerializeField] private GameUIView gameUIView;
        [SerializeField] private SoundView soundView;
        [SerializeField] private InstructionView instructionView;

        public GameUIView GetGameUIView() => gameUIView;

        public InstructionView GetInstructionView() => instructionView;

        public SoundView GetSoundView() => soundView;
    }
}