namespace ProjectShashtra.Data
{
    public class UserRepository:IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBCS");
        }
    }
}
