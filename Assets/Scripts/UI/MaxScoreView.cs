using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MaxScoreView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private Score _score;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = " / " + _score.MaxValue.ToString();
    }
}
