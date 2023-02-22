using Drones;
using UnityEngine;
using UnityEngine.UI;

public class DroneComponentOutfitter : MonoBehaviour
{
    [SerializeField] private GameObject componentPrefab;
    [SerializeField] private DroneAttachmentSlot droneAttachmentSlot;
    private Button _button; 

    private void OnEnable() => _button.onClick.AddListener(DecorateAttachmentPoint);
    private void OnDisable() => _button.onClick.RemoveListener(DecorateAttachmentPoint);
    private void Awake() => _button = GetComponent<Button>();

    private void DecorateAttachmentPoint()
    {
        if (!droneAttachmentSlot.GetDrone().GetAttachmentPoints()[0].HasAttachment) // or if attachment is a different component
        {
            DroneAttachment droneAttachment = Instantiate(componentPrefab).GetComponent<DroneAttachment>();
            AttachmentPoint attachmentPoint = droneAttachmentSlot.GetAttachmentPoint();
            droneAttachmentSlot.GetDrone().Decorate(droneAttachment, attachmentPoint);
            
            /*ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = new Color(1, 0, 0, 0.02f);
            colorBlock.highlightedColor = GetComponent<Button>().colors.highlightedColor;
            colorBlock.pressedColor = GetComponent<Button>().colors.pressedColor;
            colorBlock.selectedColor = new Color(1, 0, 0, 0.02f);
            colorBlock.disabledColor = GetComponent<Button>().colors.disabledColor;
            colorBlock.colorMultiplier = GetComponent<Button>().colors.colorMultiplier;
            colorBlock.fadeDuration = GetComponent<Button>().colors.fadeDuration;
            GetComponent<Button>().colors = colorBlock;*/
        }
    }
}
