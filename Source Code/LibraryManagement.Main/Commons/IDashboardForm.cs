using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Commons {
    public interface IDashboardForm {
        void showMessage(string msg);

        void InitForm();
    }
}
