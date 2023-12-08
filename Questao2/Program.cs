//using Newtonsoft.Json;
using System.Text.Json;
using Questao2;

public class Program
{
    static int gols;
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        gols = 0;

        string urlBase = "https://jsonmock.hackerrank.com/api/football_matches";
        string queryParams = "?year=" + year + "&team1=" + team;
        
        string url = urlBase + queryParams;

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                JsonRoot jsonRoot = new JsonRoot();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                jsonRoot = JsonSerializer.Deserialize<JsonRoot>(responseBody);


                
                // ajustar
                    foreach(JsonRoot match in matches)
                    { 
                        if (match.Year == year.ToString())
                        {
                            if (match.Team1 == team)
                            {
                                gols = gols + 1;
                            }
                        }
                    }

            }

        }


       return gols;
    }

}