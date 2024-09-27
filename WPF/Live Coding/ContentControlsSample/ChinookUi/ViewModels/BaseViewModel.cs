using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChinookUi.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    protected ChinookContext GetContext(bool noTracking = false, Action<string> logAction = null)
    {
        DbContextOptionsBuilder<ChinookContext> builder = new DbContextOptionsBuilder<ChinookContext>()
                                                                .UseSqlServer(Properties.Settings.Default.ChinookConnection);

        if (noTracking)
        {
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        else
        {
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        if (logAction != null)
        {
            builder.LogTo(logAction, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        return new ChinookContext(builder.Options);
    }

}
