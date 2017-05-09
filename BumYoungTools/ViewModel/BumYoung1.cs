using BumYoungTools.Async;
using BumYoungTools.Business;
using BumYoungTools.Extension;
using BumYoungTools.Message;
using BumYoungTools.Model;
using BumYoungTools.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BumYoungTools.ViewModel
{
    public class BumYoung1 : ViewModelBase, IDisposable
    {
        private IConfigManager _config;

        #region UI Control DataSource (Not Edit This Section)
        private string _query = "검색어 입력";
        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                RaisePropertyChanged("Query");
            }
        }
        private string _blogUrl = "tistory.com";
        public string BlogUrl
        {
            get { return _blogUrl; }
            set
            {
                _blogUrl = value;
                RaisePropertyChanged("BlogUrl");
            }
        }

        private string _withoutUrl = "";
        public string WithoutUrl
        {
            get { return _withoutUrl; }
            set
            {
                _withoutUrl = value;
                RaisePropertyChanged("WithoutUrl");
            }
        }

        private bool _st0 = true;
        public bool st0 {
            get { return _st0; }
            set
            {
                _st0 = value;
                RaisePropertyChanged("st0");
            }
        }
        private bool _st1 = false;

        public bool st1
        {
            get { return _st1; }
            set
            {
                _st1 = value;
                RaisePropertyChanged("st1");
            }
        }
        private bool _srchby0 = true;

        public bool srchby0
        {
            get { return _srchby0; }
            set
            {
                _srchby0 = value;
                RaisePropertyChanged("srchby0");
            }
        }
        private bool _srchby1 = false;

        public bool srchby1
        {
            get { return _srchby1; }
            set
            {
                _srchby1 = value;
                RaisePropertyChanged("srchby1");
            }
        }

        //date_option
        private bool _date_option0 = true;

        public bool date_option0
        {
            get { return _date_option0; }
            set
            {
                _date_option0 = value;
                RaisePropertyChanged("date_option0");
            }
        }
        private bool _date_option1 = false;

        public bool date_option1
        {
            get { return _date_option1; }
            set
            {
                _date_option1 = value;
                RaisePropertyChanged("date_option1");
            }
        }
        private bool _date_option2 = false;

        public bool date_option2
        {
            get { return _date_option2; }
            set
            {
                _date_option2 = value;
                RaisePropertyChanged("date_option2");
            }
        }
        private bool _date_option3 = false;

        public bool date_option3
        {
            get { return _date_option3; }
            set
            {
                _date_option3 = value;
                RaisePropertyChanged("date_option3");
            }
        }
        private bool _date_option4 = false;

        public bool date_option4
        {
            get { return _date_option4; }
            set
            {
                _date_option4 = value;
                RaisePropertyChanged("date_option4");
            }
        }
        private bool _date_option5 = false;

        public bool date_option5
        {
            get { return _date_option5; }
            set
            {
                _date_option5 = value;
                RaisePropertyChanged("date_option5");
            }
        }
        private bool _date_option6 = false;

        public bool date_option6
        {
            get { return _date_option6; }
            set
            {
                _date_option6 = value;
                RaisePropertyChanged("date_option6");
            }
        }
        private bool _dup_remove0 = true;
        public bool dup_remove0
        {
            get { return _dup_remove0; }
            set
            {
                _dup_remove0 = value;
                RaisePropertyChanged("dup_remove0");
            }
        }
        // http://stackoverflow.com/questions/20659070/wpf-datepicker-returns-previously-selected-date-using-mvvm
        // DatePicker 에서 DateTime 으로 변환 시 아래와 같은 방법을 취한다.
        // Nullable 은 실제 초기화시 Null 또한 수용 가능함을 나타내는 타입으로 생각된다. 
        // 미쳐 초기화되지 않았을 경우를 대비한다. 매우 적절하다.
        private Nullable<DateTime> _startTime;
        public Nullable<DateTime> startTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                RaisePropertyChanged("startTime");
            }
        }
        private Nullable<DateTime> _endTime;
        public Nullable<DateTime> endTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                RaisePropertyChanged("endTime");
            }
        }

        private bool _dup_remove1 = false;
        public bool dup_remove1
        {
            get { return _dup_remove1; }
            set
            {
                _dup_remove1 = value;
                RaisePropertyChanged("dup_remove1");
            }
        }

        private string search_unit = "10";
        public string Search_unit
        {
            get { return search_unit; }
            set
            {
                search_unit = value;
                RaisePropertyChanged("Search_unit");
            }
        }
        private string index = "0";
        public string Index
        {
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged("Index");
            }
        }
        #endregion

        public ICommand Open { get; private set; }
        public ICommand Turn { get; private set; }

        private void TurnOver(object collector)
        {
            if (!(collector is HistoryRepository))
                return;
            History = new AsyncObservableCollection<SearchHistory>(((HistoryRepository)collector).getHistory());
        }

        private static void OpenBrowser (object item)
        {
            if (!(item is SearchHistory))
                return;
            System.Diagnostics.Process.Start(((SearchHistory)item).artcLink);
        }
        SemaphoreSlim limitConnection = new SemaphoreSlim(5);
        private AsyncObservableCollection<SearchHistory> _history;
        public AsyncObservableCollection<SearchHistory> History
        {
            get { return _history; }
            set
            {
                _history = value;
                RaisePropertyChanged("History");
            }
        }
        private AsyncObservableCollection<HistoryRepository> _cache;
        public AsyncObservableCollection<HistoryRepository> Cache
        {
            get { return _cache; }
            set
            {
                _cache = value;
                RaisePropertyChanged("Cache");
            }
        }

        //public RelayCommand GoSearch { get; private set; }
        public ICommand GoSearch { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public BumYoung1(IConfigManager config) {
            _config = config;
            Messenger.Default.Register<ExitMessage>(this, ReceiveExitMessage);
            _cache = new AsyncObservableCollection<HistoryRepository>(_config.getCachedRepo());
            // 이전 데이터를 불러와서 복구한다.
            Open = new RelayCommand<object>(OpenBrowser);
            Turn = new RelayCommand<object>(TurnOver);
            CopyCommand = new RelayCommand(CopyDataGrid);
            // _config 에 Deserialzed 된 데이터가 존재하는 경우, 이전 데이터를 복구
            // 그렇지 않을 경우 새로운 Collection 을 생성
            // 
            //_history = new AsyncObservableCollection<SearchHistory>();

            GoSearch = new DelegateCommand(() => {
                History = new AsyncObservableCollection<SearchHistory>();
                for (int i = int.Parse(Index); i < int.Parse(Index) + int.Parse(Search_unit); i+= 10) { 
                    Uri _url = NaverUrlGenerate(i);
                    var countBytes = AsyncCommand.Create(token => NaverCrawlerServices.NaverBlogCrawlerAsync(_url, History, limitConnection, token));
                    countBytes.Execute(null);
                }
                Cache.Add(new HistoryRepository(Query, History));
            });
        }
        private void CopyDataGrid() {
            MessageBox.Show("Common");
        }
        private void ReceiveExitMessage(ExitMessage message)
        {
            if (message.Exit)
            {
                IList<HistoryRepository> tmp = new List<HistoryRepository>();
                foreach( var _item in Cache)
                {
                    tmp.Add(new HistoryRepository(_item.Keyword, _item.getHistory().ToList<SearchHistory>()));
                }
                RecordManager manager = new RecordManager(tmp);
                _config.SaveAll(manager);
            }
        }

        private Uri NaverUrlGenerate(int start) {
            /*
                    https://search.naver.com/search.naver?
                    where=post&query=%EC%95%84%EB%AC%B4%EA%B1%B0%EB%82%98
                    &ie=utf8
                    &st=sim&sm=tab_opt&date_from=20030520&date_to=20170426&date_option=7&srchby=all
                    &dup_remove=1&post_blogurl=tistory.com&post_blogurl_without=
                    &nso=so%3Ar%2Ca%3Aall%2Cp%3A1y&mson=0
                    
                    파라메터 분석 
                    query : 검색 키워드 : 아무거나
                    st : 정렬기준 : 최신순(date) | 유사도(sim) (선택)
                    srchby : 영역 : 전체(all) | 제목(title) (선택) 
                    date_option : 기간 설정 (1-8) 
                         (0: 전체, 2: 1일, 3: 1주, 4: 1개월, 6: 6개월, 7: 1년, 8: 기간설정)
                    dup_remove : 중복 제거 : 0 (포함) | 1 (제거)
                    post_blogurl : 블로그 URL (TEXT)
                    post_blogurl_without : 제외 블로그 URL (TEXT)
                    nso : (불명)
                    mson : (불명)
             */
            #region DateOption 
            string _date_option = "";
            if (date_option0 == true)
            {
                _date_option = "0";
            }else if (date_option1 == true)
            {
                _date_option = "2";
            }
            else if (date_option2 == true)
            {
                _date_option = "3";
            }
            else if (date_option3 == true)
            {
                _date_option = "4";
            }
            else if (date_option4 == true)
            {
                _date_option = "6";
            }
            else if (date_option5 == true)
            {
                _date_option = "7";
            }
            else if (date_option6 == true)
            {
                _date_option = "8";
            }
            #endregion

            var dicParams = new Dictionary<string, string> {
            { "query", HttpUtility.UrlEncode(Query, Encoding.UTF8) },
            { "st",  st0 == true ? "date" : "sim" },
            { "date_option", _date_option },
            { "dup_remove", dup_remove0 == true ? "0" : "1" },
            { "post_blogurl", BlogUrl },
            { "post_blogurl_without", WithoutUrl },
            { "date_from",date_option6 == true ? startTime.Value.ToString("yyyyMMdd") : ""},
            { "date_to", date_option6 == true ? endTime.Value.ToString("yyyyMMdd") : "" },
            { "start", start == -1 ? "0" : start.ToString() },
            };
            // https://search.naver.com/search.naver?where=post&query=%EC%95%84%EB%AC%B4%EA%B1%B0%EB%82%98&ie=utf8&st=sim&sm=tab_opt&date_from=20030520&date_to=20170426&date_option=7&srchby=all&dup_remove=1&post_blogurl=tistory.com&post_blogurl_without=&nso=so%3Ar%2Ca%3Aall%2Cp%3A1y&mson=0&start=0
            Uri url = new Uri("https://search.naver.com/search.naver?where=post&query=&ie=utf8&st=sim&sm=tab_opt&date_from=20030520&date_to=20170426&date_option=7&srchby=all&dup_remove=1&post_blogurl=tistory.com&post_blogurl_without=&start=0");
            
            var query_url = url.ExtendQuery(dicParams);
            return query_url;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.

                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~BumYoung1() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
