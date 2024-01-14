using IstanbulLMC.DTOs;
using Newtonsoft.Json;

namespace IstanbulLMC.MicroServices
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            session = _httpContextAccessor.HttpContext.Session;
        }

        public void SetTransferSession(TransferDTO transferDTO)
        {
            session.SetString("transferDTO", JsonConvert.SerializeObject(transferDTO));
        }

        public TransferDTO GetTransferDTO()
        {
            try
            {
                return JsonConvert.DeserializeObject<TransferDTO>(session.GetString("transferDTO"));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
