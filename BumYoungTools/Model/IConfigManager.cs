using BumYoungTools.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Model
{
    public interface IConfigManager
    {
        void SaveAll();
        void SaveAll(IRecordManager _manger);
        void SaveAs(string fname);
    }
}
