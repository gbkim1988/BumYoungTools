using BumYoungTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Business
{
    public interface IHistoryRepository
    {
        IList<SearchHistory> getHistory();
        string getKeyword();
    }
}
