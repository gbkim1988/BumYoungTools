using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Business
{
    public interface IRecordManager
    {
        /// <summary>
        ///  문제 상황 정의 
        ///  IList<IHistoryRepository> 타입에 IList<HistoryRepository> 할당 및 
        ///  반환이 가능한가 ? 
        ///  인터페이스를 사용하는 이유는 ?
        ///  -> 
        /// </summary>
        IList<IHistoryRepository> getCollection();
    }
}
