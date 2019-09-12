using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CheckersBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Move currentMove;
        String winner;
        String turn;
        public string turnAI = "Red";
        public bool testing = false, playervsplayer = false, playervsai = false, aivsai = false; //These are the bools to toggle between the menu items (game modes)

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Checkers! Blacks turn!";//Setting the title of the window
            currentMove = null; //Nulling the current move and winner to make sure there are no values passed in
            winner = null;
            turn = "Black";
            MakeBoard();
        }
        /*=====================================
         * Creating and initialising the chess board
         * =====================================*/
        void ClearBoard()
        {
            for (int r = 1; r < 9; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
                    CheckersGrid.Children.Remove(stackPanel);
                }
            }
        }

        void MakeBoard()
        {
            for (int r = 1; r < 9; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = new StackPanel(); //Creating new stack panels to allow the creation of children of the grid
                    if (r % 2 == 1)
                    {
                        if (c % 2 == 0)
                            stackPanel.Background = Brushes.White;  //Painting the tiles
                        else
                            stackPanel.Background = Brushes.Black;
                    }
                    else
                    {
                        if (c % 2 == 0)
                            stackPanel.Background = Brushes.Black;
                        else
                            stackPanel.Background = Brushes.White;
                    }
                    Grid.SetRow(stackPanel, r);
                    Grid.SetColumn(stackPanel, c);
                    CheckersGrid.Children.Add(stackPanel);
                }
            }

            MakeButtons(); //Calling the making of the buttons at the end to make sure all the buttons are created and the AI and player can access the pieces
        }

        void MakeButtons()
        {
            for (int r = 1; r < 9; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
                    Button button = new Button();
                    button.Click += new RoutedEventHandler(button_Click); //Creating a new button on the black tiles in the grid
                    button.Height = 60;
                    button.Width = 60;
                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    button.VerticalAlignment = VerticalAlignment.Center;
                    var redBrush = new ImageBrush();
                    redBrush.ImageSource = new BitmapImage(new Uri("Resources/red60p.png", UriKind.Relative)); //The location of the images to be applied to the buttons
                    var blackBrush = new ImageBrush();
                    blackBrush.ImageSource = new BitmapImage(new Uri("Resources/black60p.png", UriKind.Relative));
                    switch (r)
                    {
                        case 1:
                            if (c % 2 == 1) //Switch statement to create the buttons with the correct images on each button
                            {

                                button.Background = redBrush; 
                                button.Name = "buttonRed" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 2:
                            if (c % 2 == 0)
                            {
                                button.Background = redBrush;
                                button.Name = "buttonRed" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 3:
                            if (c % 2 == 1)
                            {
                                button.Background = redBrush;
                                button.Name = "buttonRed" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 4:
                            if (c % 2 == 0)
                            {
                                button.Background = Brushes.Black;
                                button.Name = "button" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 5:
                            if (c % 2 == 1)
                            {
                                button.Background = Brushes.Black;
                                button.Name = "button" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 6:
                            if (c % 2 == 0)
                            {
                                button.Background = blackBrush;
                                button.Name = "buttonBlack" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 7:
                            if (c % 2 == 1)
                            {
                                button.Background = blackBrush;
                                button.Name = "buttonBlack" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 8:
                            if (c % 2 == 0)
                            {
                                button.Background = blackBrush;
                                button.Name = "buttonBlack" + r + c;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        UIElement GetGridElement(Grid g, int r, int c)
        {
            for (int i = 0; i < g.Children.Count; i++) //This access all of the stack panels children allowing them to be used elsewhere in the AI
            {
                UIElement e = g.Children[i];
                if (Grid.GetRow(e) == r && Grid.GetColumn(e) == c)
                    return e;
            }
            return null;
        }

        public void button_Click(Object sender, RoutedEventArgs e) //This is the handler for all button presses from the player
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)button.Parent;
            int row = Grid.GetRow(stackPanel); //Access what button is pressed
            int col = Grid.GetColumn(stackPanel);
            Console.WriteLine("Row: " + row + " Column: " + col); //Output to check the buttons are correct locations
            if (currentMove == null)
                currentMove = new Move();
            if (currentMove.piece1 == null)
            {
                currentMove.piece1 = new Piece(row, col);
                stackPanel.Background = Brushes.Green;
            }
            else
            {
                currentMove.piece2 = new Piece(row, col);
                stackPanel.Background = Brushes.Green;
            }
            if ((currentMove.piece1 != null) && (currentMove.piece2 != null)) //This is in reference to the Piece.cs in the file (getters and setters)
            {
                if (playervsplayer) //Bool to check the gamemode
                {
                    if (CheckMove()) //Bool to see who's move it is
                    {
                        MakeMove(); //Method to handle the move and button presses
                    }
                }
                else
                {
                    if (CheckMove())
                    {
                        MakeMove();
                        aiMakeMove(); //Same as above but the ai making the move
                    }
                }
            }
        }

        bool CheckMove()
        {
            StackPanel stackPanel1 = (StackPanel)GetGridElement(CheckersGrid, currentMove.piece1.Row, currentMove.piece1.Column);
            StackPanel stackPanel2 = (StackPanel)GetGridElement(CheckersGrid, currentMove.piece2.Row, currentMove.piece2.Column);
            Button button1 = (Button) stackPanel1.Children[0]; //Accessing the
            Button button2 = (Button) stackPanel2.Children[0];
            stackPanel1.Background = Brushes.Black;
            stackPanel2.Background = Brushes.Black;

            if ((turn == "Black") && (button1.Name.Contains("Red")))
            {
                currentMove.piece1 = null;
                currentMove.piece2 = null;
                if(testing != true)
                {
                    displayError("It is blacks turn.");

                }
                return false;
            }
            if ((turn == "Red") && (button1.Name.Contains("Black")))
            {
                currentMove.piece1 = null;
                currentMove.piece2 = null;
                displayError("It is reds turn.");
                return false;
            }
            if (button1.Equals(button2))
            {
                currentMove.piece1 = null;
                currentMove.piece2 = null;
                return false;
            }
            if(button1.Name.Contains("Black"))
            {
                return CheckMoveBlack(button1, button2);
            }
            else if (button1.Name.Contains("Red"))
            {
                return CheckMoveRed(button1, button2);
            }
            else
            {
                currentMove.piece1 = null;
                currentMove.piece2 = null;
                Console.WriteLine("False");
                return false;
            }
        }

        bool CheckMoveRed(Button button1, Button button2)
        {
            CheckerBoard currentBoard = GetCurrentBoard();
            List<Move> jumpMoves = currentBoard.checkJumps("Red");
            
            if (jumpMoves.Count > 0)
            {
                bool invalid = true;
                foreach (Move move in jumpMoves)
                {
                    if (currentMove.Equals(move))
                        invalid = false;
                }
                if (invalid)
                {
                    displayError("Jump must be taken");
                    currentMove.piece1 = null;
                    currentMove.piece2 = null;
                    Console.WriteLine("False");
                    return false;
                }
            }

            if (button1.Name.Contains("Red"))
            {
                if (button1.Name.Contains("King"))
                {
                    if ((currentMove.isAdjacent("King")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                        return true;
                    Piece middlePiece = currentMove.checkJump("King");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(CheckersGrid, middlePiece.Row, middlePiece.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Black"))
                        {
                            CheckersGrid.Children.Remove(middleStackPanel);
                            AddBlackButton(middlePiece);
                            return true;
                        }
                    }
                }
                else
                {
                    if ((currentMove.isAdjacent("Red")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                        return true;
                    Piece middlePiece = currentMove.checkJump("Red");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(CheckersGrid, middlePiece.Row, middlePiece.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Black"))
                        {
                            CheckersGrid.Children.Remove(middleStackPanel);
                            AddBlackButton(middlePiece);
                            return true;
                        }
                    }
                }
            }
            currentMove = null;
            displayError("Invalid Move. Try Again.");
            return false;
        }

        bool CheckMoveBlack(Button button1, Button button2)
        {
            CheckerBoard currentBoard = GetCurrentBoard();
            List<Move> jumpMoves = currentBoard.checkJumps("Black");

            if (jumpMoves.Count > 0)
            {
                bool invalid = true;
                foreach (Move move in jumpMoves)
                {
                    if (currentMove.Equals(move))
                        invalid = false;
                }
                if (invalid)
                {
                    displayError("Jump must be taken");
                    currentMove.piece1 = null;
                    currentMove.piece2 = null;
                    Console.WriteLine("False");
                    return false;
                }
            }

            if (button1.Name.Contains("Black"))
            {
                if (button1.Name.Contains("King"))
                {
                    if ((currentMove.isAdjacent("King")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                        return true;
                    Piece middlePiece = currentMove.checkJump("King");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(CheckersGrid, middlePiece.Row, middlePiece.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Red"))
                        {
                            CheckersGrid.Children.Remove(middleStackPanel);
                            AddBlackButton(middlePiece);
                            return true;
                        }
                    }
                }
                else
                {
                    if ((currentMove.isAdjacent("Black")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                        return true;
                    Piece middlePiece = currentMove.checkJump("Black");
                    if ((middlePiece != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("Red")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(CheckersGrid, middlePiece.Row, middlePiece.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Red"))
                        {
                            CheckersGrid.Children.Remove(middleStackPanel);
                            AddBlackButton(middlePiece);
                            return true;
                        }
                    }
                }
            }
            currentMove = null;
            displayError("Invalid Move. Try Again.");
            return false;
       }

        void MakeMove()
        {
            if ((currentMove.piece1 != null) && (currentMove.piece2 != null))
            {
                Console.WriteLine("Piece1 " + currentMove.piece1.Row + ", " + currentMove.piece1.Column);
                Console.WriteLine("Piece2 " + currentMove.piece2.Row + ", " + currentMove.piece2.Column);
                StackPanel stackPanel1 = (StackPanel)GetGridElement(CheckersGrid, currentMove.piece1.Row, currentMove.piece1.Column);
                StackPanel stackPanel2 = (StackPanel)GetGridElement(CheckersGrid, currentMove.piece2.Row, currentMove.piece2.Column);
                CheckersGrid.Children.Remove(stackPanel1);
                CheckersGrid.Children.Remove(stackPanel2);
                Grid.SetRow(stackPanel1, currentMove.piece2.Row);
                Grid.SetColumn(stackPanel1, currentMove.piece2.Column);
                CheckersGrid.Children.Add(stackPanel1);
                Grid.SetRow(stackPanel2, currentMove.piece1.Row);
                Grid.SetColumn(stackPanel2, currentMove.piece1.Column);
                CheckersGrid.Children.Add(stackPanel2);
                CheckKing(currentMove.piece2);
                currentMove = null;
                if (turn == "Black")
                {
                    this.Title = "Checkers! Reds turn!";
                    turn = "Red";
                }
                else if (turn == "Red")
                {
                    this.Title = "Checkers! Blacks turn!";
                    turn = "Black";
                }
                CheckWin();
            }
        }

        void aiMakeMove()
        {
            currentMove = CheckersAI.GetMove(GetCurrentBoard());
            if (currentMove != null)
            {
                if (CheckMove())
                {
                    MakeMove();
                }
            }
        }
        void aiMakeMoveBlack()
        {
            currentMove = CheckersAI.GetMoveBlack(GetCurrentBoard());
            if (currentMove != null)
            {
                if (CheckMove())
                {
                    MakeMove();
                }
            }
        }

        CheckerBoard GetCurrentBoard()
        {
            CheckerBoard board = new CheckerBoard();
            for (int r = 1; r < 9; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
                    if (stackPanel.Children.Count > 0)
                    {
                        Button button = (Button)stackPanel.Children[0];
                        if (button.Name.Contains("Red"))
                        {
                            if (button.Name.Contains("King"))
                                board.SetState(r - 1, c, 3);
                            else
                                board.SetState(r - 1, c, 1);
                        }
                        else if (button.Name.Contains("Black"))
                        {
                            if (button.Name.Contains("King"))
                                board.SetState(r - 1, c, 4);
                            else
                                board.SetState(r - 1, c, 2);
                        }
                        else
                            board.SetState(r - 1, c, 0);

                    }
                    else
                    {
                        board.SetState(r - 1, c, -1);
                    }
                }
            }
            return board;
        }

        void CheckKing(Piece tmpPiece)
        {
            StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, tmpPiece.Row, tmpPiece.Column);
            if (stackPanel.Children.Count > 0)
            {
                Button button = (Button)stackPanel.Children[0];
                var redBrush = new ImageBrush();
                redBrush.ImageSource = new BitmapImage(new Uri("Resources/red60p_king.png", UriKind.Relative));
                var blackBrush = new ImageBrush();
                blackBrush.ImageSource = new BitmapImage(new Uri("Resources/black60p_king.png", UriKind.Relative));
                if ((button.Name.Contains("Black")) && (!button.Name.Contains("King")))
                {
                    if (tmpPiece.Row == 1)
                    {
                        button.Name = "button" + "Black" + "King" + tmpPiece.Row + tmpPiece.Column;
                        button.Background = blackBrush;
                    }
                }
                else if ((button.Name.Contains("Red")) && (!button.Name.Contains("King")))
                {
                    if (tmpPiece.Row == 8)
                    {
                        button.Name = "button" + "Red" + "King" + tmpPiece.Row + tmpPiece.Column;
                        button.Background = redBrush;
                    }
                }
            }
        }
        
        void AddBlackButton(Piece middleMove)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Background = Brushes.Black;
            Button button = new Button();
            button.Click += new RoutedEventHandler(button_Click);
            button.Height = 60;
            button.Width = 60;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = Brushes.Black;
            button.Name = "button" + middleMove.Row + middleMove.Column;
            stackPanel.Children.Add(button);
            Grid.SetColumn(stackPanel, middleMove.Column);
            Grid.SetRow(stackPanel, middleMove.Row);
            CheckersGrid.Children.Add(stackPanel);
        }

        void CheckWin()
        {
            int totalBlack = 0, totalRed = 0;
            for (int r = 1; r < 9; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
                    if (stackPanel.Children.Count > 0)
                    {
                        Button button = (Button)stackPanel.Children[0];
                        if (button.Name.Contains("Red"))
                            totalRed++;
                        if (button.Name.Contains("Black"))
                            totalBlack++;
                    }
                }
            }
            if (totalBlack == 0)
                winner = "Red";
            if (totalRed == 0)
                winner = "Black";
            if (winner != null)
            {
                for (int r = 1; r < 9; r++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        StackPanel stackPanel = (StackPanel)GetGridElement(CheckersGrid, r, c);
                        if (stackPanel.Children.Count > 0)
                        {
                            Button button = (Button)stackPanel.Children[0];
                            button.Click -= new RoutedEventHandler(button_Click);
                        }
                    }
                }
                MessageBoxResult result = MessageBox.Show(winner + " is the winner! Would you like to play another?", "Winner", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    newGame();
            }
        }

        public void AiGame()
        {
            currentMove = null;
            winner = null;
            this.Title = "Checkers! Red Turn!";
            turn = "Red";
            ClearBoard();
            MakeBoard();
            Random r = new Random();
            int rint = r.Next(1, 10);
            for (int i = 0; i < 1000; i++)
            {
                //System.Threading.Thread.Sleep(20);
                if (rint > 5)
                {
                    if (i % 2 == 0)
                    {
                        turn = "Black";
                        aiMakeMoveBlack();
                    }
                    else
                    {
                        turn = "Red";
                        aiMakeMove();
                    }
                }
                else if (rint < 5)
                {

                    if (i % 2 == 0)
                    {
                        turn = "Red";
                        aiMakeMove();
                    }
                    else
                    {
                        turn = "Black";
                        aiMakeMoveBlack();
                    }
                }
            }

        }
        public void TwoPlayer()
        {
            currentMove = null;
            winner = null;
            ClearBoard();
            MakeBoard();
            for (int i = 0; i < 1000; i++)
            {
                
                //System.Threading.Thread.Sleep(20);
                    if (i % 2 == 0)
                    {
                        turn = "Black";
                        
                    }
                    else
                    {
                        turn = "Red";
                        
                    }
                

            }

        }
        private async void waiting(int time)
        {
            await Task.Delay(time);
        }

        void newGame()
        {
            currentMove = null;
            winner = null;
            this.Title = "Checkers! Blacks turn!";
            turn = "Black";
            ClearBoard();
            MakeBoard();
        }

        void displayError(string error)
        {
            MessageBox.Show(error, "Invalid Move", MessageBoxButton.OK);
        }

        void newGame_Click(object sender, RoutedEventArgs e)
        {
            if (playervsai == true) { newGame(); }
            else if (aivsai == true) { AiGame(); }
            else if(playervsplayer == true) { TwoPlayer(); }
            
        }

        void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
