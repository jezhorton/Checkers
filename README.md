# Checkers
A game of checkers! 
## Overview
Checkers built using WPF in .NET, this project has a simple AI to play against. It features a clear game function.

## First Segment
During the completion of the first segment the first steps were laid out for completion. During the first time stamps the tasks were:
1. Create the board with an accessible member type
..* This was done using a grid in xmal creating stack as part of the grid with a width and height of 60 (defined in the xaml). These buttons are then accessible by other methods to check if the place is free to move
2. Try to derive a method of creating new objects as children of the grid
..* This was done as buttons to allow the checking to see if they are activated, using
3. Manage the movement of objects in the grid
..* During this step I revised the first two steps and changed the creation of buttons or rectangles(both tried using) and created a stack panel instead. This allowed me to create real children of each of the objects, each stack panel had an attribute set on creation using stackPanel.Background == Brushes.Colour. 
..* This meant that each button could then be attributed on top of the stack panel, making it easier to move objects or in this case, get the state of each button then destroy or add a new image of the counter. 
..* One of the othe key features of this step was to create the grid referencing properly, to do this I created an UIElement class to reference the grids 
## Second Segment
This second segment was the main implemention of the functions once and abilities of the project.
1. Creating the two different sides
..* This is the main spawning of the objects depending on if they are 'red' or 'black'. This was created using an if statement, getting the number of the row and column and if the column is a multiple of two then it will spawn an object, if the row is between 1-3 then it's the red colour and if the row is 6-9 then it will create the players tiles.
2. Setting the available moves
..* Getting the available moves was also much a part of the ai, this enabled the player to be able to select where to move. It checks the type of object (king or normal piece).
..* Once the pieces have been checked, it checks the rows above and below of the objects. If there is a piece there it moves onto the next row where ti checks the jump position. Allowing the button on the available square to be pressed and move the current piece there(technically destroying and re-making it at the new position).
3. Handling the AI
..* The AI handling is really simple in terms of what it actually does, it selects a piece at random then if that piece can move it moves it to a random position. I have implemented a feature where the piece has to jump if it can, this enables the AI to always take the opposite piece. 
## Third Segment
1. Fixing up some bugs and adding in a main menu for it
..* Created a new form and making that open up the game
2. Writing the documentation
