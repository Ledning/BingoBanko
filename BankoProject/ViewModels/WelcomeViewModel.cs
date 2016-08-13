﻿using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BankoProject.Models;

namespace BankoProject.ViewModels
{
  class WelcomeViewModel : Screen, IMainViewItem
  {
    private IWindowManager _winMan;
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;
    

    public WelcomeViewModel()
    {

    }

    protected override void OnViewReady(object view)
    {
      _winMan = IoC.Get<IWindowManager>();
      _events = IoC.Get<IEventAggregator>();
      _bingoEvent = IoC.Get<BingoEvent>();
    }


    public void CreateEvent()
    {

      bool? result = _winMan.ShowDialog(new CreateEventViewModel()); //Når du skal lave viewet til nye events ledning, skal du kalde den som sådan en her
                                                                            //Erstat dialogviewmodel med det du har lavet, og husk at kalde _bingoevent.initialize i din viewmodel somewhere, efter data er indtastet
      

    }


    public void OpenFileDialog()
    {
      var ofd = new Microsoft.Win32.OpenFileDialog()
      {
        Filter = "Event filer (*.bingoEvent)|*bingoEvent"
      };

      if (ofd.ShowDialog() ?? false)
      {
        var d = ofd.FileNames;
      }
    }
  }
}
