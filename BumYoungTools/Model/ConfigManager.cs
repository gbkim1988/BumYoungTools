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
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = string.Concat(fname + ".by");
            _dataFile = Path.Combine(baseDir, fileName);

            Deserialize();
        }

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
                Manager = new RecordManager(new List<IHistoryRepository>());
        }
    }
}
