using ListViewXamarin.Helpers;
using ListViewXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        WebAPIService WebAPIService;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Order> items;

        //Two-way binding process on Propertychange Event
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int intTotalCount { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<Order> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                RaisepropertyChanged("Items");
            }
        }
        //private ObservableCollection<Order> Items { get; set; } = new ObservableCollection<Order>();
        //public ObservableCollection<Order> _Items
        //{
        //    get
        //    {
        //        return _Items;
        //    }
        //    set
        //    {
        //        _Items = value;
        //        OnPropertyChanged();
        //        RaisepropertyChanged("Items");
        //    }
        //}
        //To Declare Count Variable
        private string strtotalRecordCount = "";
        public string TotalRecordCount
        {
            get { return strtotalRecordCount; }
            set
            {
                strtotalRecordCount = value;
                RaisepropertyChanged("TotalRecordCount");
            }
        }
        #endregion

        #region Constructor
        public ViewModel()
        {
            try
            {
                WebAPIService = new WebAPIService();
                //Item source which needs to be displayed on the list view.
                Items = new ObservableCollection<Order>();
                GetDataFromWebAPI();
                if(Items.Count != null) intTotalCount = Items.Count;
                TotalRecordCount = "Total Records" + " (" + intTotalCount + ")";

            }
            catch (Exception ex)
            {
                App.Current.MainPage?.DisplayAlert("Error", ex.Message, "ok");
            }
        }
        #endregion

        #region Methods 
        async void GetDataFromWebAPI()
        {
            Items = await WebAPIService.RefreshDataAsync();

        }
        void RaisepropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}