using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    private bool battle = false;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeBattleState);
    }

    public void ChangeBattleState()
    {
        battle = true;
        Debug.Log("Battle started!");
        // Add your battle logic here
    }
}
