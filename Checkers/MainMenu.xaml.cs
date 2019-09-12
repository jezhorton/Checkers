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
using System.Windows.Shapes;

namespace CheckersBoard
{


    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow game = new MainWindow();
            game.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TwoPlayer_Clickl(object sender, RoutedEventArgs e)
        {
            MainWindow game = new MainWindow();
            game.testing = true;
            game.Show();
            game.TwoPlayer();
        }

        private void AI_Click(object sender, RoutedEventArgs e)
        {
            MainWindow game = new MainWindow();
            game.testing = true;
            game.Show();
            game.AiGame();
        }
    }
}
