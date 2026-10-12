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
        {0, 6, 6, 6, 6, 6, 6},
        {6, 6, 6, 6, 6, 6, 0}
    };
    return board;
}
static void show_board(int[,] board){
    print("  | 6   5   4   3   2   1 |", color: ConsoleColor.Red);
    print("  |=======================|");
    print("  | ", false);
    Console.ForegroundColor = ConsoleColor.Red;
    for (int i = 1; i < board.GetLength(1); i++){
        print($"{board[0, i]} | ", false);
    }
    print("");
    print(board[0, 0], false, ConsoleColor.Red);
    print(" |———+———+———+———+———+———| ", false);
    print(board[1, 6], false, ConsoleColor.Blue);
    print("  | ", false);
    for (int i = 1; i < board.GetLength(1); i++){
        print($"{board[0, i]} | ", false);
    }
    Console.ForegroundColor = ConsoleColor.White;
    print("\n  |=======================|");
    print("  | 1   2   3   4   5   6 |", color: ConsoleColor.Blue);
}
static void play(int[,] board){
    show_board(board);
    print("player 1 (1-6)");
    bool player0 = true, player1 = true;
    // while (player0 && player1){
    //     for (i)
    // }
}
}