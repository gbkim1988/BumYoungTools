using BumYoungTools.Business;
using BumYoungTools.Model;
using BumYoungTools.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BumYoungTools
{
    public class BootStrapper
    {
        public IUnityContainer Container { get; set; }

        public BootStrapper()
        {
            Container = new UnityContainer();

            ConfigureContainer();
        }

        /// <summary>
        /// We register here every service / interface / viewmodel.
        /// </summary>
        private void ConfigureContainer()
        {
            //Container.RegisterInstance<INoteRepository>(new NoteRepository("notes"));
            //Container.RegisterInstance<ICategoryRepository>(new CategoryRepository("categories"));
            Container.RegisterInstance<IBumYoung1Profile>(new BumYoung1Profile());
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<BumYoung1>();
        }
    }
}
