﻿namespace DotNet8.PosBackendApi.Features.Report;

[Route("api/v1/report")]
[ApiController]
public class ReportController : BaseController
{
    private readonly BL_Report _report;
    private readonly ResponseModel _response;

    public ReportController(IServiceProvider serviceProvider, BL_Report report, ResponseModel response)
        : base(serviceProvider)
    {
        _report = report;
        _response = response;
    }

    [HttpGet("daily-report/{DateDay}/{DateMonth}/{DateYear}/{PageNo}/{PageSize}")]
    public async Task<IActionResult> DailyReport(int DateDay, int DateMonth, int DateYear, int PageNo, int PageSize)
    {
        try
        {
            var lst = await _report.DailyReport(DateDay, DateMonth, DateYear, PageNo, PageSize);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpPost("daily-report")]
    public async Task<IActionResult> DailyReportV1(SaleDailyReportRequestModel requestModel)
    {
        try
        {
            var lst = await _report.DailyReportV1(requestModel);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    //[Route("monthly-report")]
    /*[HttpGet("monthly-report/{month}/{year}/{PageNo}/{PageSize}")]
    public async Task<IActionResult> MonthlyReport(int month, int year, int PageNo, int PageSize)
    {
        try
        {
            var lst = await _report.MonthlyReport(month, year, PageNo, PageSize);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }*/

    //[Route("yearly-report")]

    [HttpGet("yearly-report/{year}/{PageNo}/{PageSize}")]
    public async Task<IActionResult> YearlyReport(int year, int PageNo, int PageSize)
    {
        try
        {
            var lst = await _report.YearlyReport(year, PageNo, PageSize);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpGet("daily-report/{fromDate}/{toDate}/{pageNo}/{pageSize}")]
    public async Task<IActionResult> DailyReport(DateTime fromDate, DateTime toDate, int pageNo, int pageSize)
    {
        try
        {
            var lst = await _report.DailyReport(fromDate, toDate, pageNo, pageSize);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpGet("monthly-report/{fromDate}/{toDate}/{pageNo}/{pageSize}")]
    public async Task<IActionResult> MonthlyReport(DateTime fromDate, DateTime toDate, int pageNo, int pageSize)
    {
        try
        {
            var lst = await _report.MonthlyReport(fromDate, toDate, pageNo, pageSize);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpGet("yearly-report/{fromDate}/{toDate}/{PageNo}/{PageSize}")]
    public async Task<IActionResult> YearlyReport(DateTime fromDate, DateTime toDate, int PageNo, int PageSize)
    {
        try
        {
            var lst = await _report.YearlyReport(fromDate, toDate, PageNo, PageSize);
            var model = _response.Return(
                new ReturnModel
                {
                    Token = RefreshToken(),
                    Count = lst.Data.Count,
                    EnumPos = EnumPos.Report,
                    IsSuccess = lst.MessageResponse.IsSuccess,
                    Message = lst.MessageResponse.Message,
                    Item = lst.Data,
                    PageSetting = lst.PageSetting
                });
            return Content(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}