namespace StudyGarden.Infrastructure.Abstractions;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string hashedPassword, string providedPassword);
}