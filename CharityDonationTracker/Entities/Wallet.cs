namespace CharityDonationTracker.Entities;

public class Wallet
{
    public decimal Balance { get; set; }

    public Wallet()
    {
        Balance = 0;
    }

    public void AddMoney(decimal amount)
    {
        Balance += amount;
    }
}
