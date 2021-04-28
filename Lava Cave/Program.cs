using System;
using System.Collections;

namespace Lava_Cave
{
    class Program
    {
        static void Main(string[] args)
        {
            //----------------- Intro Text with Instructions -----------------\\
            Console.WriteLine("Welcome to Lava Cave!");
            Console.WriteLine("Loot the treasure without falling into lava.");
            Console.WriteLine("Try the following commands:");
            Console.WriteLine("Move (F)orward, Turn (L)eft, Turn (R)ight,");
            Console.WriteLine("(S)earch for items, Loot the (T)reasure,");
            Console.WriteLine("(Q)uit the game, Use(X) to cheat.");
            Console.WriteLine("");


            //====================================> Creates a 4 x 4 board <====================================\\
            string[,] board = new string[5, 5]; //technically 5 but player will only use 4x4
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    board[x, y] = ".";
                }
            }

            // random Generator
            Random rand = new Random();

            // Start with no Key
            Boolean hasKey = false;

            // 2 ints ready to be used for random numbers
            int r1;
            int r2;

            // Player position
            Point playerPos = new Point(0, 0);

            // The Sensing Sentences
            String[] sense = { "You sense a blast of heat.",                    // 0
                             "You sense a rusty smell.",                        // 1
                             "You sense a rusty smell and a blast of heat.",    // 2
                             "You sense a shiny glow.",                         // 3
                             "You sense a shiny glow and a blast of heat.",     // 4
                             "You sense nothing unusual." };                    // 5

            int senseNumber = 5;

