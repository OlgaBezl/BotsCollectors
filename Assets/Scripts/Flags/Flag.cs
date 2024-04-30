using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Flag : MonoBehaviour
{
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private MouseFollower _mouseFollower;

    public event Action<Vector3, Unit> ReadyToBuilded;
    public event Action<Vector3> Setted;

    public FlagState CurrentState { get; private set; } = FlagState.Hide;

    private void OnEnable()
    {
        _playingField.Clicked += TrySet;
    }

    private void OnDisable()
    {
        _playingField.Clicked -= TrySet;
    }

    public void SetParameters(PlayingField playingField, Camera camera, Transform planeTransform)
    {
        _playingField = playingField;
        _playingField.Clicked += TrySet;
        _mouseFollower.SetParameters(camera, planeTransform);
    }

    public void Activate()
    {
        if (CurrentState == FlagState.Move)
            return;

        GetComponent<Collider>().enabled = false;
        transform.gameObject.SetActive(true);
        CurrentState = FlagState.Move;
        _mouseFollower.SetActive(true);
    }

    public void Deactivate()
    {
        if (CurrentState == FlagState.Hide)
            return;

        GetComponent<Collider>().enabled = false;
        transform.gameObject.SetActive(false);
        CurrentState = FlagState.Hide;
        _mouseFollower.SetActive(false);
    }
    
    public void TrySet()
    {
        if (CurrentState == FlagState.Hide || CurrentState == FlagState.DeliveryResourcesToFlag)
        {
            return;
        }

        if(CurrentState != FlagState.Move)
        {
            _mouseFollower.UpdatePosition();
        }
        else
        {
            CurrentState = FlagState.Setted;
            _mouseFollower.SetActive(false);
        }

        GetComponent<Collider>().enabled = true;
        CurrentState = FlagState.Setted;
        Setted?.Invoke(transform.position);
    }

    public void WaitDeliveryResources()
    {
        CurrentState = FlagState.DeliveryResourcesToFlag;
    }

    public void BuildBase(Unit firstUnit)
    {
        Deactivate();
        ReadyToBuilded?.Invoke(transform.position, firstUnit);
    }
}
