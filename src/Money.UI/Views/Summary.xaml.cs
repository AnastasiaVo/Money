﻿using Money.Services.Models;
using Money.ViewModels;
using Money.ViewModels.Parameters;
using Money.Views.Controls;
using Money.Views.Navigation;
using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Money.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [NavigationParameter(typeof(SummaryParameter))]
    public sealed partial class Summary : Page
    {
        private readonly INavigator navigator = ServiceProvider.Navigator;
        private bool isAmountSorted;
        private bool isCategorySorted;

        public SummaryViewModel ViewModel
        {
            get { return (SummaryViewModel)DataContext; }
            set { DataContext = value; }
        }

        public bool IsPieChartPrefered
        {
            get { return (bool)GetValue(IsPieChartPreferedProperty); }
            set { SetValue(IsPieChartPreferedProperty, value); }
        }

        public static readonly DependencyProperty IsPieChartPreferedProperty = DependencyProperty.Register(
            "IsPieChartPrefered",
            typeof(bool),
            typeof(Summary),
            new PropertyMetadata(false)
        );

        public bool IsBarGraphPrefered
        {
            get { return (bool)GetValue(IsBarGraphPreferedProperty); }
            set { SetValue(IsBarGraphPreferedProperty, value); }
        }

        public static readonly DependencyProperty IsBarGraphPreferedProperty = DependencyProperty.Register(
            "IsBarGraphPrefered",
            typeof(bool),
            typeof(Summary),
            new PropertyMetadata(false)
        );

        public Summary()
        {
            InitializeComponent();
            ViewModel = new SummaryViewModel(ServiceProvider.Navigator, ServiceProvider.QueryDispatcher);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SummaryParameter parameter = (SummaryParameter)e.Parameter;
            switch (parameter.ViewType)
            {
                case SummaryViewType.PieChart:
                    IsPieChartPrefered = true;
                    IsBarGraphPrefered = false;
                    break;
                case SummaryViewType.BarGraph:
                    IsPieChartPrefered = false;
                    IsBarGraphPrefered = true;
                    break;
            }

            if (grpGroups.SelectedItem != null)
                OnPeriodChanged();
        }

        private void OnGroupSelectedItemChanged(object sender, SelectedItemEventArgs e)
        {
            if (ViewModel != null)
                OnPeriodChanged();
        }

        private void OnPeriodChanged()
        {
            MonthModel month = grpGroups.SelectedItem as MonthModel;
            if (month != null)
            {
                ViewModel.Month = month;
                return;
            }

            YearModel year = grpGroups.SelectedItem as YearModel;
            if (year != null)
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        private void lvwBarGraph_ItemClick(object sender, ItemClickEventArgs e)
        {
            SummaryItemViewModel item = (SummaryItemViewModel)e.ClickedItem;
            OpenOverview(item.CategoryKey);
        }

        private void lviSummary_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OpenOverview(KeyFactory.Empty(typeof(Category)));
        }

        private void OpenOverview(IKey categoryKey)
        {
            CategoryOverviewParameter parameter = null;

            MonthModel month = grpGroups.SelectedItem as MonthModel;
            if (month != null)
                parameter = new CategoryOverviewParameter(categoryKey, month);

            YearModel year = grpGroups.SelectedItem as YearModel;
            if (year != null)
                parameter = new CategoryOverviewParameter(categoryKey, year);

            navigator
                .Open(parameter)
                .Show();
        }

        private void mfiSortAmount_Click(object sender, RoutedEventArgs e)
        {
            if (isAmountSorted)
            {
                ViewModel.Items.SortDescending(i => i.AmountValue);
                isAmountSorted = false;
            }
            else
            {
                ViewModel.Items.Sort(i => i.AmountValue);
                isAmountSorted = true;
            }

            isCategorySorted = false;
        }

        private void mfiSortCategory_Click(object sender, RoutedEventArgs e)
        {
            if (isCategorySorted)
            {
                ViewModel.Items.SortDescending(i => i.Name);
                isCategorySorted = false;
            }
            else
            {
                ViewModel.Items.Sort(i => i.Name);
                isCategorySorted = true;
            }

            isAmountSorted = false;
        }
    }
}
