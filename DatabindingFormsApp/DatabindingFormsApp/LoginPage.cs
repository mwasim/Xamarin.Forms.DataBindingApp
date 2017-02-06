using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DatabindingFormsApp
{
    public class UsernameViewModel : INotifyPropertyChanged
    {
        private INavigation navigation;
        public UsernameViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value) return;

                _username = value;

                OnPropertyChanged("Username"); //subscribe to property changed notification
                OnPropertyChanged("Display"); //Also update Display property
            }
        }

        //public string Display
        //{
        //    get { return $"Hello there: {_username}"; }
        //}

        public string Display => $"Hello there: {_username}";


        private Command<string> loginCommand;

        public Command LoginCommand
        {
            get { return loginCommand ?? (loginCommand = new Command<string>(ExecuteLoginCommand, CanLogin)); }
        }

        private void ExecuteLoginCommand(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            navigation.PushAsync(new TimeListPage());

            //navigation.PushAsync(new ContentPage
            //{
            //    Title = "User Loggedin",

            //});
        }

        private bool CanLogin(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null) return; //if there is no subscription, return

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            this.BindingContext = new UsernameViewModel(Navigation);

            var entry = new Entry
            {
                Placeholder = "Enter Username:"
            };

            entry.SetBinding<UsernameViewModel>(Entry.TextProperty,
                vm => vm.Username, BindingMode.TwoWay);

            var label = new Label {};
            label.SetBinding<UsernameViewModel>(Label.TextProperty,
                vm=>vm.Display);

            var button = new Button
            {
                Text =  "Login",
            };

            button.SetBinding<UsernameViewModel>(Button.CommandProperty, 
                vm=>vm.LoginCommand);

            button.SetBinding<UsernameViewModel>(Button.CommandParameterProperty,
                vm => vm.Username);

            Content = new StackLayout
            {
                Padding = 10,
                Spacing = 10,
                Children = { entry, label, button}
            };
        }
    }
}
