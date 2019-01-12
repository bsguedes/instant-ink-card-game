using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InstantInkCardGame.Cards
{
    /// <summary>
    /// Interaction logic for CardBase.xaml
    /// </summary>
    public abstract partial class CardBase : UserControl
    {
        internal string CardName { get; }

        public CardBase(Brush color, string name, string iconName, string imgPath, bool IsFullCard, string label = null, bool isWhite = false)
        {
            InitializeComponent();
            SetBackgroundColor(color);
            SetCardName(name);
            SetCardLabel(string.IsNullOrEmpty(label) ? string.Empty : label, isWhite);
            if (isWhite)
            {
                cardCopyright.Foreground = Brushes.White;
                cardNumber.Foreground = Brushes.White;
            }
            SetCardIcon(iconName);
            this.CardName = imgPath;
            if (IsFullCard)
            {
                fullCard.Visibility = Visibility.Visible;
            }
            else
            {
                halfCard.Visibility = Visibility.Visible;
            }
        }

        public void SetCardSet(string imgPath)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, string.Format(@"Images\{0}", imgPath));
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            cardSet.Source = bitmap;
        }

        public void SetCardImage(string imgPath)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, string.Format(@"Images\{0}", imgPath));
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            cardImage.Source = bitmap;
        }

        public void SetAltText(string text)
        {
            cardAlt.Text = text;
            cardAlt.Visibility = Visibility.Visible;
        }

        public void SetCopyright(int setYear)
        {
            cardCopyright.Text = string.Format("© {0} Bruno Guedes", setYear);
        }

        public void SetCardIcon(string iconPath)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, string.Format(@"Icons\{0}.png", iconPath));
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            cardIcon.Source = bitmap;
        }

        public void SetLoreText(string lore, bool isItalic = true)
        {
            cardLore.Text = lore;
            cardLore.FontStyle = isItalic ? FontStyles.Italic : FontStyles.Normal;
        }

        public void SetCardText(string text)
        {
            cardText.Text = text;
        }

        public void SetPoints(int points)
        {
            cardPoints.Text = points.ToString();
            borderPoints.Visibility = Visibility.Visible;

            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"CardIcons\vp.png");
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            imgVP.Source = bitmap;
        }

        public void SetInkCount(int ink)
        {
            cardInkLevel.Text = ink.ToString();
            borderInkLevel.Visibility = Visibility.Visible;

            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"CardIcons\ink.png");
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            imgInk.Source = bitmap;
        }

        public void SetPageCount(int pages)
        {
            cardPagesCounter.Text = pages.ToString();
            borderPagesCounter.Visibility = Visibility.Visible;

            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"CardIcons\pages.png");
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            imgPages.Source = bitmap;
        }

        public void SetCost(int cost)
        {
            cardCost.Text = cost.ToString();
            borderCost.Visibility = Visibility.Visible;

            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"CardIcons\money.png");
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            imgMoney.Source = bitmap;
        }

        public void SetCostPer10Pages(int cost)
        {
            cardCostPer10Pages.Text = cost.ToString();
            borderCostPer10Pages.Visibility = Visibility.Visible;

            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"CardIcons\extra.png");
            BitmapImage bitmap = new BitmapImage(new Uri(path));
            imgExtra.Source = bitmap;
        }

        public void SetNumber(int i, int cardCount)
        {
            cardNumber.Text = string.Format("{0}/{1}", i, cardCount);
        }

        protected void SetCardName(string name)
        {
            cardName.Text = name;
            cardName.LayoutTransform = new ScaleTransform(0.75 * Math.Pow(0.94, Math.Max(0, name.Length - 19)), 1);
        }

        public void SetCardLabel(string label, bool isWhite = false)
        {
            cardLabel.Text = label;
            cardLabel.LayoutTransform = new ScaleTransform(0.8, 1);
            cardLabel.Foreground = isWhite ? Brushes.White : Brushes.Black;
        }

        protected void SetBackgroundColor(Brush color)
        {
            cardBackground.Background = color;
        }
    }
}
