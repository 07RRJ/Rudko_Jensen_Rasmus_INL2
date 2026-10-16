using System;
internal class Program{
private static void Main(string[] args){
    Kalah game = new Kalah(); // start 
    // OBS: for the rules i mainly follow "https://kalaha.krus.dk"
}
class Kalah{
    Random rng = new Random();
    int[] board = new int[14]; // game data (board values)
    int pitAmount = 6; // amount of pits
    int playerStore = 6; // idx on board for player store
    int aiStore = 13; // idx on board for ai store
    bool playerMove;
    ConsoleColor red = ConsoleColor.Red;
    ConsoleColor blue = ConsoleColor.Blue;
    ConsoleColor green = ConsoleColor.Green;
    public Kalah(){
        board = new int[] {6, 6, 6, 6, 6, 6, 0, 6, 6, 6, 6, 6, 6, 0};
        Init(); // start init or rather the "game loop" and logic
    }
    void ShowBoard(){
        // v-   6    5    4    3    2    1  <-
        //   |=============================|
        //   |  A |  A |  A |  A |  A |  A |
        // A |————+————+————+————+————+————| B
        //   |  B |  B |  B |  B |  B |  B |
        //   |=============================|
        // ->   1    2    3    4    5    6  -^

        Print("");
        Print("  v-   6    5    4    3    2    1  <-");
        Print("    |=============================|");
        Print("    | ", false);

        for (int i = 12; i >= 7; i--) // write pits mirrored from data
            Print($"{board[i], 2} | ", false, red);

        Print("");
        Print($" {board[aiStore], 2}", false, red);
        Print(" |————+————+————+————+————+————| ", false);
        Print($"{board[playerStore], 2}", color: blue);

        Print("    | ", false);
        for (int i = 0; i < pitAmount; i++) // write out pits
            Print($"{board[i], 2} | ", false, blue);

        Print("");
        Print("    |=============================|");
        Print("  ->   1    2    3    4    5    6  -^");
        Print("");
    }
    bool OneSideIsEmpty(){
        bool noAvailabePlayerMoves = true, noAvailabeAiMoves = true;
        for (int i = 0; i < pitAmount; i++){ // checks if one player doesnt have any moves
            if (board[i] > 0)
                noAvailabePlayerMoves = false;
            if (board[i + pitAmount + 1] > 0)
                noAvailabeAiMoves = false;
        }
        return noAvailabePlayerMoves || noAvailabeAiMoves;
    }
    bool GameOver(){
        return board[aiStore] > 36 || board[playerStore] > 36 || OneSideIsEmpty();
        //     return true; // has more than half and at least one side is empty

        // return false;
    }
    bool IsValidMove(int move){
        if (playerMove && (move < 0 || move > 5)) // valid player move?
            return false;

        if (!playerMove && (move < 7 || move > 12)) // valid ai move?
            return false;

        return board[move] > 0; // is the pit empty?
    }
    void PlayOutMove(int move){
        int currentPit = move; // better name for the situation and nice to keep separate
        int ownStore = playerMove ? playerStore : aiStore; // your store idx
        int opponentStore = playerMove ? aiStore : playerStore; // opponent store idx

        int stones = board[currentPit]; // store stones
        board[currentPit] = 0; // remove taken stones

        while (stones > 0){
            currentPit = (currentPit + 1) % 14; // increase idx and loop around

            if (currentPit == opponentStore) // skip opponents store
                continue;

            board[currentPit]++; // place stone
            stones--; // remove stone from picked up ones
        }

        if (currentPit == ownStore){
            int startPit = playerMove ? 0 : 7;
            for (int i = startPit; i < pitAmount + startPit; i++)
                if (board[i] > 0)
                    return; // if you end in your store play again
        }

        bool landedOnOwnSide = playerMove ? // if landed on your own side
            (currentPit >= 0 && currentPit <= 5) : // player
            (currentPit >= 7 && currentPit <= 12); // ai

        if (landedOnOwnSide && board[currentPit] == 1){ // if it was empty
            int oppositePit = 12 - currentPit; // opposite side idx

            board[ownStore] += board[oppositePit] + 1; // take them
            board[oppositePit] = 0; // empty
            board[currentPit] = 0; // empty
        }
        playerMove = !playerMove; // change turn
    }
    void MakePlayerMove(){
        int move;
        string? input;

        while (true) {
            Print("choose (1-6): ", false, color: green);
            input = Console.ReadLine();

            if (!int.TryParse(input, out move)){ // check if number
                Print("must enter an integer");
                continue;
            }

            move--; // from 1-6 to 0-5 to get board idx

            if (!IsValidMove(move)){ // if it is a valid move
                Print("invalid number");
                continue;
            }
            break;
        }

        PlayOutMove(move);
    }
    void MakeAiMove(){
        int move = -1; // simpler to do this instead a do while when testing since i can set first input

        while (!IsValidMove(move)){ // re generate a number until it is valid 
            move = rng.Next(7, 13); // 7-12 since idx 0-5 is player pits, 6 is player store 7-12 ai pit, 13 ai store
        }

        PlayOutMove(move);
        Print($"ai chose pit: {move - 6}");
    }
    void CollectRemainingStones(){ // empty pits
        for (int i = 0; i < pitAmount; i++){
            board[aiStore] += board[i + 7];
            board[i + 7] = 0;

            board[playerStore] += board[i];
            board[i] = 0;
        }
    }
    void Init(){
        Console.Clear();

        playerMove = rng.Next(0, 2) == 0; // "randomly" decide who starts
        string starts = playerMove ? "player" : "ai";
        Print($"{starts} starts");

        while (!GameOver()){ // while no one has reached 36+ and everyone has stones
            ShowBoard();
            if (playerMove)
                MakePlayerMove();
            else
                MakeAiMove();
        }
        CollectRemainingStones(); // set all pits to 0
        ShowBoard(); // show final board

        int playerScore = board[playerStore];
        int aiScore = board[aiStore];

        if (playerScore > aiScore){ // player won
            Print($"player won: {playerScore}", false, color: blue);
            Print($" - {aiScore}", color: red);
        }
        else if (aiScore > playerScore){ // ai won
            Print($"ai won: {aiScore}", false, color: red);
            Print($" - {playerScore}", color: blue);
        }
        else{ // draw
            Print($"draw, player: {playerScore}", false, color: blue);
            Print($", ai: {aiScore}", color: red);
        }
    }
}
static void Print(object text, bool line = true, ConsoleColor color = ConsoleColor.White){
    string msg = $"{text}";

    if (color != ConsoleColor.White){ // if you want to color numbers
        foreach (char letter in msg){
            if (char.IsDigit(letter)){ // if char is digit write it out in selected color
                Console.ForegroundColor = color;
                Console.Write(letter);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else{
                Console.Write(letter);
            }
        }
        if (line)
            Console.WriteLine();
    }
    else if (line){ // full line (default for this func)
        Console.WriteLine(msg);
    }
    else{
        Console.Write(msg);
    }
}
}