using IstanbulLMC.DTOs;
using Newtonsoft.Json;

static public class RecaptchaService
{
    static public async Task<bool> IsCapchaValid(string googleCaptchToken, string RemoteIpAddress)
    {
        var secret = "6LcU9k8pAAAAAPHiLb_X2tUwNCUR7l7XluH7-7hv";
        using (var client = new HttpClient())
        {
            var valuse = new Dictionary<string, string>
            {
                {"secret", secret},
                {"response", googleCaptchToken},
                {"remotipe", RemoteIpAddress},
            };

            var content = new FormUrlEncodedContent(valuse);
            var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            var captchaResponseJson = await verify.Content.ReadAsStringAsync();
            CaptchaResponse? captchaResult = JsonConvert.DeserializeObject<CaptchaResponse>(captchaResponseJson);
            if (captchaResult != null)
            {
                return captchaResult.Success
                        && captchaResult.Action == "Sparis"
                        && captchaResult.Score > 0.5;
            }
            else
            {
                return false;
            }

        }
    }
}