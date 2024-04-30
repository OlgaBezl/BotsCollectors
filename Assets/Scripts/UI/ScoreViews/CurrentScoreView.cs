using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrentScoreView : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private const float _halfFactor = 0.5f;

    private TextMeshProUGUI _text;
    private RectTransform _uiElement;
    private Vector3 _sceneElementPosition;
    private RectTransform _canvasRectTransform;
    private Camera _camera;

    private void Start()
    {
        _uiElement = GetComponent<RectTransform>();
        _text = GetComponent<TextMeshProUGUI>();

        if(_counter != null)
        {
            _counter.Changed += UpdateView;
            UpdateView(_counter.CurrentValue);
        }
    }

    private void OnDisable()
    {
        _counter.Changed -= UpdateView;
    }

    public void SetParameters(RectTransform canvasRectTransform, Camera camera, Vector3 sceneElementPosition, Counter counter)
    {
        _canvasRectTransform = canvasRectTransform;
        _camera = camera;
        _sceneElementPosition = sceneElementPosition;
        _counter = counter;

        Start();
        UpdatePositionOnCanvas();
    }

    private void UpdateView(int currentScore)
    {
        _text.text = currentScore.ToString();
    }

    private void UpdatePositionOnCanvas()
    {
        Vector2 viewportPosition = _camera.WorldToViewportPoint(_sceneElementPosition);

        _uiElement.anchoredPosition = new Vector2(
            ((viewportPosition.x * _canvasRectTransform.sizeDelta.x) - (_canvasRectTransform.sizeDelta.x * _halfFactor)),
            ((viewportPosition.y * _canvasRectTransform.sizeDelta.y) - (_canvasRectTransform.sizeDelta.y * _halfFactor)));
    }
}
