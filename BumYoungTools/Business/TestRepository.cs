using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools.Business
{
    class TestRepository : ITestRepository
    {
        private string test;

        public TestRepository() {
            this.test = "Gura";
        }
        public string Test() {
            return this.test;
        }
    }
}
