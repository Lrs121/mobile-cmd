﻿using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Command_Prompt.Pages
{
    public sealed partial class Settings : Page
    {
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public Settings()
        {
            this.InitializeComponent();
            GetSettings();
        }

        private void GetSettings()
        {
            string Font = localSettings.Values["Font"] as string;
            if (Font != null)
            {
                FontSelection.SelectedItem = Font;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Font = e.AddedItems[0].ToString();
            localSettings.Values["Font"] = Font;
        }
    }
}
