using BumYoungTools.Async;
using BumYoungTools.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace BumYoungTools.Service
{
    public static class NaverCrawlerServices
    {
        public static async Task<int> NaverBlogCrawlerAsync(Uri url, 
            AsyncObservableCollection<SearchHistory> _history,
            SemaphoreSlim sem,
            bool isEnd,
            CancellationToken token = new CancellationToken())
        {
            string _url = HttpUtility.UrlDecode(url.ToString());
            int nonActiveCount = 0;

            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            // 세마포어를 이용해서 동시 접속 횟수 제한 적용
            await sem.WaitAsync();
            
            // https://d-fens.ch/2016/12/27/howto-set-cookie-header-on-defaultrequestheaders-of-httpclient/
            using (var httpClient = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                httpClient.BaseAddress = url;
                httpClient.DefaultRequestHeaders.Add("Cookie", "nx_ssl=2; NNB=O7YFEWW5PMGVS; npic=AufcQZ+cQOyIo5d6SRBVMR/Gzz4Gj5OPsNzQyXGgclR3ZYehGMYAYJl3q4lfK3+uCA==; page_uid=Td2AElpVuEVsssmrPKssssssssl-163723; nx_open_so=1");
                httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, sdch, br");
                httpClient.DefaultRequestHeaders.Add("Accept-Language", "ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4");
                using (var response = await httpClient.GetAsync(_url, token).ConfigureAwait(false))
                {
                    string stReadData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    string status = response.StatusCode.ToString();
                    HtmlDocument htmlDoc = new HtmlDocument();
                    // HTML 로드
                    htmlDoc.LoadHtml(stReadData);

                    for (int i = 1; i < 11; i++)
                    {
                        HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode(string.Format("//*[@id='sp_blog_{0}']", i));
                        string title = "";
                        string link = "";
                        string date = "";
                        string summary = "";
                        bool isActive = true;
                        // 제목 
                        try
                        {
                            title = node.SelectSingleNode("dl/dt/a").InnerText;
                            link = node.SelectSingleNode("dl/dt/a").Attributes["href"].Value;
                            date = node.SelectSingleNode("dl/dd[1]/text()").InnerHtml;
                            summary = node.SelectSingleNode("dl/dd[2]").InnerText;

                            // URL 재조립
                            Uri _blogLink = new Uri(link);
                            string _strBlogLink = _blogLink.Scheme + Uri.SchemeDelimiter + _blogLink.Host + ":" + _blogLink.Port;

                            using (var subClient = new HttpClient(new HttpClientHandler { UseCookies = false }))
                            using (var stat = await subClient.GetAsync(_strBlogLink, token).ConfigureAwait(false))
                            {
                                await stat.Content.ReadAsStringAsync().ConfigureAwait(false);
                                if (stat.StatusCode == HttpStatusCode.NotFound)
                                    isActive = false;
                            }
                            if (isActive == false) {
                                
                                if ( _history.Where(item => !item.artcLink.Contains(_strBlogLink)).Count() == 0 ) 
                                    _history.Add(new SearchHistory(title, date, summary, link, isActive, null));
                            }
                            

                        }
                        catch
                        {
                            // 모든 Exception 날리기
                        }
                        finally
                        {
                            if (isEnd)
                                MessageBox.Show("검색이 완료되었습니다.", "알림");
                        }

                    }
                }
            }
            sem.Release();


            return nonActiveCount;
        }
    }
}
