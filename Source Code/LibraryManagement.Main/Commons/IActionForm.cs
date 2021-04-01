using System;

namespace LibraryManagement.Commons {
    public interface IActionForm {
        void showMessage(string msg);
        void openNewForm(Object obj);
    }
}
