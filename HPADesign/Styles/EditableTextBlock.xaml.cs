using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.ComponentModel;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace HPADesign.Styles
{
    public sealed partial class EditableTextBlock : UserControl
    {
        public string Text
        {
            get { return GetValue(TextProperty) as string; }
            set { SetValue(TextProperty, value); }
        }
        
        public EditableTextBlock()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        
        public static readonly DependencyProperty TextProperty
            = DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(""));

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBox.Visibility = Visibility.Visible;
            TextBlock.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox.Visibility = Visibility.Collapsed;
            TextBlock.Visibility = Visibility.Visible;
        }
    }
}
