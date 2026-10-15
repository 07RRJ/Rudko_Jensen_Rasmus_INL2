using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Principal;

internal class Program{
private static void Main(string[] args){
    Console.Clear();
    Kalah game = new Kalah();
}
class Kalah{
    Random rng = new Random();
    int[] board = new int[14];
    int pit_amount = 6, p0_store = 0, p1_store = 13;
    bool player_move;
    ConsoleColor red = ConsoleColor.Red;
    ConsoleColor blue = ConsoleColor.Blue;

    public Kalah(){
        int[] board = {0, 6, 6, 6, 6, 6, 6, 0, 6, 6, 6, 6, 6, 6};
        this.board = board;
        this.init();
    }
    void show_board(){
        //     6   5   4   3   2   1
        //   |=======================|
        //   | A | A | A | A | A | A |
        // A |———+———+———+———+———+———| B
        //   | B | B | B | B | B | B |
        //   |=======================|
        //     6   5   4   3   2   1

        print("    6   5   4   3   2   1", color: red);
        print("  |=======================|");
        print("  | ", false);
        for (int i = 1; i < 1 + pit_amount; i++)
            print($"{board[i]} | ", false, red);
        print("");
        print(p0_store, false, red);

        print(" |———+———+———+———+———+———| ", false);

        print(board[p1_store], color: blue);
        print("  | ", false);
        for (int i = pit_amount + 2; i < board.Length; i++)
            print($"{board[i]} | ", false, blue);
        print("");
        print("  |=======================|");
        print("    1   2   3   4   5   6", color: blue);
    }
    bool game_over(){
        bool player0 = false, player1 = false;
        for (int i = 0; i < pit_amount; i++){
            if (board[i + 1] != 0)
                player0 = true;
            if (board[i + pit_amount + 1] != 0)
                player0 = true;
        }
        return player0 && player1;
    }
    bool possible_move(int move){
        if (move > 6 || move < 0)
            return false;
        move -= 1;
        print($"{move}, {player_move}");
        if (player_move){
            return board[move + pit_amount + 1] != 0;
        }
        else {
            return board[move + 1] != 0;
        }
    }
    void play_out_move(int move){
        if (player_move){
            move += pit_amount + 1;
        }
        int stones = board[move];
        board[move] = 0;
    }
    void make_move(){
        int move = -1;
        if (player_move){
            do {
                show_board();
                print("player (1-6): ", false);
                string input = Console.ReadLine();
                int.TryParse(input, out move);
            }
            while (!possible_move(move));
        }
        else{
            do {
                move = rng.Next(1, 6);
            }
            while (!possible_move(move));
        }

        play_out_move(move);

        player_move = !player_move;
    }
    void init(){
        player_move = rng.Next(0, 1) == 0;
        while (!game_over()){
            // Console.Clear();
            // Console.SetCursorPosition(0, 0);
            make_move();
        }
    }
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
}