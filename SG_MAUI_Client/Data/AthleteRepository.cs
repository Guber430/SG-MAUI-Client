using SG_MAUI_Client.Utilities;
using Microsoft.Maui.Graphics;
using SG_MAUI_Client.Models;
using System.Net.Http.Headers;
using System.Numerics;

namespace SG_MAUI_Client.Data
{
    internal class AthleteRepository : IAthleteRepository
    {
        private readonly HttpClient client = new HttpClient();
        public AthleteRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task AddAthlete(Athlete athleteToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/athlete", athleteToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteAthlete(Athlete athleteToDelete)
        {
            var response = await client.DeleteAsync($"/api/athlete/{athleteToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Athlete> GetAthlete(int AthleteID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/athlete/{AthleteID}");
            if (response.IsSuccessStatusCode)
            {
                Athlete athlete = await response.Content.ReadAsAsync<Athlete>();
                return athlete;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            };
        }

        public async Task<List<Athlete>> GetAthletes()
        {
            HttpResponseMessage response = await client.GetAsync("api/athlete");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return athletes;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<List<Athlete>> GetAthletesByContingent(int ContingentID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/athlete/byContingent/{ContingentID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> Athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return Athletes;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<List<Athlete>> GetAthletesBySport(int SportID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/athlete/bySport/{SportID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> Athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return Athletes;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateAthlete(Athlete athleteToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/athlete/{athleteToUpdate.ID}", athleteToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
