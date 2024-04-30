using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MaxScoreView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private BaseCounter _baseCounter;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = " / " + _baseCounter.MaxValue.ToString();
    }
}
