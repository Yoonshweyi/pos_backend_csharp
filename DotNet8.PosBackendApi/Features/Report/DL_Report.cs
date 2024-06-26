﻿using DotNet8.PosBackendApi.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotNet8.PosBackendApi.Features.Report;

public class DL_Report
{
    private readonly AppDbContext _context;
    private readonly DapperService _dapperService;
    public DL_Report(AppDbContext context, DapperService dapperService)
    {
        _context = context;
        _dapperService = dapperService;
    }

    public async Task<MonthlyReportResponseModel> DailyReport(int dateDay, int dateMonth, int dateYear, int pageNo, int pageSize)
    {
        MonthlyReportResponseModel responseModel = new MonthlyReportResponseModel();
        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x => x.SaleInvoiceDateTime.Day >= dateDay && x.SaleInvoiceDateTime.Month >= dateMonth && x.SaleInvoiceDateTime.Year == dateYear)
            .GroupBy(x => x.SaleInvoiceDateTime.Date)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);
        int totalCount = query.Count();
        int pageCount = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            pageCount = pageCount + 1;
        }
        var report = await query
                .Pagination(pageNo, pageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
            ? new MessageResponseModel(true, EnumStatus.Success.ToString())
            : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<MonthlyReportResponseModel> DailyReportV1(SaleDailyReportRequestModel requestModel)
    {
        MonthlyReportResponseModel responseModel = new MonthlyReportResponseModel();
        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x =>
                x.SaleInvoiceDateTime.Day >= requestModel.FromDate.Day &&
                x.SaleInvoiceDateTime.Month >= requestModel.FromDate.Month &&
                x.SaleInvoiceDateTime.Year == requestModel.FromDate.Year)
            .GroupBy(x => x.SaleInvoiceDateTime.Date)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);

        int totalCount = query.Count();
        int pageCount = totalCount / requestModel.PageSetting.PageSize;
        if (totalCount % requestModel.PageSetting.PageSize != 0)
        {
            pageCount = pageCount + 1;
        }
        var report = await query
                .Pagination(
                requestModel.PageSetting.PageNo,
                requestModel.PageSetting.PageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(
                requestModel.PageSetting.PageNo,
                requestModel.PageSetting.PageSize,
                pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
            ? new MessageResponseModel(true, EnumStatus.Success.ToString())
            : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<MonthlyReportResponseModel> MonthlyReport(int month, int year, int pageNo, int pageSize)
    {
        MonthlyReportResponseModel responseModel = new MonthlyReportResponseModel();
        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x => x.SaleInvoiceDateTime.Month == month && x.SaleInvoiceDateTime.Year == year)
            .GroupBy(x => x.SaleInvoiceDateTime.Date)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);
        int totalCount = query.Count();
        int pageCount = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            pageCount = pageCount + 1;
        }

        var report = await query
                .Pagination(pageNo, pageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
                ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<MonthlyReportResponseModel> YearlyReport(int year, int pageNo, int pageSize)
    {
        MonthlyReportResponseModel responseModel = new MonthlyReportResponseModel();

        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x => x.SaleInvoiceDateTime.Year == year)
            .GroupBy(x => x.SaleInvoiceDateTime.Date)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);
        int totalCount = query.Count();
        int pageCount = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            pageCount = pageCount + 1;
        }
        var report = await query
                .Pagination(pageNo, pageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
            ? new MessageResponseModel(true, EnumStatus.Success.ToString())
            : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<ReportResponseModel> DailyReport(DateTime fromDate, DateTime toDate, int pageNo, int pageSize)
    {
        ReportResponseModel responseModel = new ReportResponseModel();
        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x => x.SaleInvoiceDateTime >= fromDate && x.SaleInvoiceDateTime <= toDate)
            .GroupBy(x => x.SaleInvoiceDateTime.Date)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);

        int totalCount = query.Count();
        int pageCount = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            pageCount = pageCount + 1;
        }
        var report = await query
                .Pagination(pageNo, pageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
            ? new MessageResponseModel(true, EnumStatus.Success.ToString())
            : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<ReportResponseModel> MonthlyReport(DateTime fromDate, DateTime toDate, int pageNo, int pageSize)
    {
        ReportResponseModel responseModel = new ReportResponseModel();
        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x => x.SaleInvoiceDateTime.Month >= fromDate.Month && x.SaleInvoiceDateTime.Month <= toDate.Month && 
                        x.SaleInvoiceDateTime.Year >= fromDate.Year && x.SaleInvoiceDateTime.Year <= toDate.Year)
            .GroupBy(x => x.SaleInvoiceDateTime.Month)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);
        int totalCount = query.Count();
        int pageCount = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            pageCount = pageCount + 1;
        }

        var report = await query
                .Pagination(pageNo, pageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
                ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<MonthlyReportResponseModel> YearlyReport(DateTime fromDate, DateTime toDate, int pageNo, int pageSize)
    {
        MonthlyReportResponseModel responseModel = new MonthlyReportResponseModel();

        var query = _context
            .TblSaleInvoices
            .AsNoTracking()
            .Where(x => x.SaleInvoiceDateTime.Year >= fromDate.Year && x.SaleInvoiceDateTime.Year <= toDate.Year)
            .GroupBy(x => x.SaleInvoiceDateTime.Year)
            .Select(y => new ReportModel
            {
                SaleInvoiceDate = y.First().SaleInvoiceDateTime,
                TotalAmount = y.Sum(c => c.TotalAmount)
            }).OrderBy(x => x.SaleInvoiceDate);

        int totalCount = query.Count();
        int pageCount = totalCount / pageSize;
        if (totalCount % pageSize != 0)
        {
            pageCount = pageCount + 1;
        }
        var report = await query
                .Pagination(pageNo, pageSize)
                .ToListAsync();

        responseModel.Data = report;
        responseModel.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, totalCount);
        responseModel.MessageResponse = responseModel.Data.Count > 0
            ? new MessageResponseModel(true, EnumStatus.Success.ToString())
            : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
        return responseModel;
    }

    public async Task<DashboardResponseModel> GetDataForDashboard(DashboardRequestModel requestModel)
    {
        DashboardResponseModel responseModel = new DashboardResponseModel();
        try
        {
            var parameters = new { SaleInvoiceDate = requestModel.SaleInvoiceDate };
            var result = await _dapperService.QueryMultipleAsync<BestSellerProductDashboardModel, DailyDashboardModel, WeeklyDashboardModel, MonthlyDashboardModel, YearlyDashboardModel>("sp_Dashboard", parameters);
            if (result.Item5 is null)
            {
                responseModel.MessageResponse = new MessageResponseModel(false, EnumStatus.NotFound.ToString());
                goto result;
            }

            responseModel.BestProductData = result.Item1.ToList();
            responseModel.DailyData = result.Item2.ToList();
            responseModel.WeeklyData = result.Item3.ToList();
            responseModel.MonthlyData = result.Item4.ToList();
            responseModel.YearlyData = result.Item5.ToList();
            responseModel.MessageResponse = responseModel.YearlyData.Count > 0
                ? new MessageResponseModel(true, EnumStatus.Success.ToString())
                : new MessageResponseModel(false, EnumStatus.NotFound.ToString());
            return responseModel;
        }
        catch (Exception ex)
        {
            responseModel.BestProductData = new List<BestSellerProductDashboardModel>();
            responseModel.DailyData = new List<DailyDashboardModel>();
            responseModel.WeeklyData = new List<WeeklyDashboardModel>();
            responseModel.MonthlyData = new List<MonthlyDashboardModel>();
            responseModel.YearlyData = new List<YearlyDashboardModel>();
            responseModel.MessageResponse = new MessageResponseModel(false, ex.Message);
        }

    result:
        return responseModel;
    }
}