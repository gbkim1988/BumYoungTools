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
        public RecordManager(IList<IHistoryRepository> history) {
            _HistoryCollection = history;
        }

        public IList<IHistoryRepository> HistoryCollection
        {
            get { return _HistoryCollection; }
        }

        public IList<IHistoryRepository> getCollection() {
            return HistoryCollection;
        }
    }
}
