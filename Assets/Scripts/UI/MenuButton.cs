using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private float _movingPosition;
    [SerializeField] private float _movingDuration;

    private float _activePosition;

    public event Action Click;

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
        RectTransform rectTransform = (RectTransform)transform;
        _activePosition = rectTransform.anchoredPosition.x;
        gameObject.SetActive(true);

        DOTween.Sequence().
            Append(rectTransform.DOAnchorPosX(_movingPosition, 0f)).
            Append(rectTransform.DOAnchorPosX(_activePosition, _movingDuration));
    }

    private void Hide()
    {
        RectTransform rectTransform = (RectTransform)transform;

        DOTween.Sequence().
            Append(rectTransform.DOAnchorPosX(_movingPosition, _movingDuration)).
            onComplete += () => gameObject.SetActive(false);
    }
}
