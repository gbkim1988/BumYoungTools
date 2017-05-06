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
    public class HistoryRepository : Notifier, IHistoryRepository
    {
        private Guid _id;
        private string _keyword;
        private IList<ISearchHistory> _historyCollection;

        public HistoryRepository(string keyword, IList<ISearchHistory> collection)
        {
            _keyword = keyword;
            _historyCollection = collection;
            _id = Guid.NewGuid();
        }

        public IList<ISearchHistory> History
        {
            get { return _historyCollection; }
            private set
            {
                // 수정 방지를 설정함 
                _historyCollection = value;
                RaisePropertyChanged("History");
            }
        }

        public Guid Id
        {
            get { return _id; }
            private set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            var other = obj as HistoryRepository;
            return other != null && other.Id == Id && other.Keyword == Keyword;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
