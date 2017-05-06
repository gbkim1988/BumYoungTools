using BumYoungTools.Business;
using BumYoungTools.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BumYoungTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(HttpUtility.UrlEncode("검색 고고", Encoding.UTF8));
            // SearchHistory 생성 및 Collection 등록
            ObservableCollection<SearchHistory> aa = new ObservableCollection<SearchHistory>();
            // HistoryRepository 초기화 
            for (int i =0; i < 1000; i++) {
                SearchHistory a = new SearchHistory();
                a.artcSummary = i.ToString();   
                // 추가
                aa.Add(a);
            }
            // ObservableCollection -> IList 할당 가능?
            // ToList 메서드를 이용해보자.
            
            IList<SearchHistory> aaa = aa.ToList();
            HistoryRepository bb = new HistoryRepository("Test1", aaa);
            HistoryRepository bbb = new HistoryRepository("Test2", aaa);
            IList<HistoryRepository> cc = new List<HistoryRepository>();
            cc.Add(bb);
            cc.Add(bbb);
            // 
            RecordManager dd = new RecordManager(cc);
            // ConfigManager 생성
            ConfigManager ee = new ConfigManager("test");
            Console.WriteLine("Config Manager가 없는 경우");
            //파일을 Default 로 생성하지 않는다. 
            Console.WriteLine("ConfigManager.SaveAll 메서드 호출의 경우");
            // 데이터 저장 확인 완료
            //ee.SaveAll(dd);

            // 다시 데이터를 불러오기 
            Console.WriteLine("ConfigManager로 부터 데이터를 불러오기");
            IRecordManager ff = ee.RecordManager;
            IList<HistoryRepository> gg = ff.getCollection();
            foreach (var repo in gg)
            {
                Console.WriteLine(repo.getKeyword());
                foreach (var item in repo.getHistory())
                {
                    Console.WriteLine(item.artcSummary);
                }
            }

            ee.SaveAs("Gurara");
        }
            
    }
}
