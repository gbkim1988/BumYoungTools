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
        private readonly IList<HistoryRepository> _HistoryCollection;
        public RecordManager(IList<HistoryRepository> history) {
            _HistoryCollection = history;
        }

        public IList<HistoryRepository> HistoryCollection
        {
            get { return _HistoryCollection; }
        }

        public IList<HistoryRepository> getCollection() {
            return HistoryCollection;
        }
    }
}
