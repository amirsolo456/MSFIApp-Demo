using MSFIApp.Services.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.Dtos.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected readonly IMessageService _messageService;

        public BaseViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public Task ShowMessageAsync(Exception ex, int duration = 3000, string Context = null)
        {
            return _messageService.ShowMessageAsync(ex.Message, duration, Context);
        }

    }
}
