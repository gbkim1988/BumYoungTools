using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BumYoungTools.Model
{
    /// <summary>
    /// 검색 결과를 관리하는 클래스
    /// </summary>
    [Serializable]
    public class SearchHistory : Notifier
    {
        
        private string _artcTitle; // 제목
        private string _artcDate; // 날짜
        private string _artcSummary; // 요약
        private string _artcLink; // 링크
        private bool _artcActive; // 링크 활성화 여부
        private MemoryStream _artcThumbImg; // 썸네일 이미지

        /// <summary>
        /// 
        /// </summary>
        public SearchHistory() :this(
            string.Empty, 
            string.Empty, 
            string.Empty, 
            string.Empty, 
            true, 
            null)  {

        }

        public SearchHistory(
            string title, 
            string date, 
            string summary, 
            string link, 
            bool active, 
            MemoryStream thumb) {

            _artcTitle = title;
            _artcDate = date;
            _artcSummary = summary;
            _artcActive = active;
            _artcThumbImg = thumb;
        }

        /// <summary>
        /// 블로그 타이틀
        /// </summary>
        public string artcTitle {
            get { return _artcTitle; }
            set
            {
                _artcTitle = value;
                RaisePropertyChanged("artcTitle");
            }
        }

        /// <summary>
        /// 블로그 날짜
        /// </summary>
        public string artcDate
        {
            get { return _artcDate; }
            set
            {
                _artcDate = value;
                RaisePropertyChanged("artcDate");
            }
        }

        /// <summary>
        /// 블로그 요약
        /// </summary>
        public string artcSummary
        {
            get { return _artcSummary; }
            set
            {
                _artcSummary = value;
                RaisePropertyChanged("artcSummary");
            }
        }

        /// <summary>
        /// 블로그 링크
        /// </summary>
        public string artcLink
        {
            get { return _artcLink; }
            set
            {
                _artcLink = value;
                RaisePropertyChanged("artcLink");
            }
        }

        public bool artcActive
        {
            get { return _artcActive; }
            set
            {
                _artcActive = value;
                RaisePropertyChanged("artcActive");
            }
        }
        public MemoryStream artcThumImg
        {
            get { return _artcThumbImg; }
            set
            {
                _artcThumbImg = value;
                RaisePropertyChanged("artcThumImg");
            }
        }
    }
}
