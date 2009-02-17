using System.Collections.Generic;
using System.IO;
using System.Windows;
using IronTwit.Models;
using Yedda;
using Newtonsoft.Json;

namespace IronTwit
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Yedda.Twitter twit = new Twitter();

            var resultString = twit.GetUserTimeline("darkxanthos@gmail.com", "You Cant Have My Password!", Twitter.OutputFormatType.JSON);
            var str = new StringReader(resultString);
            var converter = new JsonSerializer();
            converter.MissingMemberHandling = MissingMemberHandling.Ignore;
            var obj = (List<TwitObj>)converter.Deserialize(str, typeof(List<TwitObj>));

            TwitterListView.ItemsSource = obj;
        }
    }
}
