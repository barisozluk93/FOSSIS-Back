using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using ProjectManagement.DbContexts;
using ProjectManagement.Entity;
using ProjectManagement.Interfaces;
using ProjectManagement.Model;
using System.Data;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectManagementContext _dbContext;
        private readonly IConfiguration configuration;


        public ProjectService(ProjectManagementContext dbContext, IConfiguration _configuration)
        {
            _dbContext = dbContext;
            configuration = _configuration;
        }

        public async Task<Result<PagingResult<PagedList<Project>>>> Paginate(PagingParameter pagingParameter, long userId, bool isAdmin, string token)
        {
            var result = new Result<PagingResult<PagedList<Project>>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    IOrderedQueryable<Project> queryable;

                    if (isAdmin)
                    {
                        queryable = _dbContext.Projects.OrderBy(o => o.Id);
                    }
                    else
                    {
                        queryable = _dbContext.Projects.Where(x => x.UserId == userId && !x.IsDeleted).OrderBy(o => o.Id);
                    }
                    
                    var pagination = PagedList<Project>.ToPagedList(queryable, pagingParameter.PageNumber, pagingParameter.PageSize);

                    pagination.ForEach(p => p.Panel = (p.PanelId.HasValue ? GetPanel(p.PanelId.Value, token).Result : null));
                    result.SetData(new PagingResult<PagedList<Project>>()
                    {
                        Items = pagination,
                        TotalCount = pagination.TotalCount,
                        TotalPages = pagination.TotalPages
                    });

                    result.SetMessage("İşlem başarı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<List<Project>>> GetProjects()
        {
            var result = new Result<List<Project>>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var data = await _dbContext.Projects.Where(x => !x.IsDeleted).ToListAsync();

                    result.SetData(data);
                    result.SetMessage("İşlem başarı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        
        public async Task<Result<List<PvCalcMonthly>>> GetPvCalcMonthly(PvcCalcMonthlyParam pvcCalcMonthlyParam)
        {
            var result = new Result<List<PvCalcMonthly>>();
            List<PvCalcMonthly> pvCalcMonthlies = new List<PvCalcMonthly>();
            var url = "https://re.jrc.ec.europa.eu/api/v5_2/PVcalc";
            var queryParams = BuildQueryParams(pvcCalcMonthlyParam);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(url + queryParams);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonData = JObject.Parse(responseData);
                        var monthlyData = jsonData["outputs"]["monthly"]["fixed"];
                        foreach (var month in monthlyData)
                        {
                            PvCalcMonthly monthly = new PvCalcMonthly();
                            monthly.MonthNo = month["month"].Value<int>();
                            monthly.ProductionkWh = month["E_m"].Value<double>();
                            monthly.Consumption = 0;
                            monthly.SelfConsumption = 0;
                            monthly.ClippedEnergy = 0;
                            pvCalcMonthlies.Add(monthly);
                        }
                        result.SetData(pvCalcMonthlies);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage($"Hata: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<List<SeriesCalcDaily>>> GetSeriesCalcDaily(SeriesCalcDailyParam seriesCalcDailyParam)
        {
            var result = new Result<List<SeriesCalcDaily>>();

            List<SeriesCalcDaily> seriesCalcDaily = new List<SeriesCalcDaily>();

            var url = "https://re.jrc.ec.europa.eu/api/v5_2/seriescalc";
            var queryParams = BuildQueryParams(seriesCalcDailyParam);
            var requestUrl = url + queryParams;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var jsonData = JObject.Parse(responseData);
                        var hourlyData = jsonData["outputs"]?["hourly"];

                        if (hourlyData != null && hourlyData.Type == JTokenType.Array)
                        {
                            var firstDayOfMonth = new DateTime(2020, seriesCalcDailyParam.MountNumber, 1).ToString("yyyyMMdd");

                            foreach (var hour in hourlyData)
                            {
                                var dateTimeString = (string)hour["time"];
                                if (DateTime.TryParseExact(dateTimeString, "yyyyMMdd:HHmm", null, System.Globalization.DateTimeStyles.None, out var date))
                                {
                                    if (date.ToString("yyyyMMdd") == firstDayOfMonth)
                                    {
                                        var energy = (double)hour["P"];
                                        seriesCalcDaily.Add(new SeriesCalcDaily
                                        {
                                            Hour = date.Hour,
                                            PVSystemPowerW = energy,
                                            ClippedEnergy = 0,
                                            Consumption = 0,
                                            SystemCapacity = 0
                                        });
                                    }
                                }
                            }

                            result.SetData(seriesCalcDaily);
                            result.SetMessage("İşlem başarı ile gerçekleşti.");

                            if (!seriesCalcDaily.Any())
                            {
                                result.SetIsSuccess(false);
                                result.SetMessage($"Temmuz ayının ilk gününe ait P verisi bulunamadı.");
                            }
                        }
                        else
                        {
                            result.SetIsSuccess(false);
                            result.SetMessage($"Saatlik veri bulunamadı veya hatalı formatta.");
                        }
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage($"Hata: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    result.SetIsSuccess(false);
                    result.SetMessage($"Hata: {ex.Message}");
                }
            }
            return result;
        }

        public async Task<Result<Project>> Save(Project project)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!_dbContext.Projects.Where(x => (x.Name == project.Name) && !x.IsDeleted).Any())
                    {
                        _dbContext.Add(project);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(project);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Aynı isim veya kodla tanımlı bir yetki bulunmaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<Project>> Update(Project project)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldProject = await _dbContext.Projects.Where(x => x.Id == project.Id && !x.IsDeleted).FirstOrDefaultAsync();

                    if (oldProject != null)
                    {
                        if (!_dbContext.Projects.Where(x => x.Id != oldProject.Id && (x.Name == project.Name) && !x.IsDeleted).Any())
                        {
                            oldProject.Name = project.Name;
                            oldProject.BuildingId = project.BuildingId;
                            oldProject.Location = project.Location;
                            oldProject.RoofArea = project.RoofArea;
                            oldProject.RoofWkt = project.RoofWkt;
                            oldProject.GridSpace = project.GridSpace;
                            oldProject.Margin = project.Margin;
                            oldProject.PanelId = project.PanelId;
                            oldProject.SystemPower = project.SystemPower;

                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();

                            result.SetData(project);
                            result.SetMessage("İşlem başarı ile gerçekleşti.");
                        }
                        else
                        {
                            result.SetIsSuccess(false);
                            result.SetMessage("Aynı isim veya kodla tanımlı bir yetki bulunmaktadır.");
                        }
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<Project>> Delete(long id)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var oldProject = await _dbContext.Projects.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (oldProject != null)
                    {
                        oldProject.IsDeleted = true;

                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();

                        result.SetData(oldProject);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    result.SetIsSuccess(false);
                    result.SetMessage(ex.Message);
                }
            }

            return result;
        }

        public async Task<Result<Project>> GetById(long id)
        {
            var result = new Result<Project>();

            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var project = await _dbContext.Projects.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
                    if (project != null)
                    {
                        result.SetData(project);
                        result.SetMessage("İşlem başarı ile gerçekleşti.");
                    }
                    else
                    {
                        result.SetIsSuccess(false);
                        result.SetMessage("Böyle bir kayıt bulunmamaktadır.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return result;
        }


        private string BuildQueryParams(PvcCalcMonthlyParam param)
        {
            var queryParams = new List<string>();

            if (param.Lat.HasValue)
                queryParams.Add($"lat={param.Lat.Value}");
            if (param.Lon.HasValue)
                queryParams.Add($"lon={param.Lon.Value}");
            if (param.Peakpower.HasValue)
                queryParams.Add($"peakpower={param.Peakpower.Value}");
            if (param.Loss.HasValue)
                queryParams.Add($"loss={param.Loss.Value}");
            if (!string.IsNullOrEmpty(param.Outputformat))
                queryParams.Add($"outputformat={param.Outputformat}");
            if (param.Usehorizon.HasValue)
                queryParams.Add($"usehorizon={param.Usehorizon.Value}");

            return "?" + string.Join("&", queryParams);
        }

        private string BuildQueryParams(SeriesCalcDailyParam param)
        {
            var queryParams = new List<string>();

            if (param.Lat.HasValue)
                queryParams.Add($"lat={param.Lat.Value}");
            if (param.Lon.HasValue)
                queryParams.Add($"lon={param.Lon.Value}");
            if (param.StartYear.HasValue)
                queryParams.Add($"startyear={param.StartYear.Value}");
            if (param.EndYear.HasValue)
                queryParams.Add($"endyear={param.EndYear.Value}");
            if (param.PvCalculation.HasValue)
                queryParams.Add($"pvcalculation={param.PvCalculation.Value}");
            if (param.PeakPower.HasValue)
                queryParams.Add($"peakpower={param.PeakPower.Value}");
            if (param.Loss.HasValue)
                queryParams.Add($"loss={param.Loss.Value}");
            if (!string.IsNullOrEmpty(param.OutputFormat))
                queryParams.Add($"outputformat={param.OutputFormat}");
            if (param.UseHorizon.HasValue)
                queryParams.Add($"usehorizon={param.UseHorizon.Value}");
            
            return "?" + string.Join("&", queryParams);
        }


        private async Task<Panel> GetPanel(long? id, string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(configuration["AppSettings:ApiUrl"] + "/api2/Panel/" + id);

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseStr))
                {
                    try
                    {
                        Result<Panel> result = JsonConvert.DeserializeObject<Result<Panel>>(responseStr);

                        if (result != null)
                        {
                           
                            return result.GetData();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            return null;
        }

    }
}