            // Second board (invisible to user) to handle Senses
            string[,] board2 = new string[8, 8];
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board2[x, y] = ".";
                }
            }


            //====================================> Creates 3 random lava pits <====================================\\
            for (int i = 0; i < 3; i++)
            {
                r1 = rand.Next(1, 5);
                r2 = rand.Next(1, 5);
                Boolean tempLoop = true;
                while (tempLoop) {
                    // Applies lava to an empty spot on the board
                    if (board[r1, r2] == ".")
                    {
                        board[r1, r2] = "L";
                        board2[r1, r2]   = "heat";   //<= applying senses on the second board
                        board2[r1+1, r2] = "heat";
                        board2[r1-1, r2] = "heat";
                        board2[r1, r2+1] = "heat";
                        board2[r1, r2-1] = "heat";
                        tempLoop = false;
                    }
                    // If there is already something there, changes numbers to try again
                    else
                    {
                        r1 = rand.Next(1, 5);
                        r2 = rand.Next(1, 5);
                    }
                }
            }


            //====================================> Creates 1 random Treasure Chest <====================================\\
            for (int i = 0; i < 1; i++)
            {
                r1 = rand.Next(1, 5);
                r2 = rand.Next(1, 5);
                Boolean tempLoop = true;
                while (tempLoop)
                {
                    // Applies a treasure chest to an empty spot on the board
                    if (board[r1, r2] == ".")
                    {
                        board[r1, r2] = "T";
                        if (board2[r1, r2] == "heat")       //<= applying senses on the second board
                        {
                            board2[r1, r2] = "heat,Glow";
                        }
                        else
                        {
                            board2[r1, r2] = "Glow";
                        }
                            tempLoop = false;
                    }
                    // If there is already something there, changes numbers to try again
                    else
                    {
                        r1 = rand.Next(1, 5);
                        r2 = rand.Next(1, 5);
                    }
                }
            }


            //====================================> Creates 1 random Key <====================================\\
            for (int i = 0; i < 1; i++)
            {
                r1 = rand.Next(1, 5);
                r2 = rand.Next(1, 5);
                Boolean tempLoop = true;
                while (tempLoop)
                {
                    // Applies a key to an empty spot on the board
                    if (board[r1, r2] == ".")
                    {
                        board[r1, r2] = "K";
                        if (board2[r1, r2] == "heat")       //<= applying senses on the second board
                        {
                            board2[r1, r2] = "heat,smell";
                        }
                        else
                        {
                            board2[r1, r2] = "smell";
                        }
                        tempLoop = false;
                    }
                    // If there is already something there, changes numbers to try again
                    else
                    {
                        r1 = rand.Next(0, 4);
                        r2 = rand.Next(0, 4);
                    }
                }
            }

            //====================================> Creates 1 random Entrance <====================================\\
            for (int i = 0; i < 1; i++)
            {
                r1 = rand.Next(1, 5);
                r2 = rand.Next(1, 5);
                Boolean tempLoop = true;
                while (tempLoop)
                {
                    // Applies an entrance to an empty spot on the board
                    if (board[r1, r2] == ".")
                    {
                        board[r1, r2] = "E";
                        if (board2[r1 + 1, r2] == "heat")   //<= applying senses on the second board
                        {
                            senseNumber = 0;
                        }
                        else
                        {
                            senseNumber = 5;
                        }

                        playerPos.x = r1;
                        playerPos.y = r2;
                        tempLoop = false;
                    }
                    // If there is already something there, changes numbers to try again
                    else
                    {
                        r1 = rand.Next(1, 5);
                        r2 = rand.Next(1, 5);
                    }
                }
            }


            //----------------- Player Direction -----------------\\ 
            //(For dirnNum) -> 0        1       2        3
            String[] Dir = {"North", "East", "South", "West"};
            int dirNum = 1;

            // Game Start Info
            Console.WriteLine("You are at: "+ "(" + (playerPos.x - 1) + "," + (playerPos.y - 1) + ")");
            Console.WriteLine("You face " + Dir[dirNum]);
            Console.WriteLine("You sense " + sense[senseNumber]);
            Console.Write("Enter Cmd: ");

            //............................. -= gameLoop :) =- .............................\\
            Boolean gameLoop = true;
            while (gameLoop) {

                string answer = Console.ReadLine();
                //----------------- Pulling up the Map -----------------\\
                if (answer == "x" || answer == "X")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Cheat!");
                    Console.WriteLine("=======");

                    // Prints out the board as a grid
                    for (int y = 1; y < 5; y++)
                    {
                        for (int x = 1; x < 5; x++)
                        {
                            Console.Write(board[x, y] + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("=======");
                }

                //----------------- Turning to the left <- *CounterClockwise* -----------------\\
                else if (answer == "l" || answer == "L")
                {
                    // East to North
                    if (dirNum == 1)
                    {
                        dirNum = 0;
                    }

                    // South to East
                    else if (dirNum == 2)
                    {
                        dirNum = 1;
                    }

                    // West to South
                    else if (dirNum == 3)
                    {
                        dirNum = 2;
                    }

                    // North to West
                    else
                    {
                        dirNum = 3;
                    }
                }

                //----------------- Turning to the right -> *Clockwise* -----------------\\
                else if (answer == "r" || answer == "R")
                {
                    // North to East
                    if (dirNum == 0)
                    {
                        dirNum = 1;
                    }

                    // East to South
                    else if (dirNum == 1)
                    {
                        dirNum = 2;
                    }

                    // South to West
                    else if (dirNum == 2)
                    {
                        dirNum = 3;
                    }

                    // West to North
                    else
                    {
                        dirNum = 0;
                    }
                }

                //----------------- Moving forward based on direction the player is facing -----------------\\
                else if (answer == "f" || answer == "F")
                {
                    // Moving North
                    if (dirNum == 0 && playerPos.y <= 4 && playerPos.y > 1)
                    {
                        playerPos.y--;
                        if (board[playerPos.x, playerPos.y] == "L")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You fall into lava and burn to a crisp!");
                            Console.WriteLine("Game Over.");
                            Environment.Exit(1);
                        }
                    }

                    // Moving East
                    else if (dirNum == 1 && playerPos.x < 4)
                    {
                        playerPos.x++;
                        if (board[playerPos.x, playerPos.y] == "L")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You fall into lava and burn to a crisp!");
                            Console.WriteLine("Game Over.");
                            Environment.Exit(1);
                        }
                    }

                    // Moving South
                    else if (dirNum == 2 && playerPos.y >= 1 && playerPos.y <4)
                    {
                        playerPos.y++;
                        if (board[playerPos.x, playerPos.y] == "L")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You fall into lava and burn to a crisp!");
                            Console.WriteLine("Game Over.");
                            Environment.Exit(1);
                        }
                    }

                    // Moving West
                    else if (dirNum == 3 && playerPos.x > 1)
                    {
                        playerPos.x--;
                        if (board[playerPos.x, playerPos.y] == "L")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You fall into lava and burn to a crisp!");
                            Console.WriteLine("Game Over.");
                            Environment.Exit(1);
                        }
                    }

                    // When player tries to go beyond the 4x4 grid
                    else
                    {
                        Console.WriteLine("You bump into a wall.");
                    }

                }


                //----------------- Attempt to search room for key command -----------------\\
                else if (answer == "s" || answer == "S")
                {
                    if (board[playerPos.x, playerPos.y] == "K" && hasKey == false)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("You find a key!");
                        if (board2[playerPos.x, playerPos.y] == "heat,smell")
                        {
                            board2[playerPos.x, playerPos.y] = "heat";
                        }
                        else
                        {
                            board2[playerPos.x, playerPos.y] = ".";
                        }
                        hasKey = true; 
                    }
                    else
                    {
                        Console.WriteLine("You can not find anything to pick up.");
                    }
                }

                //----------------- Attempt to open treasure chest command -----------------\\
                else if (answer == "t" || answer == "T")
                {
                    if (board[playerPos.x, playerPos.y] == "T" && hasKey == true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("You open the chest and find inner peace!");
                        Console.WriteLine("You Win.");
                        Environment.Exit(1);
                    }
                    else if (board[playerPos.x, playerPos.y] == "T" && hasKey == false)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("The treasure chest is locked.");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("You can not find any treasure here.");
                    }
                }

                //----------------- exit game -----------------\\
                else if (answer == "q" || answer == "Q")
                {
                    Environment.Exit(1);
                }

                //----------------- When user inputs invalid command -----------------\\
                else
                {
                    
                    Console.WriteLine("");
                    Console.WriteLine("Invalid command");
                    Console.WriteLine("Try the following commands:");
                    Console.WriteLine("Move (F)orward, Turn (L)eft, Turn (R)ight,");
                    Console.WriteLine("(S)earch for items, Loot the (T)reasure,");
                    Console.WriteLine("(Q)uit the game, Use(X) to cheat.");
                }


                //====================================> Senses updated <====================================\\
                //-------Facing North-------
                if (dirNum == 0){
                    if ( board2[playerPos.x, playerPos.y] == "heat,smell")
                    {
                        // senseNumber 2 = "You sense a rusty smell and a blast of heat."
                        senseNumber = 2;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "heat,Glow")
                    {
                        // senseNumber 4 = "You sense a shiny glow and a blast of heat."
                        senseNumber = 4;
                    }
                    else if (board2[playerPos.x, playerPos.y - 1] == "heat")
                    {
                        // senseNumber 0 = "You sense a blast of heat."
                        senseNumber = 0;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "Glow")
                    {
                        // senseNumber 3 = "You sense a shiny glow."
                        senseNumber = 3;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "smell")
                    {
                        // senseNumber 1 = "You sense a rusty smell."
                        senseNumber = 1;
                    }
                    else
                    {
                        // senseNumber 5 = "You sense nothing unusual."
                        senseNumber = 5;
                    }
                }

                //-------Facing East-------
                if (dirNum == 1)
                {
                    if (board2[playerPos.x, playerPos.y] == "heat,smell")
                    {
                        // senseNumber 2 = "You sense a rusty smell and a blast of heat."
                        senseNumber = 2;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "heat,Glow")
                    {
                        // senseNumber 4 = "You sense a shiny glow and a blast of heat."
                        senseNumber = 4;
                    }
                    else if (board2[playerPos.x + 1, playerPos.y] == "heat")
                    {
                        // senseNumber 0 = "You sense a blast of heat."
                        senseNumber = 0;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "Glow")
                    {
                        // senseNumber 3 = "You sense a shiny glow."
                        senseNumber = 3;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "smell")
                    {
                        // senseNumber 1 = "You sense a rusty smell."
                        senseNumber = 1;
                    }
                    else
                    {
                        // senseNumber 5 = "You sense nothing unusual."
                        senseNumber = 5;
                    }
                }

                //-------Facing South-------
                if (dirNum == 2)
                {
                    if (board2[playerPos.x, playerPos.y] == "heat,smell")
                    {
                        // senseNumber 2 = "You sense a rusty smell and a blast of heat."
                        senseNumber = 2;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "heat,Glow")
                    {
                        // senseNumber 4 = "You sense a shiny glow and a blast of heat."
                        senseNumber = 4;
                    }
                    else if (board2[playerPos.x, playerPos.y + 1] == "heat")
                    {
                        // senseNumber 0 = "You sense a blast of heat."
                        senseNumber = 0;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "Glow")
                    {
                        // senseNumber 3 = "You sense a shiny glow."
                        senseNumber = 3;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "smell")
                    {
                        // senseNumber 1 = "You sense a rusty smell."
                        senseNumber = 1;
                    }
                    else
                    {
                        // senseNumber 5 = "You sense nothing unusual."
                        senseNumber = 5;
                    }
                }

                //-------Facing West-------
                if (dirNum == 3)
                {
                    if (board2[playerPos.x, playerPos.y] == "heat,smell")
                    {
                        // senseNumber 2 = "You sense a rusty smell and a blast of heat."
                        senseNumber = 2;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "heat,Glow")
                    {
                        // senseNumber 4 = "You sense a shiny glow and a blast of heat."
                        senseNumber = 4;
                    }
                    else if ( board2[playerPos.x - 1, playerPos.y] == "heat")
                    {
                        // senseNumber 0 = "You sense a blast of heat."
                        senseNumber = 0;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "Glow")
                    {
                        // senseNumber 3 = "You sense a shiny glow."
                        senseNumber = 3;
                    }
                    else if (board2[playerPos.x, playerPos.y] == "smell")
                    {
                        // senseNumber 1 = "You sense a rusty smell."
                        senseNumber = 1;
                    }
                    else
                    {
                        // senseNumber 5 = "You sense nothing unusual."
                        senseNumber = 5;
                    }
                }

                //---------------- Status update ----------------\\
                Console.WriteLine("");
                //Console.WriteLine("Player Pos = " + "(" + (playerPos.x - 1) + "," + (playerPos.y - 1) + ")"); for debugging
                Console.WriteLine("You face " + Dir[dirNum]);
                Console.WriteLine("You sense " + sense[senseNumber]);
                Console.Write("Enter Cmd: ");
                




            }

        }
    }
}
