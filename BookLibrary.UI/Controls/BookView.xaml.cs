using System;
using System.Windows;
using System.Windows.Controls;

namespace BookLibrary.UI.Controls
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookView : UserControl
    {
        public static readonly DependencyProperty BookNameProperty =
            DependencyProperty.Register("BookName", typeof(string), typeof(BookView), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty BookAuthorsProperty =
            DependencyProperty.Register("BookAuthors", typeof(string), typeof(BookView), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty BookYearProperty =
            DependencyProperty.Register("BookYear", typeof(DateTime), typeof(BookView), new UIPropertyMetadata(DateTime.Now));

        public BookView()
        {
            InitializeComponent();
        }

        public string BookName
        {
            get { return (string)GetValue(BookNameProperty); }
            set { SetValue(BookNameProperty, value); }
        }

        public string BookAuthors
        {
            get { return (string)GetValue(BookAuthorsProperty); }
            set { SetValue(BookAuthorsProperty, value); }
        }

        public DateTime BookYear
        {
            get { return (DateTime)GetValue(BookYearProperty); }
            set { SetValue(BookYearProperty, value); }
        }
    }
}
