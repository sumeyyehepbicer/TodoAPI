namespace Todo.Infrastructure.Implementation.Services.UserServices
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string Username { get; }
        string ParentId { get; }
    }
}