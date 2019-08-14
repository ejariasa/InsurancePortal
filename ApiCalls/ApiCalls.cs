using Model.EntityModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Business.ApiCalls
{
    public class ApiCalls
    {

        public string BaseUrl = "https://localhost:44341/";



        bool CheckStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpStatusCodeException((int)response.StatusCode, response.ReasonPhrase);
            }

            return true;
        }

        /// <summary>
        /// Makes the get API call.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="site">The site.</param>
        /// <returns></returns>
        /// <exception cref="MyAccountException"></exception>
        public T MakeGetAPICall<T>(string apiUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (CheckStatusCode(response))
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return JsonConvert.DeserializeObject<T>(result);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Makes the post API call.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="content">Content of the string.</param>
        /// <returns></returns>
        /// <exception cref="MyAccountException"></exception>
        public T MakePostAPICall<T>(string apiUrl, string content)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(apiUrl, new StringContent(content, Encoding.UTF8, "text/json")).Result;

                    if (CheckStatusCode(response))
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return JsonConvert.DeserializeObject<T>(result);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Makes the Post API Call when no json input parameter is sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public T MakePostAPICallNoJson<T>(string apiUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage response = client.PostAsync(apiUrl, new StringContent(string.Empty)).Result;

                    if (CheckStatusCode(response))
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        if (response.Content.Headers.ContentType.MediaType == "text/html")
                        {
                            return (T)(object)result;
                        }
                        else
                        {
                            return JsonConvert.DeserializeObject<T>(result);
                        }
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Makes the put API call
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public T MakePutAPICall<T>(string apiUrl, string content)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PutAsync(apiUrl, new StringContent(content, Encoding.UTF8, "text/json")).Result;

                    if (CheckStatusCode(response))
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return JsonConvert.DeserializeObject<T>(result);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Makes the Put API Call when no json input parameter is sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public T MakePutAPICallNoJson<T>(string apiUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage response = client.PutAsync(apiUrl, new StringContent(string.Empty)).Result;

                    if (CheckStatusCode(response))
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return JsonConvert.DeserializeObject<T>(result);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
