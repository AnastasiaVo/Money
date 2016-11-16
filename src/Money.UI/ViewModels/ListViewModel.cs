﻿using Neptuo.Observables.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.ViewModels
{
    public class ListViewModel : ViewModel
    {
        private Guid groupId;
        public Guid GroupId
        {
            get { return groupId; }
            set
            {
                if (groupId != value)
                {
                    groupId = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<ListItemViewModel> Items { get; private set; }

        public ListViewModel()
        {
            Items = new ObservableCollection<ListItemViewModel>();
        }
    }
}
