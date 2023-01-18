using Newtonsoft.Json;
using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Interfaces;
using DigiFamily.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiFamily.Services
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<T> GetAsync<T>(string url)
        {
            if (!await CommonHelper.GetIsConnecttedAsync())
                return default(T);
            string oauthToken = await CommonHelper.GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(oauthToken))
            {
                using (HttpClient client = new HttpClient(new HttpClientHandler()))
                {
                    client.Timeout = TimeSpan.FromMinutes(60);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
                    using (HttpResponseMessage response = await client.GetAsync(Config.BaseURL + url))
                    {
                        //if (response.IsSuccessStatusCode)
                        //return CommonHelper.ConvertToObject<T>(await response.Content.ReadAsStringAsync());
                        Stream stream = await response.Content.ReadAsStreamAsync();
                        if (response.IsSuccessStatusCode)
                            return CommonHelper.DeserializeJsonFromStream<T>(stream);
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorResponse errorResponse = CommonHelper.DeserializeJsonFromStream<ErrorResponse>(stream);
                            if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.ErrorDescription))
                            {
                                Logger.Error(errorResponse.ErrorDescription);
                                await CommonHelper.ShowToastAsync(errorResponse.ErrorDescription, ToastType.Error);
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            await Shell.Current.GoToAsync("//LoginPage");
                        string errorMsg = await CommonHelper.StreamToStringAsync(stream);
                        if (!string.IsNullOrEmpty(errorMsg))
                        {
                            Logger.Error(errorMsg);
                            await CommonHelper.ShowToastAsync(errorMsg, ToastType.Error);
                        }
                    };
                };
            }
            return default(T);
        }
        public async Task<T> PostAsync<T>(string url, HttpContent content)
        {
            if (!await CommonHelper.GetIsConnecttedAsync())
                return default(T);
            string oauthToken = await CommonHelper.GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(oauthToken))
            {
                using (HttpClient client = new HttpClient(new HttpClientHandler()))
                {
                    client.Timeout = TimeSpan.FromMinutes(60);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
                    using (HttpResponseMessage response = await client.PostAsync(Config.BaseURL + url, content))
                    {
                        //if (response.IsSuccessStatusCode)
                        //    return CommonHelper.ConvertToObject<T>(await response.Content.ReadAsStringAsync());
                        Stream stream = await response.Content.ReadAsStreamAsync();
                        if (response.IsSuccessStatusCode)
                            return CommonHelper.DeserializeJsonFromStream<T>(stream);
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorResponse errorResponse = CommonHelper.DeserializeJsonFromStream<ErrorResponse>(stream);
                            if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.ErrorDescription))
                            {
                                Logger.Error(errorResponse.ErrorDescription);
                                await CommonHelper.ShowToastAsync(errorResponse.ErrorDescription, ToastType.Error);
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            await Shell.Current.GoToAsync("//LoginPage");
                        string errorMsg = await CommonHelper.StreamToStringAsync(stream);
                        if (!string.IsNullOrEmpty(errorMsg))
                        {
                            Logger.Error(errorMsg);
                            await CommonHelper.ShowToastAsync(errorMsg, ToastType.Error);
                        }
                    };
                };
            }
            return default(T);
        }

        public async Task<T> AnonymousGetAsync<T>(string url)
        {
            if (!await CommonHelper.GetIsConnecttedAsync())
                return default(T);
            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
                client.Timeout = TimeSpan.FromMinutes(60);
                using (HttpResponseMessage response = await client.GetAsync(Config.BaseURL + url))
                {
                    //if (response.IsSuccessStatusCode)
                    //return CommonHelper.ConvertToObject<T>(await response.Content.ReadAsStringAsync());
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    if (response.IsSuccessStatusCode)
                        return CommonHelper.DeserializeJsonFromStream<T>(stream);
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ErrorResponse errorResponse = CommonHelper.DeserializeJsonFromStream<ErrorResponse>(stream);
                        if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.ErrorDescription))
                        {
                            Logger.Error(errorResponse.ErrorDescription);
                            await CommonHelper.ShowToastAsync(errorResponse.ErrorDescription, ToastType.Error);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                        await Shell.Current.GoToAsync("//LoginPage");
                    string errorMsg = await CommonHelper.StreamToStringAsync(stream);
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        Logger.Error(errorMsg);
                        await CommonHelper.ShowToastAsync(errorMsg, ToastType.Error);
                    }
                };
            };
            return default(T);
        }
        public async Task<T> AnonymousPostAsync<T>(string url, HttpContent content)
        {
            if (!await CommonHelper.GetIsConnecttedAsync())
                return default(T);

            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
                client.Timeout = TimeSpan.FromMinutes(60);
                using (HttpResponseMessage response = await client.PostAsync(Config.BaseURL + url, content))
                {
                    //if (response.IsSuccessStatusCode)
                    //    return CommonHelper.ConvertToObject<T>(await response.Content.ReadAsStringAsync());
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        if (response.IsSuccessStatusCode)
                            return CommonHelper.DeserializeJsonFromStream<T>(stream);
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorResponse errorResponse = CommonHelper.DeserializeJsonFromStream<ErrorResponse>(stream);
                            if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.ErrorDescription))
                            {
                                Logger.Error(errorResponse.ErrorDescription);
                                await CommonHelper.ShowToastAsync(errorResponse.ErrorDescription, ToastType.Error);
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            await Shell.Current.GoToAsync("//LoginPage");
                        string errorMsg = await CommonHelper.StreamToStringAsync(stream);

                        if (!string.IsNullOrEmpty(errorMsg))
                        {
                            Logger.Error(errorMsg);
                            await CommonHelper.ShowToastAsync(errorMsg, ToastType.Error);
                        }
                    }
                };
            };
            return default(T);
        }
    }
}
