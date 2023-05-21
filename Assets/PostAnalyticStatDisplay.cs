using Testing;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class PostAnalyticStatDisplay : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        if (JsonFileHandler.CheckFileExists("PostAnalyticData"))
        {
            PostMatchAnalyticsData data = JsonFileHandler.Load<PostMatchAnalyticsData>("PostAnalyticData");
            _text.text = $"Attackers Lost: {data.attackersLost}\nDefenders Lost: {data.defendersLost}";
        }
        else
        {
            _text.text = "No Data Found!";
        }
    }
}
