using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;

namespace ChinookUi.ViewModels;

public class CustomerBrowserViewModel : BaseViewModel
{
    public CustomerBrowserViewModel()
    {
        using ChinookContext context = GetContext(noTracking: true);

        ListCollectionView customers = new ListCollectionView(context.Customers.ToList());

        customers.GroupDescriptions.Add(new PropertyGroupDescription("Country"));
        customers.SortDescriptions.Add(new System.ComponentModel.SortDescription("Country", System.ComponentModel.ListSortDirection.Ascending));
        customers.SortDescriptions.Add(new System.ComponentModel.SortDescription("LastName", System.ComponentModel.ListSortDirection.Ascending));

        this.Customers = customers;
    }

    private Customer? _selectedCustomer;

    public Customer? SelectedCustomer
    {
        get { return _selectedCustomer; }
        set
        {
            _selectedCustomer = value;
            using ChinookContext context = GetContext(noTracking: true);
            _selectedCustomer.Invoices = context.Invoices.Where(i => i.CustomerId == _selectedCustomer.CustomerId).ToList();
            OnPropertyChanged();

            this.SelectedInvoice = _selectedCustomer.Invoices.FirstOrDefault();
        }
    }

    private Invoice? _selectedInvoice;

    public Invoice? SelectedInvoice
    {
        get { return _selectedInvoice; }
        set
        {
            _selectedInvoice = value;
            using ChinookContext context = GetContext(noTracking: true);
            if (_selectedInvoice != null)
                _selectedInvoice.InvoiceLines = context.InvoiceLines.Include(i => i.Track).ThenInclude(t => t.Album).ThenInclude(a => a.Artist)
                                                                    .Where(i => i.InvoiceId == _selectedInvoice.InvoiceId).ToList();
            OnPropertyChanged();
        }
    }


    private ListCollectionView _customers;

    public ListCollectionView Customers
    {
        get { return _customers; }
        set
        {
            _customers = value;
            OnPropertyChanged();
        }
    }


}
