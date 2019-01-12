using InstantInkCardGame.Cards;
using Reporter.Data;
using System.Collections.Generic;

namespace InstantInkCardGame
{
    class Deck
    {
    }

    public abstract class InstantInkCardGameSet
    {
        public abstract string SetName { get; }
        protected abstract int MaxCards { get; }

        private IEnumerable<CardBase> Cards
        {
            get
            {
                int i = 0;
                foreach (var c in InternalCards)
                {
                    yield return NumerateCard(c, ++i);
                }
            }
        }

        private CardBase NumerateCard(CardBase c, int i)
        {
            c.SetCardSet(string.Format(@"{0}.png", this.SetName));
            c.SetCardImage(string.Format(@"{0}\{1}.png", this.SetName, c.CardName));
            c.SetNumber(i, MaxCards);
            c.SetCopyright(this.SetYear);
            return c;
        }

        protected abstract IEnumerable<CardBase> InternalCards { get; }

        public abstract int SetYear { get; }
        public abstract string Title { get; }

        internal IEnumerable<TableRow> GetRows(int v)
        {
            TableRow tr = null;
            List<object> list = null;
            int i = 0;
            foreach (CardBase c in this.Cards)
            {
                if (i % v == 0)
                {
                    if (tr != null)
                    {
                        yield return tr;
                    }
                    tr = new TableRow();
                    list = new List<object>();
                    tr.Values = list;
                }
                i++;
                list.Add(c);
            }
            yield return tr;
        }
    }
}
