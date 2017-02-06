using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DatabindingFormsApp
{
    public class ListPage : ContentPage
    {
        private ObservableCollection<string> Items;
        public ListPage()
        {
            Title = "List";

            Items = new ObservableCollection<string>();

            var list = new ListView();
            list.ItemsSource = Items;

            list.ItemSelected += (sender, args) =>
            {
                if (list.SelectedItem == null) return;

                this.Navigation.PushAsync(new ContentPage
                {
                    Title = "Page 2",
                    Content = new Label
                    {
                        Text = args.SelectedItem as string
                    }
                });

                list.SelectedItem = null;
            };

            var button = new Button
            {
                Text = "Add Time"
            };

            button.Clicked += (sender, args) =>
            {                
                Items.Add(DateTime.Now.ToString("F"));
            };


            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children = { list, button}
            };

            Content = stack;
        }
    }

    public class TimeListPage : TabbedPage
    {
        public TimeListPage()
        {
            this.Title = "Time List";

            Children.Add(new ListPage());

            //Children.Add(new ContentPage
            //{
            //    Title = "Tab 1",
            //    Padding = 10,
            //    Content = new Label
            //    {
            //        Text = "Hello from Tab 1"
            //    }
            //});

            Children.Add(new ContentPage
            {
                Title = "Tab 2",
                Padding = 10,
                Content = new Label
                {
                    Text = "Hello from Tab 2"
                }
            });
        }
    }
}

