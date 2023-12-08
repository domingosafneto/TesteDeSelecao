//using Newtonsoft.Json;
using System.Text.Json;
using Questao2;

public class Program
{    

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
        int vGols = 0;
       // FazerRequisicao(team, year);
        
        return 0;
    }


    public static async Task FazerRequisicao(string time, int ano)
    {
        string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
        string queryParams = "?year=" + ano.ToString() + "&team1=" + time;

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + queryParams);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    List<FootballMatch> json = JsonSerializer.Deserialize<List<FootballMatch>>(responseBody);

                    if (json != null)
                    {
                        foreach (FootballMatch match in json)
                        {
                            if (match.Year == ano)
                            {
                                if (match.Team1 == time)
                                {

                                }
                            }
                        }
                    }                    

                }
                else
                {
                    Console.WriteLine("A requisição não foi bem sucedida. Código: " + response.StatusCode);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Erro ao fazer a requisição: " + e.Message);
            }
        }
    }


}