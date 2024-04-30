using UnityEngine;

[RequireComponent (typeof(Collider))]
public class UnitCollector : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private void OnTriggerEnter(Collider other)
    {
        switch (_unit.CurrentState)
        {
            case UnitState.MoveToCristal:
                if (other.TryGetComponent(out Crystal crystal))
                {
                    if (_unit.CurrentCrystal == crystal)
                        _unit.PickUpCrystal();
                }

                break;

            case UnitState.MoveToParentBase:
                if (other.TryGetComponent(out Base parentBase))
                {
                    if (parentBase.transform.position == _unit.ParentBasePosition)
                        _unit.ReturnToBase(parentBase);
                }

                break;

            case UnitState.MoveToFlag:
                if (other.TryGetComponent(out Flag flag))
                {
                    _unit.BuildBase(flag);
                }

                break;

            default: break;
        }
    }
}
