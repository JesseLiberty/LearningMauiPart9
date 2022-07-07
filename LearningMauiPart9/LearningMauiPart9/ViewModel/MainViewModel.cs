using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningMauiPart9.Model;
using LearningMauiPart9.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.Input;

namespace LearningMauiPart9.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        public ZipCodeService _zipCodeService;

        [ObservableProperty]
        public string _title;
        

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        public bool _isBusy;

        public bool IsNotBusy => !IsBusy;
        

        //[ObservableProperty]
        //public List<Result> _resultList;

        public ObservableCollection<Result> Results { get; } = new();

        public MainViewModel(ZipCodeService _zipCodeService)
        {
            Title = "Zip Finder";
            this._zipCodeService = _zipCodeService;
        }

        [ICommand]
        public async Task GetZipCodesAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var results = await _zipCodeService.GetResults();
                
                if (Results.Count != 0)
                    Results.Clear();

                foreach (var zipCode in results)  // consider observableRangeCollection
                    Results.Add(zipCode);

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Unable to get results!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }

}
