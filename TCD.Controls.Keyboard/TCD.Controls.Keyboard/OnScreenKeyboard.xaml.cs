

using Windows.Foundation;
/**
Copyright(c) Microsoft Open Technologies, Inc.All rights reserved.
Modified by Michael Osthege (2016)
The MIT License(MIT)
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files(the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions :
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
**/
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TCD.Controls.Keyboard
{
    public partial class OnScreenKeyBoard : UserControl
    {
        #region Properties
        public object Host { get; set; }
        
        public KeyboardLayouts InitialLayout
        {
            get { return (this.DataContext as KeyboardViewModel).Layout; }
            set { (this.DataContext as KeyboardViewModel).Layout = value; }
        }
        public static readonly DependencyProperty InitialLayoutProperty = DependencyProperty.Register("InitialLayout", typeof(KeyboardLayouts), typeof(OnScreenKeyBoard), new PropertyMetadata(KeyboardLayouts.English));

        public Visibility IsVisible
        {
            get { return (this.DataContext as KeyboardViewModel).IsVisible; }
            set { (this.DataContext as KeyboardViewModel).IsVisible = value; }
        }
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisibleProperty", typeof(Visibility), typeof(OnScreenKeyBoard), new PropertyMetadata(Visibility.Collapsed));
        #endregion

        public OnScreenKeyBoard()
        {
            DataContext = new KeyboardViewModel(this);
            InitializeComponent();
        }

        public void RegisterTarget(TextBox control)
        {
            RegisterBox(control);
        }
        public void RegisterTarget(PasswordBox control)
        {
            RegisterBox(control);
        }
        public void RegisterTarget(Control control)
        {
            RegisterBox(control);
        }
        private void RegisterBox(Control control)
        {
            control.GotFocus += delegate
            {
                (DataContext as KeyboardViewModel).TargetBox = control;
                System.Diagnostics.Debug.WriteLine("focused");
                (DataContext as KeyboardViewModel).IsVisible = Visibility.Visible;
                this.SizeChanged += OnScreenKeyBoard_SizeChanged;
            };
            control.LostFocus += delegate
            {
                (DataContext as KeyboardViewModel).TargetBox = null;
                System.Diagnostics.Debug.WriteLine("left");
                (DataContext as KeyboardViewModel).IsVisible = Visibility.Collapsed;
            };
        }

        private void OnScreenKeyBoard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var control = (DataContext as KeyboardViewModel).TargetBox;
            if (control != null)
                BringFocusedControlToView(control);
        }

        private void BringFocusedControlToView(Control control)
        {
            FrameworkElement frameworkElement = control.Parent as FrameworkElement;
            GeneralTransform generalTransform = control.TransformToVisual(frameworkElement);
            Point point = generalTransform.TransformPoint(new Point(0.0, 0.0));
            ScrollViewer scrollViewer;
            do
            {
                scrollViewer = (frameworkElement as ScrollViewer);
                frameworkElement = (VisualTreeHelper.GetParent(frameworkElement) as FrameworkElement);
            }
            while (scrollViewer == null && frameworkElement != null);

            if (scrollViewer != null)
            {
                scrollViewer.ChangeView(new double?(0.0), new double?(point.Y), new float?((float)1));
            }
        }

    }
}

