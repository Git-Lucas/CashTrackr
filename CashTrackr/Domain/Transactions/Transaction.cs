namespace CashTrackr.Domain.Transactions;

public class Transaction
{
    public int Id { get; private set; }
    public DateOnly Date { get; private set; }
    public Amount Amount { get; private set; }
    public Type Type { get; private set; }

    public Transaction(DateOnly date, decimal value, Type type)
    {
        Date = date;
        Amount = new Amount(value);
        Type = type;
    }

    public Transaction() { }

    public decimal NormalizeTransactionValue()
    {
        if (Type == Type.Debit)
        {
            return -Amount.Value;
        }

        return Amount.Value;
    }
}
