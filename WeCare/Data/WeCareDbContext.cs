using WeCare.Models;
using WeCare.Security;

namespace WeCare.Data;

public class WeCareDbContext
{
    public List<User> Users { get; } = new();
    public List<Donor> Donors { get; } = new();
    public List<Seeker> Seekers { get; } = new();
    public List<Hospital> Hospitals { get; } = new();
    public List<WeCareOrganization> WeCareOrganizations { get; } = new();
    public List<AccountStatus> AccountStatuses { get; } = new();
    public List<UserType> UserTypes { get; } = new();
    public List<Campaign> Campaigns { get; } = new();
    public List<EventTable> Events { get; } = new();
    public List<EventResponseTable> EventResponses { get; } = new();

    public InMemoryDatabase Database { get; } = new();

    public WeCareDbContext()
    {
        SeedLookups();
        SeedAdminUser();
        SeedDonors();
    }

    public Task<int> SaveChangesAsync()
    {
        return Task.FromResult(0);
    }

    private void SeedLookups()
    {
        if (AccountStatuses.Count == 0)
        {
            AccountStatuses.Add(new AccountStatus { AccountStatusId = 1, StatusName = "Active" });
            AccountStatuses.Add(new AccountStatus { AccountStatusId = 2, StatusName = "Inactive" });
        }

        if (UserTypes.Count == 0)
        {
            UserTypes.Add(new UserType { UserTypeId = 1, TypeName = "Donor" });
            UserTypes.Add(new UserType { UserTypeId = 2, TypeName = "Seeker" });
            UserTypes.Add(new UserType { UserTypeId = 3, TypeName = "Staff" });
            UserTypes.Add(new UserType { UserTypeId = 4, TypeName = "Hospital" });
            UserTypes.Add(new UserType { UserTypeId = 5, TypeName = "Organization" });
            UserTypes.Add(new UserType { UserTypeId = 6, TypeName = "Admin" });
        }
    }

    private void SeedAdminUser()
    {
        if (Users.Any(u => u.EmailAddress.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        Users.Add(new User
        {
            UserId = 1,
            UserName = "System Admin",
            EmailAddress = "admin@gmail.com",
            PasswordHash = PasswordHasher.HashPassword("12345678"),
            AccountStatusId = 1,
            UserTypeId = 6
        });
    }

    private void SeedDonors()
    {
        if (Donors.Count > 0)
        {
            return;
        }

        Donors.AddRange(new[]
        {
            new Donor
            {
                DonorId = 1,
                FullName = "Amina Rahman",
                Gender = "Female",
                BloodGroup = "A+",
                LastDonationDate = DateTime.Today.AddDays(-45),
                ContactInfo = "amina.rahman@example.com",
                Address = "Dhanmondi, Dhaka",
                JobStatus = "Student",
                ContactNo = "+8801700000001"
            },
            new Donor
            {
                DonorId = 2,
                FullName = "Rafiul Islam",
                Gender = "Male",
                BloodGroup = "O+",
                LastDonationDate = DateTime.Today.AddDays(-80),
                ContactInfo = "rafiul.islam@example.com",
                Address = "Gulshan, Dhaka",
                JobStatus = "Engineer",
                ContactNo = "+8801700000002"
            },
            new Donor
            {
                DonorId = 3,
                FullName = "Nadia Sultana",
                Gender = "Female",
                BloodGroup = "B-",
                LastDonationDate = DateTime.Today.AddDays(-120),
                ContactInfo = "nadia.sultana@example.com",
                Address = "Uttara, Dhaka",
                JobStatus = "Teacher",
                ContactNo = "+8801700000003"
            },
            new Donor
            {
                DonorId = 4,
                FullName = "Imran Chowdhury",
                Gender = "Male",
                BloodGroup = "AB+",
                LastDonationDate = DateTime.Today.AddDays(-30),
                ContactInfo = "imran.chowdhury@example.com",
                Address = "Chittagong",
                JobStatus = "Doctor",
                ContactNo = "+8801700000004"
            },
            new Donor
            {
                DonorId = 5,
                FullName = "Farzana Karim",
                Gender = "Female",
                BloodGroup = "O-",
                LastDonationDate = DateTime.Today.AddDays(-200),
                ContactInfo = "farzana.karim@example.com",
                Address = "Sylhet",
                JobStatus = "Banker",
                ContactNo = "+8801700000005"
            },
            new Donor
            {
                DonorId = 6,
                FullName = "Mahmud Hasan",
                Gender = "Male",
                BloodGroup = "A-",
                LastDonationDate = DateTime.Today.AddDays(-65),
                ContactInfo = "mahmud.hasan@example.com",
                Address = "Rajshahi",
                JobStatus = "Freelancer",
                ContactNo = "+8801700000006"
            },
            new Donor
            {
                DonorId = 7,
                FullName = "Sara Ahmed",
                Gender = "Female",
                BloodGroup = "B+",
                LastDonationDate = DateTime.Today.AddDays(-95),
                ContactInfo = "sara.ahmed@example.com",
                Address = "Khulna",
                JobStatus = "Designer",
                ContactNo = "+8801700000007"
            },
            new Donor
            {
                DonorId = 8,
                FullName = "Tanvir Rahman",
                Gender = "Male",
                BloodGroup = "AB-",
                LastDonationDate = DateTime.Today.AddDays(-150),
                ContactInfo = "tanvir.rahman@example.com",
                Address = "Barisal",
                JobStatus = "Analyst",
                ContactNo = "+8801700000008"
            }
        });
    }
}

public class InMemoryDatabase
{
    public Task<InMemoryTransaction> BeginTransactionAsync()
    {
        return Task.FromResult(new InMemoryTransaction());
    }
}

public class InMemoryTransaction : IAsyncDisposable
{
    public Task CommitAsync() => Task.CompletedTask;

    public Task RollbackAsync() => Task.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
