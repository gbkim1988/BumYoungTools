using BumYoungTools.Async;
using BumYoungTools.Business;
using BumYoungTools.Extension;
using BumYoungTools.Model;
using BumYoungTools.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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
    public class BumYoung1 : ViewModelBase
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
        #endregion

        public ICommand Open { get; private set; }

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

        public BumYoung1(IConfigManager config) {
            _config = config;
            Open = new RelayCommand<object>(OpenBrowser);
            // _config 에 Deserialzed 된 데이터가 존재하는 경우, 이전 데이터를 복구
            // 그렇지 않을 경우 새로운 Collection 을 생성
            // 
            //_history = new AsyncObservableCollection<SearchHistory>();
            _cache = new AsyncObservableCollection<HistoryRepository>();
            GoSearch = new DelegateCommand(() => {
                History = new AsyncObservableCollection<SearchHistory>();
                Cache.Add(new HistoryRepository(Query, new List<SearchHistory>()));
                for (int i = 990; i < 1000; i+= 10) { 
                    Uri _url = NaverUrlGenerate(i);
                    var countBytes = AsyncCommand.Create(token => NaverCrawlerServices.NaverBlogCrawlerAsync(_url, History, limitConnection, token));
                    countBytes.Execute(null);
                }
            });
        }

        private void NaverSearch()
        {
            /*
            DateTime myDate = new DateTime();
            try {
                MessageBox.Show(startTime);
                myDate = DateTime.ParseExact(startTime, "M/dd/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                MessageBox.Show(myDate.ToString());
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            */
            // 검색 시작 시 아래와 같은 절차를 거친다. 
            // 1. Control 의 속성을 통해 URL 을 생성한다. 
            // 2. 비동기 요청을 전송한다.        

            // 1. URL 생성 
           
            // Control 로 부터 정보를 수집 및 검색 URL 반환

            // URL 에 대한 조회를 비동기로 추가
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

        ~BumYoung1()
        {
            // To Do : AsyncObservableCollection<SearchHistory> -> ConfigManager 
            //         로 데이터 전달 및 저장
            _config.SaveAll();
        }

    }
}
