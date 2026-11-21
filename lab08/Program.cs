/*
* Name: Z Bradford
* Course: CSCI 1250, Section 201
* Assignment: Lab 08, Game Night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/


// Constants
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

//Look for the data files next to Program,cs, however the program is started.
string projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

//Turn paths into arrays
string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);
string[] history = File.ReadAllLines(HISTORY_FILE);

//Read the history
Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");
if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
}
else
{
    Console.WriteLine("Nights played: " + history.Length);
    foreach(string night in history)
    {
        Console.WriteLine(" " + night);
    }
}
int thisNight = history.Length + 1;
Console.WriteLine("Tonight is night " + thisNight + ".");

// Part 1: The Standings
//Calculations
int leader = LeaderIndex(points);
int total = TotalPoints(points);

//Output
Console.WriteLine(" ");
Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

for (int i = 0; i < players.Length; i++)
{
    Console.WriteLine(players[i].PadRight(11) + points[i]);
}
Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
Console.WriteLine("Season total: " + total);

//Part 2: Game Night
//Calculations
int winner = 0;
int score = 0;
Console.WriteLine(" ");
for (int i = 0; i < players.Length; i++)
{
    int name = Convert.ToInt32(i);
    Console.Write("Points tonight for " + players[name] + ": ");
    int scores = Convert.ToInt32(Console.ReadLine());
    while(scores > MAX_POINTS || scores < 0)
    {
        Console.WriteLine("Points must be frome 0 to 20.");
        Console.Write("Points tonight for " + players[name] + ": ");
        scores = Convert.ToInt32(Console.ReadLine());
    }
    if(scores > score)
    {
        score = scores;
        winner = i;
    }
    int season = Convert.ToInt32(points[i]) + score;
    points[i] = season.ToString();
}
File.WriteAllLines(POINTS_FILE, points);

//Output
Console.WriteLine("Tonight's winner: " + players[winner] + " with " + score);
Console.WriteLine(" ");

//Part 3: The History 
string[] entry = { HistoryEntry(thisNight, players[winner], score)};
File.AppendAllLines(HISTORY_FILE, entry);
//METHODS

//FindLeader
static int LeaderIndex(string[] points)
{
    int leader = 0;
    for(int i = 1; i < points.Length; i++)
    {
        if (Convert.ToInt32(points[i]) > Convert.ToInt32(points[leader]))
        {
            leader = i;
        }
    }
    return leader;
}
//Calculate total
static int TotalPoints(string[] points)
{
    int total = 0;
    foreach (string i in points)
    {
        int add = Convert.ToInt32(i);
        total = total + Convert.ToInt32(add);
    }
    return total;
}
static string HistoryEntry(int night, string winner, int points)
{
    return "Night " + night + ": " + winner + " won with " + points + " points";
}