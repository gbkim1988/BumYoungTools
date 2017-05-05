using BumYoungTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Business
{
    /// <summary>
    /// NaverSearch 목록을 저장
    /// </summary>
    [Serializable]
    public class HistoryRepository : IHistoryRepository
    {
        private Guid _id;
        private string SearchKeyword;
        private IList<ISearchHistory> _HistoryCollection;

        public HistoryRepository(string keyword, IList<ISearchHistory> collection) {
            SearchKeyword = keyword;
            _HistoryCollection = collection;
        }


    }
}
