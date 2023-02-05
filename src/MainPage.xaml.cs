﻿using Microsoft.UI.Xaml.Controls;
using static MobileTerminal.Classes.Globals;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MobileTerminal.Classes;

namespace MobileTerminal;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        GetSettings();
    }

    private void GetSettings()
    {
        if (localSettings.Values["Fullscreen"] as string == "true")
        {
            WindowManager.EnterFullScreen(true);
        }
        else
        {
            WindowManager.EnterFullScreen(false);
        }
    }

    private void TabView_Loaded(object sender, RoutedEventArgs e)
    {
        NewTerminalTab();
    }

    private void TabView_AddButtonClick(TabView sender, object args)
    {
        NewTerminalTab();
    }

    private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        sender.TabItems.Remove(args.Tab);
        GC.Collect();
    }

    public void NewTerminalTab()
    {
        CreateNewTab("Terminal", "Pages.Terminal", Symbol.Document);
    }

    private void CreateNewTab(string header, object pageTag, Symbol icon)
    {
        TabViewItem newItem = new()
        {
            Header = header,
            IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = icon }
        };

        Frame frame = new();
        newItem.Content = frame;
        frame.Navigate(Type.GetType($"MobileTerminal.{pageTag}"));

        Tabs.TabItems.Add(newItem);
        Tabs.SelectedItem = newItem;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        RuntimeManager.ExitApp();
    }

    private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {
        switch ((sender as MenuFlyoutItem).Tag)
        {
            case "NewTab":
                NewTerminalTab();
                break;
            case "NewWindow":
                await WindowManager.OpenPageAsNewWindowAsync(typeof(MainPage));
                break;
            case "OpenHistory":
                CreateNewTab("History", "Pages.History", Symbol.Placeholder);
                break;
            case "OpenSettings":
                CreateNewTab("Settings", "Pages.Settings", Symbol.Setting);
                break;
            case "OpenHelp":
                CreateNewTab("Help", "Pages.Help", Symbol.Help);
                break;
            case "OpenAbout":
                CreateNewTab("About", "Pages.About", Symbol.ContactInfo);
                break;
        }
    }
}
