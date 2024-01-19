using Spectre.Console;

namespace CharityDonationTracker.Uis;

public class CharityUI
{
    private MainService donationTracker;

    public CharityUI(MainService donationTracker)
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
                    new FigletText("* Charity *")
                        .LeftJustified()
                        .Color(Color.Cyan1));
            Console.WriteLine();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .PageSize(5)
                    .AddChoices(new[] { "Donate", "View Donations", "Back" }));

            AnsiConsole.Clear();

            switch (option)
            {
                case "Donate":
                    decimal amount = AnsiConsole.Ask<decimal>("Enter the donation amount:");
                    string additionalInfo = AnsiConsole.Ask<string>("Enter additional information (optional):");
                    bool donationAdded = donationTracker.AddDonation(amount, additionalInfo);
                    if (donationAdded)
                    {
                        AnsiConsole.Markup($"Donation of {amount} added successfully.");
                    }
                    else
                    {
                        AnsiConsole.Markup($"[red1]Unable to add donation. Insufficient funds.[/]");
                    }
                    break;

                case "View Donations":
                    donationTracker.ShowDonationHistory();
                    break;

                case "Back":
                    keepRunning = false;
                    break;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[yellow]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }
}