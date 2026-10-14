using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Principal;

internal class Program{
private static void Main(string[] args){
    int[,] board = setup();

    play(board);
}
static void print(object text, bool line = true, ConsoleColor color = ConsoleColor.White){ // like python ish
    string msg = $"{text}";
    if (color != ConsoleColor.White){
        foreach (char letter in msg){
            if (char.IsDigit(letter)){
                Console.ForegroundColor = color;
                Console.Write(letter);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else    
                Console.Write(letter);
        }
        if (line)
            Console.WriteLine("");
    }
    else if (line)
        Console.WriteLine(msg);
    else
        Console.Write(msg);
}
static int[,] setup(){
    int[,] board = {
        {6, 6, 6, 6, 6, 6, 0},
        {6, 6, 6, 6, 6, 6, 0}
    };
    return board;
}
static void show_board(int[,] board){
    ConsoleColor red = ConsoleColor.Red;
    ConsoleColor blue = ConsoleColor.Blue;
    print("    6   5   4   3   2   1", color: red);
    print("  |=======================|");
    print("  | ", false);
    for (int i = 1; i < board.GetLength(1); i++)
        print($"{board[0, i]} | ", false, red);
    print("");
    print(board[0, 0], false, red);
    print(" |———+———+———+———+———+———| ", false);
    print(board[1, 6], color: blue);
    print("  | ", false);
    for (int i = 1; i < board.GetLength(1); i++)
        print($"{board[0, i]} | ", false, blue);
    print("");
    print("  |=======================|");
    print("    1   2   3   4   5   6", color: blue);
}
static bool game_over(int[,] board){
    bool player0 = true, player1 = true;
    return player0 && player1;
}
static void make_move(int[,] board){
}
static void play(int[,] board){
    show_board(board);
    print("player 1 (1-6)");
    // while (player0 && player1){
    //     for (i)
    // }
    while (!game_over(board)){
        make_move(board);
    }
}
}

