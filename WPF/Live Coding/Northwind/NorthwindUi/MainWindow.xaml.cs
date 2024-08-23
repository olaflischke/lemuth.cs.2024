using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NorthwindDal.Model;
using System.Configuration;
using System.Diagnostics;
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

namespace NorthwindUi;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private NorthwindContext context;

    public MainWindow()
    {
        InitializeComponent();

        //context = GetContext();
    }

    private NorthwindContext GetContext()
    {
        DbContextOptionsBuilder<NorthwindContext> builder = new DbContextOptionsBuilder<NorthwindContext>()
                                                        .UseSqlServer(ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString)
                                                        .LogTo(log => Debug.WriteLine(log), Microsoft.Extensions.Logging.LogLevel.Information);

        return new NorthwindContext(builder.Options);
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        using NorthwindContext context = GetContext();
        IQueryable<string?> qLaender = context.Customers.Select(cu => cu.Country).Distinct();

        foreach (string? item in qLaender)
        {
            TreeViewItem tvi = new TreeViewItem() { Header = item };
            tvi.Items.Add(new TreeViewItem());
            tvi.Expanded += this.Land_Expanded;
            trvCustomers.Items.Add(tvi);
        }
    }

    private void Land_Expanded(object sender, RoutedEventArgs e)
    {
        if (sender is TreeViewItem tviLand)
        {
            string land = tviLand.Header.ToString();

            using NorthwindContext context = GetContext();
            IQueryable<Customer> qKundenDesLandes = context.Customers.Where(cu => cu.Country == land);

            tviLand.Items.Clear();

            foreach (Customer kunde in qKundenDesLandes)
            {
                TreeViewItem tvi = new TreeViewItem() { Header = kunde.CompanyName, Tag = kunde.CustomerId };
                tvi.Selected += this.Kunde_Selected;
                tviLand.Items.Add(tvi);
            }
        }
    }

    private void Kunde_Selected(object sender, RoutedEventArgs e)
    {
        if (sender is TreeViewItem tviKunde && tviKunde.Tag != null)
        {
            //Customer kunde = context.Customers.Find(tviKunde.Tag.ToString());

            using NorthwindContext context = GetContext();
            var qOrdersDesKunden = context.Orders
                                            .Include(od => od.OrderDetails) // OrderDetails mitladen
                                            .ThenInclude(od => od.Product) // die Products zu den OrderDetails laden
                                            .Where(od => od.CustomerId == tviKunde.Tag.ToString()).ToList(); // Query muss asugeführt werden! (-> Deferred Execution)

            cbxOrders.ItemsSource = qOrdersDesKunden;
        }
    }

    private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
    {
        if (((TreeViewItem)trvCustomers.SelectedItem).Tag != null)
        {
            using NorthwindContext context = GetContext();
            Customer kundeToEdit = context.Customers.Find(((TreeViewItem)trvCustomers.SelectedItem).Tag.ToString());

            AddEditCustomer dlgEditCustomer = new AddEditCustomer(kundeToEdit);
            if (dlgEditCustomer.ShowDialog() == true)
            {
                context.SaveChanges();
            }
            else
            {
                context.Entry(kundeToEdit).Reload(); // Kunde zurücksetzen
            }
        }
    }

    private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
    {
        Customer neuerKunde = new Customer();
        neuerKunde.Country = "Germany"; // TOD: Land des Knotens eintragen

        AddEditCustomer dlgAddCustomer = new AddEditCustomer(neuerKunde);
        if (dlgAddCustomer.ShowDialog() == true)
        {
            using NorthwindContext context = GetContext();

            context.Customers.Add(neuerKunde);

            context.SaveChanges();
        }
    }

    //private void cbxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    if (cbxOrders.SelectedItem is Order order)
    //    {
    //        int orderId = order.Id;

    //        using NorthwindContext context = GetContext();
    //        var qOrderInfo = context.OrderDetails.Where(od => od.OrderId == orderId)
    //                                            .Select(od => new { od.Quantity, od.Product.ProductName, od.UnitPrice, od.Discount })
    //                                            .ToList();

    //        dgOrderInfo.ItemsSource = qOrderInfo;
    //    }


    //}
}