using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private CrystalGenerator _crystalGenerator; 
    [SerializeField] private BaseGenerator _baseGenerator; 
    [SerializeField] private ResetButton _resetButton;

    private void OnEnable()
    {
        _baseCounter.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _baseCounter.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        _resetButton.Active();
        _crystalGenerator.Reset();
        _baseGenerator.Reset();
    }
}
