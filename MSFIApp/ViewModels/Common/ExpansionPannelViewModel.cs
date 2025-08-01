using MSFIApp.Dtos.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.Common
{
    public partial class ExpansionPannelViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExpansionPannelRow> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ExpansionPannelViewModel()
        {

        }

        private void OnItemClicked(string text)
        {
            System.Diagnostics.Debug.WriteLine($"{text} کلیک شد.");
        }

        bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
