using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; //sound: or Music:
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

     private void UpdateVolume()
    {   
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volumeValue. ToString();
    }
}
