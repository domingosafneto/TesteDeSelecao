//using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json;
using Questao2;

public class Program
{
    static int gols;    

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);        

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

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
        int numeroTime = 2;

        while (numeroTime >= 1)
        {


            int paginas;

            string urlBase = "https://jsonmock.hackerrank.com/api/football_matches";
            string queryParams;

            if (numeroTime == 1)
                 queryParams = "?year=" + year + "&team1=" + team;
            else
            {
                queryParams = "?year=" + year + "&team2=" + team;
            }

            string url = urlBase + queryParams;

            using (HttpClient client = new HttpClient())
            {
                JsonRoot jsonRoot = new JsonRoot();

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    jsonRoot = JsonConvert.DeserializeObject<JsonRoot>(responseBody);

                    paginas = int.Parse(jsonRoot.Total_pages);

                    while (paginas >= 1)
                    {
                        using (HttpClient client2 = new HttpClient())
                        {
                            JsonRoot jsonRoot2 = new JsonRoot();
                            string queryParams2 = "?year=" + year + "&team1=" + team + "&page=" + paginas;
                            string url2 = urlBase + queryParams2;

                            HttpResponseMessage response2 = client.GetAsync(url2).Result;

                            if (response2.IsSuccessStatusCode)
                            {
                                string responseBody2 = response2.Content.ReadAsStringAsync().Result;
                                jsonRoot2 = JsonConvert.DeserializeObject<JsonRoot>(responseBody2);

                                foreach (var item in jsonRoot2.data)
                                {
                                    //if (item.Year == year.ToString())
                                    //{
                                    //    if (item.Team1 == team)
                                    //    {
                                    if (numeroTime == 1)
                                        gols = gols + int.Parse(item.Team1Goals);
                                    else
                                        gols = gols + int.Parse(item.Team2Goals);
                                    //     }

                                    //    if (item.Team2 == team)
                                    //    {
                                    //       gols = gols + int.Parse(item.Team2Goals);
                                    //  }
                                    //  }
                                }

                            }
                        }

                        paginas = paginas - 1;
                    }

                }

            }

            numeroTime = numeroTime - 1;
        }                // fim while principal

            return gols;
    }    

}