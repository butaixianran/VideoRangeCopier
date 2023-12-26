using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using VideoRangeCopier.Util;

namespace VideoRangeCopier.ViewModels;

public partial class MainViewModel : ViewModelBase
{

    [ObservableProperty]
    private ViewModelBase currentPage;

    private Dictionary<string, ViewModelBase> pages = new Dictionary<string, ViewModelBase>();


    public MainViewModel()
    {
        //Add all view models to pages with custom route names
        pages["home"] = new HomeViewModel();
        pages["merge"] = new MergeViewModel();

        //Set current page to home on start up
        currentPage = pages["home"];

        //delegate GotoPage to Global as Action
        Global.GotoPage = new Action<string>(GotoPage);
    }

    public void GotoPage(string name)
    {
        if (!pages.ContainsKey(name)) { return; }

        CurrentPage = pages[name];
    }

}
