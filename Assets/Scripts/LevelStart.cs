using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private StartButton _startButton;
    [SerializeField] private ResetButton _resetButton;
    [SerializeField] private Base _base;
    [SerializeField] private CrystalGenerator _crystalGenerator;
    [SerializeField] private Score _score;

    private void Start()
    {
        _startButton.Click += OnClick;
        _resetButton.Click += OnClick;
    }

    private void OnDisable()
    {
        _resetButton.Click -= OnClick;
    }

    private void OnClick()
    {
        _startButton.Click -= OnClick;
        _score.Reset();
        _base.StartLevel();
        _crystalGenerator.StartGeneration();
    }
}
