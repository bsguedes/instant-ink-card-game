using System.Windows.Media;

namespace InstantInkCardGame.Cards
{
    public class PrintingJobCard : CardBase
    {
        public PrintingJobCard(string name, int pages, int ink, int points, string lore, string imgPath, string label = null)
            : base(Brushes.DodgerBlue, name, "printingjob", imgPath, false, label, true)
        {
            base.SetPageCount(pages);
            base.SetInkCount(ink);
            base.SetPoints(points);
            base.SetLoreText(lore);
        }
    }

    public class PrinterCard : CardBase
    {
        public PrinterCard(string name, string lore, int ink, int cost, string imgPath)
            : base(Brushes.LightGray, name, "printer", imgPath, false)
        {
            base.SetInkCount(ink);
            base.SetCost(cost);
            base.SetLoreText(lore);
        }
    }

    public class PlanCard : CardBase
    {
        public PlanCard(string name, string lore, int pages, int cost, int cost10, string imgPath)
            : base(Brushes.MediumPurple, name, "plan", imgPath, false)
        {
            base.SetCost(cost);
            base.SetPageCount(pages);
            base.SetCostPer10Pages(cost10);
            base.SetLoreText(lore);
        }
    }

    public class EventCard : CardBase
    {
        public EventCard(string name, string text, string imgPath, string label = null, string altText = null)
            : base(Brushes.Orange, name, "event", imgPath, true, label)
        {
            base.SetCardText(text);
            if (!string.IsNullOrEmpty(altText))
            {
                base.SetAltText(altText);
            }
        }
    }

    public class ReplaceCartridgeCard : CardBase
    {
        public ReplaceCartridgeCard(string name, string text, string imgPath, string altText = null)
            : base(Brushes.LawnGreen, name, "replacecartridge", imgPath, true)
        {
            base.SetCardText(text);
            if (!string.IsNullOrEmpty(altText))
            {
                base.SetAltText(altText);
            }
        }
    }

    public class EnrollmentCard : CardBase
    {
        public EnrollmentCard(string name, string text, string imgPath, string label = null)
            : base(Brushes.LightGoldenrodYellow, name, "enrollment", imgPath, true, label)
        {
            base.SetCardText(text);
        }
    }

    public class GoalCard : CardBase
    {
        public GoalCard(string name, string text, int points, string imgPath, string label = null)
            : base(Brushes.SaddleBrown, name, "goal", imgPath, false, label, true)
        {
            base.SetLoreText(text, false);
            base.SetPoints(points);
        }
    }

    public class HelperCard : CardBase
    {
        public HelperCard(string name, string text, string imgPath, string label = null)
            : base(Brushes.Black, name, "player", imgPath, true, label, true)
        {
            base.SetCardText(text);
        }
    }
}

