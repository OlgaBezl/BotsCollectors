using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private StartButton _startButton;
    [SerializeField] private ResetButton _resetButton;
    [SerializeField] private CrystalGenerator _crystalGenerator;
    [SerializeField] private BaseGenerator _baseGenerator;
    [SerializeField] private BaseCounter _baseCounter;

    private void Start()
    {
        _startButton.Click += OnClick;
        _resetButton.Click += OnClick;
    }

    private void OnDisable()
    {
        _startButton.Click -= OnClick;
        _resetButton.Click -= OnClick;
    }

    private void OnClick()
    {
        _baseCounter.Reset();

        _baseGenerator.StartWork();
        _crystalGenerator.StartWork();
    }
}
