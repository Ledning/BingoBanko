using BankoProject.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Tools
{
  public interface ISave
  {
    bool SaveSession(ControlOptions COptoins, BingoNumberBoard BNBoard, string key); // Save all these to a text file or a database or something. dosent matter that much, nobody is gonna be snoopin around
  }
  public interface ILoad
  {
    bool LoadSession( ref ControlOptions COptoins, ref BingoNumberBoard BNBoard, string key); //Load based on the key. Make function in flyout to select from the different savefiles found automatically. This relies on coptions and bnboard being refs to the master viewmodel
  }
  public interface ISaveTimer
  {
    bool SaveTimer(BindableCollection<Deltagere> deltagere, int timer, string name);
  }
  public interface ILoadTimer
  {
    bool LoadTimer(ref BindableCollection<Deltagere> deltagere, ref int timer, string name); //again based on the fact that deltagere and timer is references. Just input loaded info from the string into there and voila the view should update.
  }
}
