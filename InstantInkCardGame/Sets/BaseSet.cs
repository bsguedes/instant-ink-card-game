using InstantInkCardGame.Cards;
using System.Collections.Generic;

namespace InstantInkCardGame.Sets
{
    public class BaseSet : InstantInkCardGameSet
    {
        public BaseSet()
        {

        }

        public override string SetName => "BASE SET";
        public override int SetYear => 2018;

        public override string Title => "Instant Ink Card Game Rules (for the Base Set)";

        protected override int MaxCards => 59;

        protected override IEnumerable<CardBase> InternalCards
        {
            get
            {
                yield return new PrintingJobCard("Accountant", 40, 40, 8, "This is the list of bills I paid this year.", "accountant");
                yield return new PrintingJobCard("Art Project", 100, 50, 14, "Let's take a look how beautiful this drawing will be.", "art");
                yield return new PrintingJobCard("Blueprint", 10, 40, 6, "That beautiful architectural project looks nice on paper!", "blueprint");
                yield return new PrintingJobCard("Business Card", 40, 50, 9, "I have to update my business card with my new phone number.", "card");
                yield return new PrintingJobCard("Childhood", 70, 70, 14, "Reliving childhood by printing these emotional pictures.", "childhood");
                yield return new PrintingJobCard("College Documents", 30, 10, 4, "I'll need many pages to print all these paperwork.", "college");
                yield return new PrintingJobCard("Concert", 40, 20, 6, "Our band will play this list of songs I've just printed!", "concert");
                yield return new PrintingJobCard("Contracts", 30, 20, 5, "Important documents for our new apartment.", "contracts");
                yield return new PrintingJobCard("Cooking", 20, 30, 6, "My grandparents will love these new recipes!", "cooking");
                yield return new PrintingJobCard("Dungeon Master", 60, 20, 7, "My next roleplaying adventure has to be printed!", "dungeonmaster");
                yield return new PrintingJobCard("Family Photos", 100, 70, 16, "Restoring happy moments with our loving ones.", "family");
                yield return new PrintingJobCard("Financial", 20, 20, 4, "Lots of bank reports to analyze.", "financial");
                yield return new PrintingJobCard("Flight Tickets", 10, 10, 2, "Do not forget to print the next flight tickets!", "flight");
                yield return new PrintingJobCard("Framed", 10, 60, 8, "These pictures from the mountains will look beautiful on the wall.", "framed");
                yield return new PrintingJobCard("Friends", 50, 70, 12, "The photos at the party were a blast!", "friends");
                yield return new PrintingJobCard("High School", 50, 10, 5, "I have to study to my next Physics exam!", "highschool");
                yield return new PrintingJobCard("Holiday Photos", 80, 60, 14, "The pictures from our last vacation could not be any better!", "holiday");
                yield return new PrintingJobCard("Homework", 40, 30, 7, "I have to print my Science project due by tomorrow!", "homework");
                yield return new PrintingJobCard("Interview", 60, 40, 10, "I'll print some questions to give to our candidates.", "interview");
                yield return new PrintingJobCard("Invitations", 70, 30, 9, "No better use to my ink than printing the birthday invitations!", "invitations");
                yield return new PrintingJobCard("Little Artist", 20, 50, 8, "I'll give these drawings to my kids to paint!", "artist");
                yield return new PrintingJobCard("Message", 20, 10, 3, "I prefer to read this long e-mail on paper instead of the screen.", "message");
                yield return new PrintingJobCard("Odyssey", 90, 40, 12, "A Greek classic looks better on paper!", "odyssey");
                yield return new PrintingJobCard("Print-And-Play", 70, 50, 12, "We've found this new game to play on Friday night!", "pnp");
                yield return new PrintingJobCard("Recent News", 10, 20, 3, "I should print these news to keep it for later.", "news");
                yield return new PrintingJobCard("Reports", 85, 30, 10, "All reports from last month meetings.", "reports");
                yield return new PrintingJobCard("Seen on the Internet", 80, 40, 11, "My workmates will love these fresh memes!", "meme");
                yield return new PrintingJobCard("Stock Charts", 50, 30, 8, "It will be easier to compare these charts on paper.", "stock");
                yield return new PrintingJobCard("Tourist", 30, 60, 10, "Won't risk going to a foreign country without some printed maps.", "tourist");
                yield return new PrintingJobCard("Weather Forecast", 15, 40, 7, "Printed maps for weather forecast for next days.", "weather");
                for (int i = 0; i < 2; i++)
                {
                    yield return new EventCard("Enjoy an Easier Way to Print", "Look at the top 6 cards of your Deck. You may put up to 2 Printing Jobs you find there into your hand. Shuffle the other cards back into your Deck.", "easier");
                    yield return new EventCard("Go Green!", "You can only play this Event if your current Ink Level is 50 or less.\n\nYou may print as many Printing Jobs as you want during this week.", "green");
                    yield return new EventCard("Increased Productivity", "You may play 2 Printing Jobs this week.", "productivity");
                    yield return new EventCard("Ink Savings", "You can only play this Event if your Ink Level is 0. \n\nYou may play a Replace Cartridge card this week and still play a Printing Job.", "inksavings");
                    yield return new EventCard("Promo Code", "Add 50 Pages to your current Page Count (do not exceed twice your Plan Page Count).", "promo");
                    yield return new EventCard("Recycle!", "You can only play this Event in the same week you played a Replace Cartridge card.\n\nDraw 3 cards.", "recycle");
                    yield return new EventCard("Refer a friend", "Congrats, you've been referred by a friend!\n\nDo not pay for your Plan next month.", "refer", altText: "Place this card over your Plan card as a reminder. Discard it after the beginning of the next month.");
                    yield return new EventCard("Welcome Kit", "Search your discard pile for an Event, and play it immediately.", "welcome");
                }
                for (int i = 0; i < 4; i++)
                {
                    yield return new EnrollmentCard("Enrollment Card RHO", "Choose 1:\n\n• Pay for a new Instant Ink Plan and add the Plan Page Count to your current Page Count (up to twice your new Plan Page Count);\n\n• Shuffle your hand into your Deck. Then, draw 4 cards.", "enrollment");
                }
                yield return new ReplaceCartridgeCard("Replace Cartridge PSI", "Replace a cartridge in your printer, filling the Ink Level to the maximum amount according to your Printer. Place this card back in the board after playing it.", "replace", "You never run out of ink!");
                yield return new PrinterCard("Printer ALPHA", "This printer is great!", 90, 30, "printer");
                yield return new PrinterCard("Printer BETA", "This printer is amazing!", 100, 40, "printer");
                yield return new PrinterCard("Printer GAMMA", "This printer is awesome!", 110, 50, "printer");
                yield return new PrinterCard("Printer DELTA", "This printer is magnificent!", 120, 60, "printer");
                yield return new PlanCard("Free Printing Plan", "The LUPS Plan!", 15, 0, 10, "lups");
                yield return new PlanCard("Occasional Printing Plan", "The 50-Page Plan!", 50, 3, 15, "occasional");
                yield return new PlanCard("Moderate Printing Plan", "The 100-Page Plan!", 100, 5, 20, "moderate");
                yield return new PlanCard("Frequent Printing Plan", "The 300-Page Plan!", 300, 10, 25, "frequent");

            }
        }
    }
}
