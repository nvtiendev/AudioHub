using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using AudioHub.Core.Exceptions;

namespace AudioHub.Core.Utilities
{
    internal static class Utils
    {
        static Random random = Random.Shared;

        #region UA
        static string ChromeUserAgent()
        {
            var major = random.Next(150, 250);
            var build = random.Next(4000);
            var branchBuild = random.Next(200);

            return $"Mozilla/5.0 ({RandomWindowsVersion()}) AppleWebKit/537.36 (KHTML, like Gecko) " +
                $"Chrome/{major}.0.{build}.{branchBuild} Safari/537.36";
        }

        static string FirefoxUserAgent()
        {
            int version = random.Next(150, 250);
            return $"Mozilla/5.0 ({RandomWindowsVersion()}; rv:{version}.0) Gecko/20100101 Firefox/{version}.0";
        }

        static string MicrosoftEdgeUserAgent()
        {
            var major = random.Next(150, 250);
            var build = random.Next(4000);
            var branchBuild = random.Next(200);
            return $"Mozilla/5.0 ({RandomWindowsVersion()}) AppleWebKit/537.36 (KHTML, like Gecko) " +
                $"Chrome/{major}.0.{build}.{branchBuild} Safari/537.36 Edg/{major}.0.{build}.{branchBuild}";
        }

        static string AudioClientUserAgent() => "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) audiohub/1.0.0 Chrome/102.0.5005.167 Electron/19.1.9 Safari/537.36";

        internal static string RandomUserAgent()
        {
            var rand = random.Next(99) + 1;
            if (rand <= 40)
                return ChromeUserAgent();
            else if (rand <= 75)
                return MicrosoftEdgeUserAgent();
            else if (rand <= 95)
                return FirefoxUserAgent();
            return AudioClientUserAgent();
        }
        #endregion

        static string RandomWindowsVersion()
        {
            var windowsVersion = "Windows NT ";
            var num = random.Next(99) + 1;

            if (num >= 1 && num <= 45)
                windowsVersion += "10.0";
            else if (num > 45 && num <= 80)
                windowsVersion += "6.1";
            else if (num > 80 && num <= 95)
                windowsVersion += "6.3";
            else
                windowsVersion += "6.2";

            if (random.NextDouble() <= 0.65)
                windowsVersion += random.NextDouble() <= 0.5 ? "; WOW64" : "; Win64; x64";

            return windowsVersion;
        }

        internal static string HashSHA512(string text, string secretKey) => BitConverter.ToString(new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)).ComputeHash(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();

        internal static string HashSHA256(string text)
        {
            return BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(text)), 0).Replace("-", "").ToLower();
        }

        internal static string ChainParams(Dictionary<string, object> parameters)
        {
            string urlParam = "";
            foreach (var param in parameters)
            {
                string key = param.Key;
                string value = param.Value.ToString()!;
                urlParam += key + "=" + Uri.EscapeDataString(value) + "&";
            }
            urlParam = urlParam.Remove(urlParam.Length - 1);
            return urlParam;
        }

        internal static void CheckErrorCode(string json, out JsonNode node)
        {
            JsonNode? jsonNode = JsonNode.Parse(json) ?? throw new AudioHubException("The API returned an invalid response");
            int errorCode = jsonNode["err"].GetIntValue();
            if (errorCode != 0)
                throw new AudioHubAPIException(errorCode, jsonNode["msg"].GetStringValue());
            if (!jsonNode.AsObject().ContainsKey("data") || jsonNode["data"] is null)
                throw new AudioHubException("The API does not return any data");
            node = jsonNode["data"]!;
        }
    }
}
