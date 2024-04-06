using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrentScoreView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private Score _score;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _score.Changed += UpdateView;
        UpdateView(_score.CurrentValue);
    }

    private void OnDisable()
    {
        _score.Changed -= UpdateView;
    }

    private void UpdateView(int currentScore)
    {
        _text.text = currentScore.ToString();
    }
}
