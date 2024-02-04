using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Volume : MonoBehaviour
{
    private TextMeshProUGUI txt;
    [SerializeField] private string volumeName;
    [SerializeField] private string volumeDescription;

    void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = volumeDescription + volumeValue.ToString();
    }


}
