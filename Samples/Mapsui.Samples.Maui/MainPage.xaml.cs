﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using Mapsui.Samples.Common;

namespace Mapsui.Samples.Maui
{
    public partial class MainPage : ContentPage
    {
        IEnumerable<ISample> allSamples;
        Func<object, EventArgs, bool> clicker;

        public MainPage()
        {
            InitializeComponent();
            allSamples = AllSamples.GetSamples();

            var categories = allSamples.Select(s => s.Category).Distinct().OrderBy(c => c);
            picker.ItemsSource = categories.ToList<string>();
            picker.SelectedIndexChanged += PickerSelectedIndexChanged;
            picker.SelectedItem = "Forms";
        }

        private void FillListWithSamples()
        {
            var selectedCategory = picker.SelectedItem?.ToString() ?? "";
            listView.ItemsSource = allSamples.Where(s => s.Category == selectedCategory).Select(x => x.Name);
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            FillListWithSamples();
        }

        private void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var sampleName = e.SelectedItem.ToString();
            var sample = allSamples.Where(x => x.Name == sampleName).FirstOrDefault<ISample>();

            clicker = null;
            if (sample is IFormsSample)
                clicker = ((IFormsSample)sample).OnClick;

            ((NavigationPage)Application.Current.MainPage).PushAsync(new MapPage(sample.Setup, clicker));

            listView.SelectedItem = null;
        }
    }
}