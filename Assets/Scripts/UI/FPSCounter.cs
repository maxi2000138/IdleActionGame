using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fps;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        _text.text = ((int)fps).ToString();
    }
}
