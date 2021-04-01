using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using LibraryManagement.Commons;
using LibraryManagement.DAOs;
using LibraryManagement.DTOs;
using System;

namespace LibraryManagement.Presenters {

    public class AuthenPresenter {
        IFirebaseConfig config = new FirebaseConfig {
            AuthSecret = "clv8HaZI7T8P4JnXiCvKiU85t9mwyx2aJS1hr1O2",
            BasePath = "https://prj321-librarymanagement-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        AuthenManager authen = new AuthenManager();
        ILoginForm loginFormView;
        IRegisterForm registerFormView;

        public AuthenPresenter(ILoginForm loginFormView) {
            this.loginFormView = loginFormView;
        }

        public AuthenPresenter(IRegisterForm registerFormView) {
            this.registerFormView = registerFormView;
        }

        public async void handleLogin() {
            string Email = loginFormView.Email;
            string Password = loginFormView.Password;

            try {
                User user = authen.Login(Email, Password);

                if (user == null) {
                    loginFormView.showMessage("Email or password wrong!");
                    return;
                }

                // record login action to firebase
                client = new FireSharp.FirebaseClient(config);
                var record = new { UserID = user.UserID, RoleID = user.RoleID, Time = DateTime.Now.ToString() };
                SetResponse response = await client.SetTaskAsync(DateTime.Now.ToFileTime().ToString(), record);

                // open dashboard
                loginFormView.openNewForm(user);
            } catch (Exception ex) {
                loginFormView.showMessage(ex.Message);
            }
        }
        public void handleRegister() {
            string UserName = registerFormView.UserName;
            string Email = registerFormView.Email;
            string Password = registerFormView.Password;

            try {
                User user = authen.Register(new User(-1, 1, UserName, Email, Password, true));

                if (user == null) {
                    registerFormView.showMessage("Register fail!");
                    return;
                }

                registerFormView.openNewForm(user);

            } catch (Exception ex) {
                registerFormView.showMessage(ex.Message);
            }
        }
    }
}
