using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CustomerLib;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MVVMUnoApp
{
    public sealed partial class Detail : UserControl
    {
        public Detail()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty CustomerProperty = DependencyProperty.Register(
            "Customer",
            typeof(Customer),
            typeof(Detail),
            new PropertyMetadata(null)
        );


        public Customer Customer
        {
            get => (Customer)GetValue(CustomerProperty);
            set => SetValue(CustomerProperty, value);
        }

    }
}
