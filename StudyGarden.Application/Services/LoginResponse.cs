namespace StudyGarden.Contracts.UserRequests;

public record LoginResponse(
    string Token,
    int ID
);