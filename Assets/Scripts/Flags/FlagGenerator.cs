using UnityEngine;

public class FlagGenerator : MonoBehaviour
{
    [SerializeField] private Flag _prefab;
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _planeTransform;

    public Flag Generate(Transform parentTransform)
    {
        Flag flag = Instantiate(_prefab, parentTransform);
        flag.SetParameters(_playingField, _camera, _planeTransform);
        return flag;
    }
}
