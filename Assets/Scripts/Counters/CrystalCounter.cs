public class CrystalCounter : Counter
{
    public bool TrySubtract(int value)
    {
        if (CurrentValue >= value)
        {
            CurrentValue -= value;
            return true;
        }

        return false;
    }
}
