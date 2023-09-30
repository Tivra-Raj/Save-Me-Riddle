using InteractionSystems;
using UnityEngine;

namespace Events
{
    public class EventService
    {
        private static EventService instance;
        public static EventService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventService();
                }
                return instance;
            }
        }

        public EventController<GameObject> OnKeyPickedUpEvent { get; private set; }

        public EventController<ItemScriptableObject> OnNoteExamineEvent { get; private set; }

        public EventController OnPlayerEscapeEvent { get; private set; }

        public EventController OnPlayerOpenDoorEvent { get; private set; }

        public EventController OnGhostSpawnedEvent { get; private set; }

        public EventController OnGhostDeSpawnedEvent { get; private set; }


        public EventService()
        {
            OnKeyPickedUpEvent = new EventController<GameObject>();
            OnNoteExamineEvent = new EventController<ItemScriptableObject>();
            OnPlayerEscapeEvent = new EventController();
            OnPlayerOpenDoorEvent = new EventController();
            OnGhostSpawnedEvent = new EventController();
            OnGhostDeSpawnedEvent = new EventController();
        }
    }
}