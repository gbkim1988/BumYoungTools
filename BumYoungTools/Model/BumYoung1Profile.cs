using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace BumYoungTools.Model
{

    public class BumYoung1Profile : IBumYoung1Profile
    {
        /*
            Serialize 하여 데이터를 저장하고 로드하도록 
            Model 클래스를 구성, 이전 데이터 저장이 가능하도록 구성
         */
        public BumYoung1Profile() {

            
        }

        public void LoadProfile() {
            //ConfigPath = new XMLConfig(); // ["BumYoungTools.Properties.Settings.ConfigFilePath"];

        }
        
    }
}
