namespace CashTrackr.Domain.Transactions;

public record Amount
{
    public decimal Value { get; private set; }

    public Amount(decimal value)
    {
        Validate(value);

        Value = value;
    }

    private static void Validate(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(Amount)} must be greater than 0.");
        }
    }
}
