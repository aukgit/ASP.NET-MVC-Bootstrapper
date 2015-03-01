namespace DevBootstrapper.Models.DesignPattern.Interfaces {
    internal interface IDevUser {
        long UserId { get; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
    }
}