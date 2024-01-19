using Spectre.Console;

namespace CharityDonationTracker.Uis;

public class WalletUI
{
    private MainService donationTracker;

    public WalletUI(MainService donationTracker)
    {
        this.donationTracker = donationTracker;
    }

    public void Run()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                    new FigletText("* Wallet *")
                        .LeftJustified()
                        .Color(Color.Blue1));
            Console.WriteLine();


            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .PageSize(5)
                    .AddChoices(new[] { "Add Money", "View Balance", "Back" }));

            AnsiConsole.Clear();

            switch (option)
            {
                case "Add Money":
                    AddMoneyToWalletUI();
                    break;
                case "View Balance":
                    ViewWalletBalance();
                    break;
                case "Back":
                    keepRunning = false;
                    break;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[yellow]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }

    private void AddMoneyToWalletUI()
    {
        var amountToAdd = AnsiConsole.Ask<decimal>("Enter the amount to add to your wallet:");
        var success = donationTracker.AddMoneyToWallet(amountToAdd);
        if (success)
        {
            AnsiConsole.WriteLine("Money added to wallet successfully!");
        }
        else
        {
            AnsiConsole.WriteLine("Invalid amount. Please enter a positive number.");
        }
    }

    private void ViewWalletBalance()
    {
        AnsiConsole.WriteLine($"Wallet Balance: {donationTracker.GetWalletBalance()}");
    }
}