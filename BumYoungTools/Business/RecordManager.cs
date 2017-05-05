using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Business
{
    [Serializable]
    public class RecordManager : IRecordManager
    {
        private readonly IList<IHistoryRepository> _HistoryCollection;
        public RecordManager() {

        }
    }
}
