using Microsoft.EntityFrameworkCore;
using SoftwareCompaniesManagement.Authorization.Model;

namespace SoftwareCompaniesManagement.Authorization.Data;

public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
}
