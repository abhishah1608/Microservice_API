using api.Domain.Entities.Error;
using api.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace api.Infrastructure.Repository
{
    /// <summary>
    /// Author : Abhi shah: Repository used to call third party API either it can be third party call or the 
    /// different microServices wihin the system. 
    /// </summary>
    public class ApiRepository : IApiRepository
    {
        private readonly HttpClient _httpClient;

        public ApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> GetDataList<T>(string url, string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonHelper.JsonToList<T>(content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">T object.</typeparam>
        /// <param name="url">string url.</param>
        /// <param name="token">string token.</param>
        /// <returns></returns>
        public async Task<T> GetData<T>(string url, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonHelper.JsonToObject<T>(content);
        }

        /// <summary>
        /// Post method that returns single object, based on single object as content type.
        /// </summary>
        /// <typeparam name="T">return Type.</typeparam>
        /// <typeparam name="T1">Passed Type for Content type.</typeparam>
        /// <param name="url">url of the post API</param>
        /// <param name="data">data to be passed in body of API.</param>
        /// <param name="token">jwt token need to be passed in header of Authorization.</param>
        /// <returns></returns>
        public async Task<T> PostData<T, T1>(string url, T1 data, string token)
        {
            T result = default(T);
            try
            {

                string jsonData = JsonHelper.ObjectToJson<T1>(data);

                // Set up the request content as JSON.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the JWT token in the Authorization header.
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Optionally, you can also add other headers if required
                // _httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                // Ensure the response status code indicates success
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into the generic type T
                result = JsonHelper.JsonToObject<T>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request-related exceptions (like 4xx or 5xx status codes)
                Console.WriteLine($"Request error: {ex.Message}");
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                
                throw ex;
            }
            return result;
        }

        public async Task<List<T>> PostDataList<T, T1>(string url, List<T1> data, string token)
        {
            List<T> result = default(List<T>);
            try
            {

                string jsonData = JsonHelper.ListToJson<T1>(data);

                // Set up the request content as JSON.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the JWT token in the Authorization header.
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Optionally, you can also add other headers if required
                // _httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                // Ensure the response status code indicates success
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into the generic type T
                result = JsonHelper.JsonToList<T>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request-related exceptions (like 4xx or 5xx status codes)
                
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> PostDataAndGetObj<T, T1>(string url, List<T1> data, string token)
        {
            T result = default(T);
            try
            {

                string jsonData = JsonHelper.ListToJson<T1>(data);

                // Set up the request content as JSON.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the JWT token in the Authorization header.
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Optionally, you can also add other headers if required
                // _httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                // Ensure the response status code indicates success
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into the generic type T
                result = JsonHelper.JsonToObject<T>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request-related exceptions (like 4xx or 5xx status codes)
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> PutData<T, T1>(string url, T1 data, string token)
        {
            T result = default(T);
            try
            {

                string jsonData = JsonHelper.ObjectToJson<T1>(data);

                // Set up the request content as JSON.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the JWT token in the Authorization header.
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Optionally, you can also add other headers if required
                // _httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                // Ensure the response status code indicates success
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into the generic type T
                result = JsonHelper.JsonToObject<T>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request-related exceptions (like 4xx or 5xx status codes)
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<T>> PutDataList<T, T1>(string url, List<T1> data, string token)
        {
            List<T> result = default(List<T>);
            try
            {

                string jsonData = JsonHelper.ListToJson<T1>(data);

                // Set up the request content as JSON.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the JWT token in the Authorization header.
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Optionally, you can also add other headers if required
                // _httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                // Ensure the response status code indicates success
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into the generic type T
                result = JsonHelper.JsonToList<T>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request-related exceptions (like 4xx or 5xx status codes)
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T> PutDataAndGetObj<T, T1>(string url, List<T1> data, string token)   
        {
            T result = default(T);
            try
            {

                string jsonData = JsonHelper.ListToJson<T1>(data);

                // Set up the request content as JSON.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the JWT token in the Authorization header.
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Optionally, you can also add other headers if required
                // _httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                // Ensure the response status code indicates success
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into the generic type T
                result = JsonHelper.JsonToObject<T>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request-related exceptions (like 4xx or 5xx status codes)
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<T> DeleteData<T>(string url, string token)
        {
            throw new NotImplementedException();
        }
    }
}
