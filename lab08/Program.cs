

// Constants
using System.Drawing;

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

// Part 1: The Standings
//Calculations
int leader = LeaderIndex(points);
int total = TotalPoints(points);

//Output
Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

for (int i = 0; i < players.Length; i++)
{
    Console.WriteLine(players[i].PadRight(11) + points[i]);
}
Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
Console.WriteLine("Season total: " + total);

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