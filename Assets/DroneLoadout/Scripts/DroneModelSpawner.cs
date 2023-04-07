using System;
using DroneLoadout.Decorators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout.Scripts
{
    /// <summary>
    /// Responsible for spawning a drone model onto the workbench.
    /// </summary>
    public class DroneModelSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnDroneModelSelected;
        
        [SerializeField] private DroneConfigData droneConfigData;
        [SerializeField] private DroneTypeSelector droneTypeSelector;
        [SerializeField] private TMP_Text droneNameText;
        [SerializeField] private TMP_Text purchasedText;
        private Button _modelButton;
        private bool _purchased;
        private ColorBlock _highlightColourBlock;
        private ColorBlock _unhighlightColourBlock;

        public void Awake()
        {
            _modelButton = GetComponent<Button>();
            purchasedText.gameObject.SetActive(false);
        }
        
        private void Start()
        {
            _highlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 0.36f, 0.02f, 0.37f),
                highlightedColor = _modelButton.colors.highlightedColor,
                pressedColor = _modelButton.colors.pressedColor,
                selectedColor = _modelButton.colors.selectedColor,
                disabledColor = _modelButton.colors.disabledColor,
                colorMultiplier = 1,
                fadeDuration = 0
            };
            
            _unhighlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 0, 0, 0.8f),
                highlightedColor = _modelButton.colors.highlightedColor,
                pressedColor = _modelButton.colors.pressedColor,
                selectedColor = _modelButton.colors.selectedColor,
                disabledColor = _modelButton.colors.disabledColor,
                colorMultiplier = 1,
                fadeDuration = 0
            };
        }

        private void OnValidate()
        {
            if (droneConfigData == null) { return;}
            droneNameText.text = droneConfigData.DroneName;
        }

        private void OnEnable() => _modelButton.onClick.AddListener(SpawnDroneModel);
        private void OnDisable() => _modelButton.onClick.RemoveListener(SpawnDroneModel);

        private void Update()
        {
            // TODO: Clean up
            if (!_purchased)
            {
                purchasedText.gameObject.SetActive(false);
                Unhighlight();
                return;
            }
            purchasedText.gameObject.SetActive(true);
            Highlight();
        }

        private void SpawnDroneModel()
        {
            droneTypeSelector.HideModelSubMenu();
            OnDroneModelSelected?.Invoke(droneConfigData.DronePrefab);
        }

        public void SetPurchased(bool purchased)
        {
            _purchased = purchased;
        }

        public bool IsPurchased() => _purchased;
        public string GetDroneModelName() => droneConfigData.DroneName;
        
        private void Highlight()
        {
            _modelButton.colors = _highlightColourBlock;
        }

        private void Unhighlight()
        {
            _modelButton.colors = _unhighlightColourBlock;
        }
    }
}