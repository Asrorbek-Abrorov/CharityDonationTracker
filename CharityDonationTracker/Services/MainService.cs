using CharityDonationTracker.Entities;
using Newtonsoft.Json;
using Spectre.Console;

public class MainService
{
    private List<Donation> donations;
    private string donationHistoryFile;
    private Wallet userWallet;

    public MainService()
    {
        donations = new List<Donation>();
        donationHistoryFile = "../../../../CharityDonationTracker/donationHistoryFile.json";
        userWallet = new Wallet();
        LoadDonationHistory();
    }

    public bool AddDonation(decimal amount, string additionalInfo)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (amount <= userWallet.Balance)
        {
            var donation = new Donation
            {
                Amount = amount,
                Date = DateTime.Now,
                AdditionalInfo = additionalInfo
            };

            donations.Add(donation);
            userWallet.Balance -= amount;
            SaveDonationHistory();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AddMoneyToWallet(decimal amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        userWallet.AddMoney(amount);
        SaveDonationHistory();
        return true;
    }

    public void ShowDonationHistory()
    {
        var table = new Table();
        table.AddColumn("Amount");
        table.AddColumn("Date");
        table.AddColumn("Additional Info");

        foreach (var donation in donations)
        {
            table.AddRow(donation.Amount.ToString(), donation.Date.ToString(), donation.AdditionalInfo);
        }

        AnsiConsole.Render(table);
    }

    private void LoadDonationHistory()
    {
        if (File.Exists(donationHistoryFile))
        {
            var json = File.ReadAllText(donationHistoryFile);
            var data = JsonConvert.DeserializeObject<dynamic>(json);

            donations = JsonConvert.DeserializeObject<List<Donation>>(data.Donations.ToString());
            userWallet.Balance = data.WalletBalance;
        }
    }

    private void SaveDonationHistory()
    {
        var dataToSave = new
        {
            Donations = donations,
            WalletBalance = userWallet.Balance
        };

        var json = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
        File.WriteAllText(donationHistoryFile, json);
    }

    public decimal GetWalletBalance()
    {
        return userWallet.Balance;
    }
}
