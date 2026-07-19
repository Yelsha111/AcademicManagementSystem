using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AcademicManagement.Services
{
    // Talks directly to team's Firebase Realtime Database over its REST API.
    // Every call is automatically scoped under "academicManagement/" so this class
    // can never accidentally read or write attendance, departments, employees,
    // medical, students, or teachers (the other teams' nodes at the root).
    public class FirebaseService
    {
        private readonly string _baseUrl;  
        private readonly string _secret;   
        private readonly HttpClient _client;

        public FirebaseService(string databaseUrl, string secret)
        {
            _baseUrl = databaseUrl.TrimEnd('/') + "/academicManagement";
            _secret = secret;
            _client = new HttpClient();
        }

        // node examples: "colleges", "programs", "courses", "curriculum", "academicofferings"
        // key is the record's own ID (e.g. "CLG001"). Leave key null to target the whole node.
        private string BuildUrl(string node, string key = null)
        {
            var path = key == null
                ? $"{_baseUrl}/{node}.json"
                : $"{_baseUrl}/{node}/{key}.json";

            return string.IsNullOrWhiteSpace(_secret) ? path : $"{path}?auth={_secret}";
        }

        // Reads an entire node (e.g. all Colleges) as a Dictionary<Id, T>.
        // Returns an empty dictionary (never null) if the node is empty or doesn't exist yet.
        public async Task<Dictionary<string, T>> GetAllAsync<T>(string node)
        {
            var url = BuildUrl(node);
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new Dictionary<string, T>();

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json) || json == "null")
                return new Dictionary<string, T>();

            var result = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            return result ?? new Dictionary<string, T>();
        }

        // Writes (creates or overwrites) a single record at node/key.
        public async Task<bool> SaveAsync(string node, string key, Dictionary<string, object> data)
        {
            var url = BuildUrl(node, key);
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        // Deletes a single record at node/key.
        public async Task<bool> DeleteAsync(string node, string key)
        {
            var url = BuildUrl(node, key);
            var response = await _client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
