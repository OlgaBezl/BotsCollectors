using System.Collections.Generic;
using UnityEngine;

public class UICounterGenerator : MonoBehaviour
{
    [SerializeField] private CurrentScoreView _scoreViewPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private Camera _camera;

    private List<CurrentScoreView> _views;

    private void Awake()
    {
        _views = new List<CurrentScoreView>();
    }

    public void Generate(Vector3 sceneElementPosition, Counter counter)
    {
        CurrentScoreView scoreView = Instantiate(_scoreViewPrefab, transform);
        scoreView.SetParameters(_canvasRectTransform, _camera, sceneElementPosition, counter);

        _views.Add(scoreView);
    }

    public void Reset()
    {
        foreach (CurrentScoreView view in _views)
        {
            Destroy(view.gameObject);
        }

        _views.Clear();
    }
}
