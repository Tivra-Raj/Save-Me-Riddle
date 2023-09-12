using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        [Header("Examine Parameters")]
        public GameObject examineWindow;
        public Image examineImage;
        public TextMeshProUGUI examineText;


        [Header("Item Showcase")]
        public TextMeshProUGUI countText;

    }
}