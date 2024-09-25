using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControlLib;

/// <summary>
/// Interaktionslogik für NumericUpDown.xaml
/// </summary>
public partial class NumericUpDown : UserControl
{
    private Regex _numMatch = new Regex(@"^-?\d+$"); // Wirklich nur Zahlen?


    public NumericUpDown()
    {
        InitializeComponent();

        this.Maximum = 10;
        this.Minimum = 0;
        //this.Value = 0;

        txtValue.Text = "0";
    }

    #region Events der Controls
    private void btnUp_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        this.Value++;
        RaiseEvent(new RoutedEventArgs(ValueIncreasedEvent));
    }

    private void btnDown_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        this.Value--;
        RaiseEvent(new RoutedEventArgs(ValueDecreasedEvent));
    }

    private void tvtValue_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.IsDown && e.Key == Key.Up && Value < Maximum)
        {
            Value++;
            RaiseEvent(new RoutedEventArgs(ValueIncreasedEvent));
        }
        else if (e.IsDown && e.Key == Key.Down && Value > Minimum)
        {
            Value--;
            RaiseEvent(new RoutedEventArgs(ValueDecreasedEvent));
        }

    }

    private void txtValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        TextBox tb = (TextBox)sender;
        string text = tb.Text.Insert(tb.CaretIndex, e.Text);

        // Wenn der Text keine Zahl ist, brich die Eingabe hier ab:
        e.Handled = !_numMatch.IsMatch(text);
    }

    private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox tb = (TextBox)sender;

        if (!_numMatch.IsMatch(tb.Text))
        {
            ResetText(tb);
        }

        Value = Convert.ToInt32(tb.Text);
        if (Value < Minimum)
        {
            Value = Minimum;
        }
        if (Value > Maximum)
        {
            Value = Maximum;
        }

    }

    #endregion

    // "Normale" Properties sind nicht bi-diektional bindbar
    //public double Value { get; set; }
    //public double Maximum { get; set; }
    //public double Minimum { get; set; }

    #region DependencyProperties
    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnPropertyChanged));

    public int Maximum
    {
        get { return (int)GetValue(MaximumProperty); }
        set { SetValue(MaximumProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));

    public int Minimum
    {
        get { return (int)GetValue(MinimumProperty); }
        set { SetValue(MinimumProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));

    #endregion

    #region RoutedEvents
    public event RoutedEventHandler ValueIncreased
    {
        add { AddHandler(ValueIncreasedEvent, value); }
        remove { RemoveHandler(ValueIncreasedEvent, value); }
    }

    private static readonly RoutedEvent ValueIncreasedEvent = EventManager.RegisterRoutedEvent("IncreaseClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

    public event RoutedEventHandler ValueDecreased
    {
        add { AddHandler(ValueDecreasedEvent, value); }
        remove { RemoveHandler(ValueDecreasedEvent, value); }
    }

    private static readonly RoutedEvent ValueDecreasedEvent = EventManager.RegisterRoutedEvent("DecreaseClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

    #endregion

    private void ResetText(TextBox textBox)
    {
        textBox.Text = 0 < Minimum ? Minimum.ToString() : "0";
        textBox.SelectAll();
    }


    private static void OnPropertyChanged(DependencyObject depencyObject, DependencyPropertyChangedEventArgs e)
    {
        NumericUpDown numericUpDown = depencyObject as NumericUpDown;
        numericUpDown.txtValue.Text = e.NewValue.ToString();
    }

}
