namespace DotNet7.PosBackendApi.Features.Setup.Staff;

public class BL_Staff
{
    private readonly DL_Staff _staff;

    public BL_Staff(DL_Staff staff)
    {
        _staff = staff;
    }

    public async Task<List<StaffModel>> GetStaffs()
    {
        var staffList = await _staff.GetStaffs();
        return staffList;
    }

    public async Task<StaffModel> GetStaff(int id)
    {
        var staff = await _staff.GetStaff(id);
        return staff;
    }

    public async Task<MessageResponseModel> CreateStaff(StaffModel requestModel)
    {
        CheckShopNullValue(requestModel);
        var responseModel = await _staff.CreateStaff(requestModel);
        return responseModel;
    }

    public async Task<MessageResponseModel> UpdateStaff(int id, StaffModel requestModel)
    {
        if (id == 0) throw new Exception("id is 0.");
        CheckShopNullValue(requestModel);
        var responseModel = await _staff.UpdateStaff(id, requestModel);
        return responseModel;
    }

    public async Task<MessageResponseModel> DeleteStaff(int id)
    {
        if (id == 0) throw new Exception("id is 0.");
        var responseModel = await _staff.DeleteStaff(id);
        return responseModel;
    }

    private static void CheckShopNullValue(StaffModel staff)
    {
        if (staff is null)
        {
            throw new Exception("Staff is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.StaffCode))
        {
            throw new Exception("StaffCode is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.StaffName))
        {
            throw new Exception("StaffName is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.MobileNo))
        {
            throw new Exception("Staff MobileNo is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.Address))
        {
            throw new Exception("Staff Address is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.DateOfBirth.ToString()))
        {
            throw new Exception("Staff DateOfBirth is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.Gender))
        {
            throw new Exception("Staff Gender is null.");
        }

        if (string.IsNullOrWhiteSpace(staff.Position))
        {
            throw new Exception("Staff Position is null.");
        }
    }
}