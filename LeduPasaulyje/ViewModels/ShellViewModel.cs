using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyje.ViewModels
{
   public class ShellViewModel : Conductor<object>
    {
        private FirstChildViewModel firstChildVM; 
        public ShellViewModel(FirstChildViewModel firstChildVM)
        {
            this.firstChildVM = firstChildVM;
            ActivateItem(this.firstChildVM);
        }
        public void LoadTestView()
        {
            ActivateItem(new TestViewModel());
        }
        public void LoadFirstChild()
        {
            ActivateItem(new FirstChildViewModel());
        }
    }
}
