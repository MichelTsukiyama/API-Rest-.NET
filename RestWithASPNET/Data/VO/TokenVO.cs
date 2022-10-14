namespace RestWithASPNET.Data.VO
{
    public class TokenVO
    {
        public TokenVO(bool authenticated, string created, string expiration, string accessToken, string refreshToken)
        {
            this.Authenticated = authenticated;
            this.Created = created;
            this.Expiration = expiration;
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;

        }
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}