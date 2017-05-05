using BumYoungTools.Business;
using BumYoungTools.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BumYoungTools.ViewModel
{
    public class BumYoung1 : ViewModelBase
    {
        private readonly IBumYoung1Profile _profile;
        public BumYoung1(IBumYoung1Profile profile) {
            _profile = profile;

            _profile.LoadProfile();
        }
    }
}
