using Spectre.Console;
using CharityDonationTracker.Uis;

public class MainUI
{
    private readonly CharityUI charityUI;
    private readonly WalletUI walletUI;
    private readonly MainService donationTracker;

    public MainUI()
    {
        donationTracker = new MainService();
        charityUI = new CharityUI(donationTracker);
        walletUI = new WalletUI(donationTracker);
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                    new FigletText("* Charity Donation System *")
                        .LeftJustified()
                        .Color(Color.Green));
            Console.WriteLine();


            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .PageSize(5)
                    .AddChoices(new[] { "Charity", "Wallet", "Exit" }));

            AnsiConsole.Clear();

            switch (option)
            {
                case "Charity":
                    charityUI.Run();
                    break;
                case "Wallet":
                    walletUI.Run();
                    break;
                case "Exit":
                    AnsiConsole.Clear();
                    AnsiConsole.Write(
                            new FigletText("* Thanks for using! *")
                                .LeftJustified()
                                .Color(Color.SandyBrown));
                    return;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }
}