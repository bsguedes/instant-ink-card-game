using InstantInkCardGame.Cards;
using System.Collections.Generic;

namespace InstantInkCardGame.Sets
{
    class DuelistSet : InstantInkCardGameSet
    {
        public override string SetName => "DUELIST SET";

        public override int SetYear => 2018;

        public override string Title => "Duelist Set New Rules";

        protected override int MaxCards => 61;

        protected override IEnumerable<CardBase> InternalCards
        {
            get
            {
                yield return new PrintingJobCard("Biology Review", 25, 10, 4, "Review notes to next week's Biology class.", "biology", "SHARED DECK");
                yield return new PrintingJobCard("Board Game Rules", 35, 20, 6, "Printed the rules of my new board game!", "board", "SHARED DECK");
                yield return new PrintingJobCard("Book Chapter", 75, 40, 11, "I will need to review a few Math lessons.", "book", "SHARED DECK");
                yield return new PrintingJobCard("Character Sheet", 20, 20, 5, "I will create lots of characters for the next roleplaying gaming session.", "character", "SHARED DECK");
                yield return new PrintingJobCard("Class Notes", 100, 30, 8, "The notes for my classes in college are distributed as PDFs.", "class", "SHARED DECK");
                yield return new PrintingJobCard("Company Presentation", 55, 30, 9, "Attendees of my presentation will receive a printed summary.", "company", "SHARED DECK");
                yield return new PrintingJobCard("Concert Tickets", 15, 30, 5, "Bought a ticket online and now I have to print it!", "concert", "SHARED DECK");
                yield return new PrintingJobCard("Conversation Logs", 90, 10, 6, "I need to keep a copy of these chats.", "chat", "SHARED DECK");
                yield return new PrintingJobCard("Coupons", 15, 40, 7, "I can redeem these coupons to earn lots of discounts", "coupons", "SHARED DECK");
                yield return new PrintingJobCard("Diagnostic Tests", 30, 10, 5, "Need to take these documents to my doctor.", "diagnostic", "SHARED DECK");
                yield return new PrintingJobCard("Driving Classes", 85, 20, 7, "The traffic rules that I need to learn before taking driving classes.", "driving", "SHARED DECK");
                yield return new PrintingJobCard("Fashion Guide", 10, 80, 10, "I will print this newspiece on fashion tendencies.", "fashion", "SHARED DECK");
                yield return new PrintingJobCard("Golf Courses", 70, 50, 11, "A summary of all golf courses in hand!", "golf", "SHARED DECK");
                yield return new PrintingJobCard("Invoices", 45, 30, 7, "Printing Instant Ink invoices to keep a printed record!", "invoice", "SHARED DECK");
                yield return new PrintingJobCard("Know Your Meme", 65, 50, 12, "The latest Internet memes!", "meme", "SHARED DECK");
                yield return new PrintingJobCard("Office Decoration", 25, 80, 13, "Funny pictures to decorate my office!", "office", "SHARED DECK");
                yield return new PrintingJobCard("Paris", 25, 50, 8, "Photos taken in the beautiful French capital.", "paris", "SHARED DECK");
                yield return new PrintingJobCard("Patch Notes", 25, 30, 7, "The new features and balance changes of my favorite online game.", "patch", "SHARED DECK");
                yield return new PrintingJobCard("Pet Friendly", 15, 10, 3, "A catalog of products to please your best friend!", "pet", "SHARED DECK");
                yield return new PrintingJobCard("Photo Calendar", 15, 40, 6, "Next year calendar will have beautiful pictures of my family.", "calendar", "SHARED DECK");
                yield return new PrintingJobCard("Politician", 50, 40, 9, "I printed the agenda of the main candidates in the next elections!", "politician", "SHARED DECK");
                yield return new PrintingJobCard("Programming Book", 80, 40, 9, "Programming lessons to read before starting a new project.", "programming", "SHARED DECK");
                yield return new PrintingJobCard("Résumé", 15, 20, 4, "Looking for my first job.", "resume", "SHARED DECK");
                yield return new PrintingJobCard("Rio de Janeiro", 15, 50, 8, "Photos from my last trip to Rio!", "rio", "SHARED DECK");
                yield return new PrintingJobCard("Sheet Music", 40, 60, 10, "My band will have a rehearsal tonight.", "sheet", "SHARED DECK");
                yield return new PrintingJobCard("Shopping List", 25, 20, 5, "Cannot forget to buy these items the next time I go shopping!", "shopping", "SHARED DECK");
                yield return new PrintingJobCard("Sports Report", 55, 30, 8, "The latest news and transferences in the most popular sports.", "sports", "SHARED DECK");
                yield return new PrintingJobCard("Teacher", 75, 40, 10, "Printing additional lessons for my students.", "teacher", "SHARED DECK");
                yield return new PrintingJobCard("Technology Magazine", 40, 40, 7, "Everything new in the Tech World!", "tech", "SHARED DECK");
                yield return new PrintingJobCard("Wedding Photos", 90, 80, 18, "Finally printed last month marriage pictures.", "wedding", "SHARED DECK");
                for (int i = 0; i < 2; i++)
                {
                    yield return new EventCard("Environmental", "If your Ink Level is lower than the other player’s Ink Level, you may play a Replace Cartridge card right now and still play a Printing Job.", "environmental");
                    yield return new EventCard("Recycle more!", "Discard 1 card from your hand. Then, take 1 card from the Shared Resources.", "recycle");
                    yield return new EventCard("Sharing Instant Ink", "Take 1 card from the Shared Resources. Then, the other player may play a Printing Job. If they do, take another card from the Shared Resources.", "sharing");
                }
                yield return new EventCard("Black Friday", "Search your Deck for an Event card, reveal it, and put it into your hand. Then, shuffle your Deck.", "black", "SHARED DECK");
                yield return new EventCard("Environmental", "If your Ink Level is lower than the other player’s Ink Level, you may play a Replace Cartridge card right now and still play a Printing Job card this week.", "environmental", "SHARED DECK");
                yield return new EventCard("Maintenance", "The other player cannot play a Printing Job during his or her next week.", "maintenance", "SHARED DECK", "Shuffle this card with the Shared Deck instead of putting it on your discard pile.");
                yield return new EventCard("Planned Printing", "Take 1 card from the Shared Resources. You may play an additional Printing Job. If you do, the other player may take up to 2 cards from the Shared Resources.", "planned", "SHARED DECK", "Shuffle this card with the Shared Deck instead of putting it on your discard pile.");
                yield return new EventCard("Recycle more!", "Discard 1 card from your hand. Then, take 1 card from the Shared Resources.", "recycle", "SHARED DECK");
                yield return new EventCard("Refreshing", "You can play this card only if you have less cards in hand than the other player.\n\nYou may play 2 additional Printing Jobs this week.", "refreshing", "SHARED DECK", "Shuffle this card with the Shared Deck instead of putting it on your discard pile.");
                yield return new EventCard("Sharing Instant Ink", "Take 1 card from the Shared Resources. Then, the other player may play a Printing Job. If they do, take another card from the Shared Resources.", "sharing", "SHARED DECK");
                for (int i = 0; i < 4; i++)
                {
                    yield return new EnrollmentCard("Enrollment Card SIGMA", "Choose 1:\n• Pay for a new Instant Ink Plan and add the Plan Page Count to your current Page Count (up to twice your new Plan Page Count);\n• Shuffle all 5 Shared Resources into the Shared Deck. Then, reveal 5 new Shared Resources. You may take 1 Shared Resource and add it to your hand.", "enrollment");
                }
                for (int i = 0; i < 3; i++)
                {
                    yield return new EnrollmentCard("Enrollment Card SIGMA", "Choose 1:\n• Pay for a new Instant Ink Plan and add the Plan Page Count to your current Page Count (up to twice your new Plan Page Count);\n• Shuffle all 5 Shared Resources into the Shared Deck. Then, reveal 5 new Shared Resources. You may take 1 Shared Resource and add it to your hand.", "enrollment", "SHARED DECK");
                }
                yield return new ReplaceCartridgeCard("Replace Cartridge PSI", "Replace a cartridge in your printer, filling the Ink Level to the maximum amount according to your Printer. Place this card back in the board after playing it.", "replace", "You never run out of ink!");
                yield return new GoalCard("Massive Printing", "Give this card to the player who printed the most Printing Jobs during this game.", 8, "massive");
                yield return new GoalCard("Paper Balance", "Give this card to the player with the lowest Page Count on their playmat at the end of the game.", 7, "balance");
                yield return new GoalCard("Enrollment Master", "Give this card to the player with more Enrollment Cards in his or her discard pile at the end of the game.", 6, "master");
                yield return new GoalCard("Nature's Friend", "Give this card to the player with the lowest Ink Level at the end of the game.", 5, "nature");
                yield return new GoalCard("Printed at Home", "Give this card to the player with less cards remaining in his or her Player Deck at the end of the game.", 4, "home");
                yield return new PlanCard("Free Printing Plan", "The LUPS Plan!", 15, 0, 10, "lups");
                yield return new PlanCard("Occasional Printing Plan", "The 50-Page Plan!", 50, 3, 15, "occasional");
                yield return new PlanCard("Moderate Printing Plan", "The 100-Page Plan!", 100, 5, 20, "moderate");
                yield return new PlanCard("Frequent Printing Plan", "The 300-Page Plan!", 300, 10, 25, "frequent");
                yield return new HelperCard("First Player", "Give this card to the First Player at the beginning of the game.\n\nThe player that has this card starts playing at the beginning of each week.\n\nGive this card to the other player at the beginning of months 2 and 4.", "first");
            }
        }
    }
}
