using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tic_tac_toe.Matchmaking;


namespace Tic_tac_toe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, Matchmaking.IServiceMatchmakingCallback
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
                ConnectPlayer();
            else
                DisconnectPlayer();
        }

        void ConnectPlayer()
        {
            client = new ServiceMatchmakingClient(new System.ServiceModel.InstanceContext(this));
            var gameInformation = client.Connect(nickname.Text);
            _lobbyID = gameInformation.Item1;
            _myID = gameInformation.Item2;
            bool _isGame = gameInformation.Item3;
            nickname.IsEnabled = false;

            if (_isGame)
                playing_field.IsEnabled = true;
            
            isConnected = true;
        }

        void DisconnectPlayer()
        {
           
            client.Disconnect(_lobbyID);
            client = null;

            nickname.IsEnabled = true;
            playing_field.IsEnabled = false;
            isConnected = false;
        }

        public void StartGame()
        {
            playing_field.IsEnabled = true;
        }

        public void MoveCallback(int move)
        {
            var button = (Button)FindName("b" + move.ToString());
            button.Content = "X";
            button.IsEnabled = false;
            playing_field.IsEnabled = true;
        }

        private void move_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            var move = int.Parse(button.Name.Substring(1));
            client.SendMove(move, _lobbyID, _myID);

            button.Content = "O";
            button.IsEnabled = false;
            playing_field.IsEnabled = false;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectPlayer();
        }



        ServiceMatchmakingClient client;
        int _lobbyID;
        int _myID;

        bool isConnected = false;   
    }
}

