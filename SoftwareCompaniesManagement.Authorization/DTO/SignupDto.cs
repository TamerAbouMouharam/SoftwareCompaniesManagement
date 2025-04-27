namespace SoftwareCompaniesManagement.Authorization.DTO;

public record class SignupDto(
    string Username,
    string Password,
    string Role,
    int CompanyId
);
