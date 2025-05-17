namespace CashTrackr.Domain.Transactions;

public class Transaction
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public Amount Amount { get; private set; }
    public Type Type { get; private set; }

    public Transaction(DateTime date, decimal value, Type type)
    {
        Date = date;
        Amount = new Amount(value);
        Type = type;
    }

    public Transaction() { }
}
