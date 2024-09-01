using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stu_card_api.interfaces;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace stu_card_api.Services
{
    public class DataAcquisitionService : IDataAcquisitionService
    {

        static bool isConnection = false;
        static int count = 1;
        static int errCount = 1;
        public List<string> backgroundPrompts = new()
        {
            "An ID card on the marble floor. The card has a blue background on the right and a gray blank on the left. The colors on both sides should not have any other coverage. The colors on both sides can have a certain curvature or not. The colors on both sides should be bright, and the marble floor surface and card should have sufficient light. Smooth. Don't have any content. Accurate card shadows and highlights. The perspective of generating images cannot be too small. Generate strictly according to my requirements",
            "An ID card on a rough wooden table. The card has a blue background on the right and a gray blank on the left. The colors on the left and right sides should not have any other coverage. The colors on the left and right sides can have a certain curvature or not. The colors on the left and right sides should be bright, the card should have sufficient lighting, and be smooth. Don't have any content. Accurate card shadows and highlights. The perspective of generating images cannot be too small. Generate strictly according to my requirements",
            "An ID card on the wooden floor. The card has a blue background on the right and a gray blank on the left. The colors on the left and right sides should not have any other coverage. The colors on the left and right sides can have a certain curvature or not. The colors on the left and right sides should be bright, the card should have sufficient lighting, and be smooth. Don't have any content. Accurate card shadows and highlights. The perspective of generating images cannot be too small. Generate strictly according to my requirements",
            "An ID card on the bed. The card background has a blue background on the right and a gray blank on the left. The colors on the left and right sides cannot have other coverings. The colors on the left and right sides can have a certain curvature or not. The left and right colors should be bright, and the card should have sufficient three-dimensionality and light and be smooth. Don't have any content. Brightness and highlights are accurate. The viewing angle of the generated image cannot be too small. Generated strictly according to my requirements"
        };

        readonly HttpClient httpClient;
        readonly IFileService fileService;
        public DataAcquisitionService(IFileService fileService)
        {
            this.httpClient = new();
            this.fileService = fileService;

        }

        public async Task<bool> BackgroundCollection()
        {
            try
            {
                var random = new Random().Next(0, backgroundPrompts.Count - 1);
                await Console.Out.WriteLineAsync($"生成随机数：{random}");
                string prompt = backgroundPrompts[random];
                await Console.Out.WriteLineAsync($"获取到prompt：{prompt}");

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api-flux1.api4gpt.com/?prompt=?" + prompt);
                var response = await this.httpClient.SendAsync(httpRequestMessage);
                var code = response.EnsureSuccessStatusCode();
                await Console.Out.WriteLineAsync($"请求code：{code}");
                var stream = await response.Content.ReadAsStreamAsync();
                long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "";

                await this.fileService.PostStreamAsync(stream, contentType, timestamp + Units.Units.GetFileSuffix(contentType), "background");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> PersionCollection()
        {
            var str = "{\r\n    \"data\": [\r\n        \"a real handsome man with random hair, blue background ID photo\",\r\n        \"\",\r\n        1407930266,\r\n        true,\r\n        1024,\r\n        1024,\r\n        5,\r\n        28\r\n    ],\r\n    \"event_data\": null,\r\n    \"fn_index\": 1,\r\n    \"trigger_id\": 5,\r\n    \"session_hash\": \"84e30s7gnsc\"\r\n}";
            var request = new HttpRequestMessage(HttpMethod.Post, "https://stabilityai-stable-diffusion-3-medium.hf.space/queue/join?__theme=light")
            {
                Content = new StringContent(str)
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("zh-CN", 0.9));
            request.Headers.Add("Origin", "https://stabilityai-stable-diffusion-3-medium.hf.space");
            request.Headers.Add("Priority", "u=1, i");
            request.Headers.Referrer = new Uri("https://stabilityai-stable-diffusion-3-medium.hf.space/?__theme=light");
            request.Headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"128\", \"Not;A=Brand\";v=\"24\", \"Google Chrome\";v=\"128\"");
            request.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
            request.Headers.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
            request.Headers.Add("Sec-Fetch-Dest", "empty");
            request.Headers.Add("Sec-Fetch-Mode", "cors");
            request.Headers.Add("Sec-Fetch-Site", "same-origin");
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36");
            request.Headers.Add("X-Zerogpu-Token", "eyJhbGciOiJFZERTQSJ9.eyJyZWFkIjp0cnVlLCJwZXJtaXNzaW9ucyI6eyJyZXBvLmNvbnRlbnQucmVhZCI6dHJ1ZX0sIm9uQmVoYWxmT2YiOnsia2luZCI6InVzZXIiLCJfaWQiOiI2NmE0YmU3OGY5NTY1NjM1ZWJkMjcwMTYiLCJ1c2VyIjoienlqYWNrIiwiaXNQcm8iOmZhbHNlfSwiaWF0IjoxNzI1MTMxMDI0LCJzdWIiOiIvc3BhY2VzL3N0YWJpbGl0eWFpL3N0YWJsZS1kaWZmdXNpb24tMy1tZWRpdW0iLCJleHAiOjE3MjUxMzEyMDUsImlzcyI6Imh0dHBzOi8vaHVnZ2luZ2ZhY2UuY28ifQ.Z1J2ZQxm25ejW1b2WIWEvAZuxq0cqaVY4o1swzTkS7Jtk_-VT0rE5VYefoo_49qz-xDpfB-jLJ15YG4wnS5tCg");
            var gen = await this.httpClient.SendAsync(request);
            await Console.Out.WriteLineAsync("发送生成请求");
            if (!gen.IsSuccessStatusCode)
            {
                errCount++;
                await Console.Out.WriteLineAsync("生成请求发送失败！,错误信息" + gen.Content);
                return false;
            }
            HttpClient http2 = new();
            var response = await http2.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://stabilityai-stable-diffusion-3-medium.hf.space/queue/data?session_hash=84e30s7gnsc"), HttpCompletionOption.ResponseHeadersRead);
            var code = response.EnsureSuccessStatusCode();
            await Console.Out.WriteLineAsync($"生成code：{code}");
            if (!code.IsSuccessStatusCode)
            {
                errCount++;
                await Console.Out.WriteLineAsync($"生成失败，信息：" + response.Content);
                return false;
            }
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            await Console.Out.WriteLineAsync("图片生成中....");
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    string patternDto = @"^\s*data:\s*";
                    string jsonData = Regex.Replace(line, patternDto, "", RegexOptions.IgnoreCase);

                    if (jsonData.Contains("output")) 
                    {
                        JObject jsonObject = JObject.Parse(jsonData);
                        JArray dataArray = (JArray)jsonObject["output"]["data"];
                        if (dataArray == null) continue;
                        // 移除非对象元素
                        for (int i = dataArray.Count - 1; i >= 0; i--)
                        {
                            if (!(dataArray[i] is JObject))
                            {
                                dataArray.RemoveAt(i);
                            }
                        }

                        // 将修改后的 JSON 对象转换回字符串
                        jsonData = jsonObject.ToString();
                    }
                 

                    var result = JsonConvert.DeserializeObject<ProgressMessage>(jsonData);
                    if (result?.Output != null)
                    {
                        var urlData = result.Output.Data?.FirstOrDefault();
                        if (urlData != null && !string.IsNullOrWhiteSpace(urlData.url))
                        {
                            var res = await this.fileService.PostUrlAsync(urlData.url, "headportrait");
                            if (res > 0)
                            {
                                await Console.Out.WriteLineAsync($"采集成功，共采集到{count}张");
                                count++;
                            }
                            else
                            {
                                await Console.Out.WriteLineAsync($"采集失败共失败{errCount}张");
                                errCount++;
                            }
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync($"采集失败共失败{errCount}张");
                            errCount++;
                        }

                    }
                    else if (result?.progress_data != null)
                    {
                        var data = result.progress_data.FirstOrDefault();
                        await Console.Out.WriteLineAsync($"当前生成步骤:{data?.Index}步，共{data?.Length}步");
                    }

                }
            }

            return true;
        }
    }


    public class ProgressData
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string Unit { get; set; }
        public object Progress { get; set; }
        public object Desc { get; set; }
    }

    public class ProgressMessage
    {
        public string Msg { get; set; }
        public string EventId { get; set; }
        public List<ProgressData> progress_data { get; set; }
        public Output Output { get; set; }
    }

    public class OutptuPath
    {
        public string url { get; set; }
    }
    public class Output
    {
        public List<OutptuPath> Data { get; set; }
        public bool IsGenerating { get; set; }
        public double Duration { get; set; }
        public double AverageDuration { get; set; }
        public object RenderConfig { get; set; }
        public List<object> ChangedStateIds { get; set; }
    }


}
