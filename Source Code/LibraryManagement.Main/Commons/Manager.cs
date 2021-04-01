using System.Configuration;

namespace LibraryManagement.Commons {
    public class Manager {
        public string strConnection;

        public Manager() {
            strConnection = ConfigurationManager.ConnectionStrings["LibraryManagement"].ConnectionString;
        }
    }
}
