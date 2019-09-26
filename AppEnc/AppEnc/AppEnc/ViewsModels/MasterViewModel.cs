﻿using AppEnc.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppEnc.ViewsModels
{
    class MasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>(new[]
            {
                    new MenuItem { Id = 0, Title = "Page 1" },
                    new MenuItem { Id = 1, Title = "Page 2" },
                    new MenuItem { Id = 2, Title = "Page 3" },
                    new MenuItem { Id = 3, Title = "Page 4" },
                    new MenuItem { Id = 4, Title = "Page 5" },
                });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}