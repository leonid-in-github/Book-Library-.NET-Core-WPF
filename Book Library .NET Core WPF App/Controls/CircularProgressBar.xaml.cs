using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Book_Library_.NET_Core_WPF_App.Controls
{
    public partial class CircularProgressBar
    {
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(1));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(1));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(CircularProgressBar), new UIPropertyMetadata(100));

        #region Fields

        private readonly DispatcherTimer _animationTimer;

        #endregion

        #region Constructors

        public CircularProgressBar()
        {
            InitializeComponent();

            IsVisibleChanged += OnVisibleChanged;

            _animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
            {
                Interval = new TimeSpan(0, 0, 0, 0, 75)
            };
        }

        #endregion

        #region Public Properties

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion

        private static void SetPosition(DependencyObject ellipse, double offset, double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50 + (Math.Sin(offset + (posOffSet * step)) * 50));
            ellipse.SetValue(Canvas.TopProperty, 50 + (Math.Cos(offset + (posOffSet * step)) * 50));
        }

        private void Start()
        {
            _animationTimer.Tick += OnAnimationTick;
            _animationTimer.Start();
        }

        private void Stop()
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= OnAnimationTick;
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            _spinnerRotate.Angle = (_spinnerRotate.Angle + 36) % 360;
        }

        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(_circle0, offset, 0.0, step);
            SetPosition(_circle1, offset, 1.0, step);
            SetPosition(_circle2, offset, 2.0, step);
            SetPosition(_circle3, offset, 3.0, step);
            SetPosition(_circle4, offset, 4.0, step);
            SetPosition(_circle5, offset, 5.0, step);
            SetPosition(_circle6, offset, 6.0, step);
            SetPosition(_circle7, offset, 7.0, step);
            SetPosition(_circle8, offset, 8.0, step);
        }

        private void OnCanvasUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }
    }
}
