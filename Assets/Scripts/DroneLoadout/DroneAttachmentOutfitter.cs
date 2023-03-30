using DroneLoadout.Decorators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DroneLoadout
{
    /// <summary>
    /// Responsible for decorating an attachment point with a drone attachment via a button.
    /// </summary>
    public class DroneAttachmentOutfitter : MonoBehaviour
    {
        //public event Action<DroneAttachmentOutfitter> OnAttachmentSelected;
        [SerializeField] private GameObject componentPrefab;
        //[SerializeField] private DroneAttachmentData droneAttachmentData;
        [SerializeField] private DroneAttachmentSlot droneAttachmentSlot;
        private Button _button;
        private TMP_Text _text;
        private ColorBlock _highlightColourBlock;
        private ColorBlock _unhighlightColourBlock;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
        private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);

        private void Start()
        {
            _highlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 1, 0.6f, 0.75f),
                highlightedColor = _button.colors.highlightedColor,
                pressedColor = _button.colors.pressedColor,
                selectedColor = new Color(0, 1, 0.6f, 0.75f),
                disabledColor = _button.colors.disabledColor,
                colorMultiplier = 1,
                fadeDuration = 0
            };
            
            _unhighlightColourBlock = new ColorBlock
            {
                normalColor = new Color(0, 0, 0, 0.8f),
                highlightedColor = _button.colors.highlightedColor,
                pressedColor = _button.colors.pressedColor,
                selectedColor = new Color(0, 0, 0, 0.8f),
                disabledColor = _button.colors.disabledColor,
                colorMultiplier = 1,
                fadeDuration = 0
            };

            //_text.text = droneAttachmentData.attachmentName;
        }
        
        private void Update()
        {
            // TODO: Clean this
            if (!droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
            {
                Unhighlight();
            }
        }

        private void DecorateAttachmentPoint()
        {
            // I.e. Empty button
            if (componentPrefab == null)
            {
                if (droneAttachmentSlot.GetAttachmentPoint().HasAttachment)
                {
                    var currentAttachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
                    droneAttachmentSlot.GetDrone().Undecorate(currentAttachmentPoint);
                    //OnAttachmentSelected?.Invoke(this);
                }
                return;
            }

            // If attachment point is empty, decorate it. Else, destroy the newly spawned component. TODO: Clean this
            var attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            if (attachmentPoint.HasAttachment)
            {
                //OnAttachmentSelected?.Invoke(this);
                droneAttachmentSlot.GetDrone().Undecorate(attachmentPoint);
                Unhighlight();
                return;
            }

            var droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
            droneAttachmentSlot.GetDrone().Decorate(droneAttachment, attachmentPoint);
            droneAttachment.Pulsate(true);
            Highlight();
            //OnAttachmentSelected?.Invoke(this);
        }

        private void Highlight()
        {
            _button.colors = _highlightColourBlock;
            _text.color = Color.black;
        }

        private void Unhighlight()
        {
            _button.colors = _unhighlightColourBlock;
            _text.color = Color.white;
        }
    }
}
