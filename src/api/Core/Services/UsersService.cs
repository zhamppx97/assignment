using api.Domain;

namespace api.Core;

public class UsersService(IUsersRepository repository) : IUsersService
{
    private readonly IUsersRepository _repository = repository;

    public async Task<UserServiceResponse<List<UsersResponse>>> SelectAsync()
    {
        var users = await _repository.GetAllAsync();
        List<UsersResponse> usersResponses = [];
        foreach (var item in users)
        {
            if (item.IsActive)
            {
                usersResponses.Add(new UsersResponse()
                {
                    UserId = item.UserId,
                    Hn = $"{item.Hn:00000}",
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNo = item.PhoneNo,
                    Email = item.Email,
                    IsActive = item.IsActive,
                    CreateDate = item.CreateDate,
                    CreateBy = item.CreateBy,
                    UpdateDate = item.UpdateDate,
                    UpdateBy = item.UpdateBy,
                });
            }
        }
        UserServiceResponse<List<UsersResponse>> res = new()
        {
            Data = usersResponses
        };
        return res;
    }

    public async Task<UserServiceResponse<Users>> InsertAsync(UserServiceRequest req)
    {
        UserServiceResponse<Users> res = new();
        bool isPhoneNoAlreadyUsed = await (_repository.DoesExist(c => c.PhoneNo == req.PhoneNo && c.IsActive));
        if (isPhoneNoAlreadyUsed)
        {
            res.Code = 4000;
            res.Messages = "phone numbers already exists";
            return res;
        }
        DateTime dateNow = DateTime.Now;
        Users entity = new()
        {
            Hn = req.Hn,
            FirstName = req.FirstName,
            LastName = req.LastName,
            PhoneNo = req.PhoneNo,
            Email = req.Email,
            IsActive = true,
            CreateDate = dateNow,
            UpdateDate = dateNow,
        };
        await _repository.InsertAsync(entity);
        await _repository.SaveChangesAsync();
        return res;
    }

    public async Task<UserServiceResponse<Users>> UpdateAsync(UserServiceRequest req)
    {
        UserServiceResponse<Users> res = new();
        Users user = await _repository.GetByIdAsync(req.UserId);
        if (user is null)
        {
            res.Code = 4000;
            res.Messages = "user does not exists";
            return res;
        }
        DateTime dateNow = DateTime.Now;
        user.FirstName = req.FirstName;
        user.LastName = req.LastName;
        user.PhoneNo = req.PhoneNo;
        user.Email = req.Email;
        user.UpdateDate = dateNow;
        _repository.Update(user);
        await _repository.SaveChangesAsync();
        return res;
    }

    public async Task<UserServiceResponse<Users>> DeleteAsync(Guid userId)
    {
        UserServiceResponse<Users> res = new();
        Users user = await _repository.GetByIdAsync(userId);
        if (user is null)
        {
            res.Code = 4000;
            res.Messages = "user does not exists";
            return res;
        }
        DateTime dateNow = DateTime.Now;
        user.IsActive = false;
        user.UpdateDate = dateNow;
        _repository.Update(user);
        await _repository.SaveChangesAsync();
        return res;
    }
}
