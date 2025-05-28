using MatchUpProyecto.Models;
using Azure.Core;
using Newtonsoft.Json;
using NugetMatchUp.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MatchUpProyecto.Services
{
    public class ServiceMatchUp
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceMatchUp(IConfiguration configuration)
        {
            this.ApiUrl = "https://apimatchup.azurewebsites.net/api/";
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "Auth/Login";
                client.BaseAddress = (new Uri(this.ApiUrl));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                LoginModel model = new LoginModel
                {
                    Email = email,
                    Password = password
                };
                string json = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject keys = JObject.Parse(data);
                    string token = keys.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}, {errorDetails}";
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        #region Deportes
        public async Task<List<Deporte>> GetDeportesAsync()
        {
            string request = "Deportes";
            List<Deporte> data = await this.CallApiAsync<List<Deporte>>(request);
            return data;
        }

        #endregion

        #region Equipos
        public async Task<List<Equipo>> GetEquiposAsync()
        {
            string request = "Equipos";
            List<Equipo> data = await this.CallApiAsync<List<Equipo>>(request);
            return data;
        }

        public async Task<List<Equipo>> GetEquiposUserAsync(int idusuario, string token)
        {
            string request = "Equipos/" + idusuario;
            List<Equipo> data = await this.CallApiAsync<List<Equipo>>(request, token);
            return data;
        }

        public async Task<EquipoDetalle> GetEquipoDetailsAsync(int idequipo)
        {
            string request = "Equipos/Details/" + idequipo;
            EquipoDetalle data = await this.CallApiAsync<EquipoDetalle>(request);
            return data;
        }

        public async Task PostEquipoAsync(string token, Equipo equipo)
        {
            string request = "Equipos/Create";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                string json = JsonConvert.SerializeObject(equipo);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }


        public async Task UnirseEquipoAsync(int idequipo, int idusuario, string token)
        {
            string request = "Equipos/Join/" + idequipo + "/" + idusuario;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpContent emptyContent = new StringContent("");
                HttpResponseMessage response = await client.PostAsync(request, emptyContent);
            }
        }

        public async Task SalirseEquipoAsync(int idEquipo, int idUsuario, string token)
        {
            string request = "Equipos/Leave/" + idEquipo + "/" + idUsuario;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpContent emptyContent = new StringContent("");
                HttpResponseMessage response = await client.PostAsync(request, emptyContent);
            }
        }

        public async Task UnirseAPartidoAsync(int idEquipo, int idPartido, string token)
        {
            string request = "Equipos/JoinToMatch/" + idEquipo + "/" + idPartido;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpContent emptyContent = new StringContent("");
                HttpResponseMessage response = await client.PostAsync(request, emptyContent);
            }
        }

        #endregion

        #region Pachangas
        public async Task<Pachanga> GetPachangasAsync()
        {
            string request = "Pachangas";
            Pachanga data = await this.CallApiAsync<Pachanga>(request);
            return data;
        }
        public async Task<List<PartidoEquipos>> GetPartidosPachangaAsync()
        {
            string request = "Pachangas/PartidosPachanga";
            List<PartidoEquipos> data = await this.CallApiAsync<List<PartidoEquipos>>(request);
            return data;
        }
        public async Task<List<PartidoEquipos>> GetPartidosMesAsync(int idusuario, string token)
        {
            string request = "Pachangas/PartidosMes/" + idusuario;
            List<PartidoEquipos> data = await this.CallApiAsync<List<PartidoEquipos>>(request, token);
            return data;
        }

        public async Task CreatePachangaAsync(Pachanga pachanga, int idequipo, string token)
        {
            string request = "Pachangas/Create/" + idequipo;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                string json = JsonConvert.SerializeObject(pachanga);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }


        public async Task UpdateResultPachanga(int local, int visitante, int idpartido, string token)
        {
            string request = "Pachangas/UpdateResult/" + local + "/" + visitante + "/" + idpartido;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpContent emptyContent = new StringContent("");
                HttpResponseMessage response = await client.PutAsync(request, emptyContent);
            }
        }
        #endregion

        #region Users

        public async Task<User> LoginAsync(string email, string password)
        {
            string request = $"User/Login/{email}/{password}";
            User data = await this.CallApiAsync<User>(request);
            return data;
        }

        public async Task RegisterUserAsync(string email, string password, string name)
        {
            string request = "User/Register/" + email + "/" + name + "/" + password;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpContent emptyContent = new StringContent("");
                HttpResponseMessage response = await client.PostAsync(request, emptyContent);
            }
        }

        public async Task UpdateImageUser(string imagen, int idusuario, string token)
        {
            string request = "User/UpdateImage/" + imagen + "/" + idusuario;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpContent emptyContent = new StringContent("");
                HttpResponseMessage response = await client.PutAsync(request, emptyContent);
            }
        }

        #endregion
    }
}