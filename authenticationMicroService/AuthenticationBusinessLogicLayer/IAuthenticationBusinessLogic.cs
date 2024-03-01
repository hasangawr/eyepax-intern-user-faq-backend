using AuthenticationDataAccessLayer.Entities;


namespace AuthenticationBusinessLogicLayer
{
    public interface IAuthenticationBusinessLogic
    {
        Task<string> Authenticate(ReqUser model);
       
    }
}
