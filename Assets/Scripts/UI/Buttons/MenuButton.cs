using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private float _movingPosition;
    [SerializeField] private float _movingDuration;

    private float _startPosition;
    private RectTransform _rectTransform;

    public event Action Click;

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
        _startPosition = _rectTransform.anchoredPosition.x;
    }

    private void OnClick()
    {
        _button.onClick.RemoveListener(OnClick);
        Click?.Invoke();
        Hide();
    }

    public void Active()
    {
        _button.onClick.AddListener(OnClick);
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        DOTween.Sequence().
            Append(_rectTransform.DOAnchorPosX(_movingPosition, 0f)).
            Append(_rectTransform.DOAnchorPosX(_startPosition, _movingDuration));
    }

    private void Hide()
    {
        DOTween.Sequence().
            Append(_rectTransform.DOAnchorPosX(_movingPosition, _movingDuration)).
            onComplete += () => gameObject.SetActive(false);
    }
}
