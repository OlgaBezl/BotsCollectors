using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private CrystalGenerator _crystalGenerator; 
    [SerializeField] private Base _base; 
    [SerializeField] private ResetButton _resetButton;

    private void OnEnable()
    {
        _score.Finished += OnFinished;
    }

    private void OnFinished()
    {
        _score.Finished -= OnFinished;
        _resetButton.Active();
        _crystalGenerator.Reset();
        _base.Reset();
    }
}
