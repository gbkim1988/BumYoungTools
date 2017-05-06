using BumYoungTools.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Model
{
    public class ConfigManager : Notifier, IConfigManager
    {
        private IRecordManager Manager;
        private string _dataFile;
        public ConfigManager(string fname)
        {
            // baseDir 변경 필요 권한 문제로 인해 설정 파일 저장 불가
            string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = string.Concat(fname + ".by");
            _dataFile = Path.Combine(baseDir, fileName);

            Deserialize();
        }z

        private void Serialize()
        {
            using (var stream = File.Open(_dataFile, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, Manager);
            }
        }

        public void SaveAll()
        {
            Serialize();
        }

        public void SaveAll(IRecordManager _manger)
        {
            Manager = _manger;

            Serialize();
        }

        public void SaveAs(string fname)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = string.Concat(fname + ".by");
            _dataFile = Path.Combine(baseDir, fileName);

            Serialize();
        }
        /// <summary>
        /// IList<HistoryRepository> 컬렉션을 반환한다. 
        /// 캐시된 내용을 포함한다.
        /// </summary>
        /// <returns></returns>
        public IList<HistoryRepository> getCachedRepo()
        {
            return RecordManager.getCollection();
        }
        public IRecordManager RecordManager
        {
            get { return Manager; }
            set
            {
                Manager = value;
                RaisePropertyChanged("RecordManager");
            }
        }


        private void Deserialize()
        {
            if (File.Exists(_dataFile))
                using (var stream = File.Open(_dataFile, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    Manager = (IRecordManager)formatter.Deserialize(stream);
                }
            else
                Manager = new RecordManager(new List<HistoryRepository>());
        }
    }
}
