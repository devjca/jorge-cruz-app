using BsusinessLogic.Contracts;
using DataAccess.Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using System.Globalization;

namespace BsusinessLogic
{
    public class ProjectionDataBL : IProjectionDataBL
    {
        public readonly IGenericRepository _genericRepository;
        public ProjectionDataBL(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

       
        public List<ProjectionResponseDto> GetAllProjections(RequestDto requestDto) 
        {
            var results = new List<ProjectionResponseDto>();
            try
            {
                if (requestDto != null)
                {
                    if (requestDto.ProjectioId != 0)
                    {
                        var getProjection = GetProjectionById(requestDto.ProjectioId);
                        results.Add(getProjection);
                    }
                    else 
                    {
                        var startDate = DateTime.ParseExact(requestDto.StartProjection,"MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        var endDate = DateTime.ParseExact(requestDto.EndProjection, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        var projections = _genericRepository.GetAll<DataAccess.Context.Projection>()
                            .Where(w => w.StartDate >= startDate && w.StartDate <= endDate).Select(s => s.ProjectionId).ToList();

                        if (projections.Any())
                        {
                            foreach (var proectionId in projections)
                            {
                                var getProjection = GetProjectionById(proectionId);
                                if (getProjection != null)
                                {
                                    results.Add(getProjection);
                                }
                                
                            }
                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public ProjectionResponseDto GetProjectionById(int projectionId)
        {
            var response = new ProjectionResponseDto();
            var HeaderProjections = new List<HeaderProjection>();

            try
            {
                var projection = _genericRepository.GetbyId<DataAccess.Context.Projection>(projectionId);

                if (projection != null)
                {
                    var actual = _genericRepository.GetAll<DataAccess.Context.Actual>().Where(w => w.ProjectionId == projectionId);
                    var xmlInfo = DeserializeXml(projection.Xml);
                    response.Projection = projection.ProjectionId;

                    foreach (var item in xmlInfo)
                    { 
                        var headerProjection = new HeaderProjection();

                        var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.MonthYear.Month);
                        
                        headerProjection.MonthYear = $"{monthName} {item.MonthYear.Year}";
                        headerProjection.Activity = item.Description;
                        headerProjection.Estimated = item.Deposit;
                        
                        var detailProjections = actual.Where(s => s.DateDeposited == item.MonthYear).Select(s => new DetailProjection()
                        {
                            Activity = s.Memo,
                            Estimated = item.Payment,
                            Actual = Convert.ToDouble(s.Amount),
                            EstimatedBalance = item.Projected

                        }).ToList();
                        
                        headerProjection.DetailProjections = detailProjections;
                       
                        HeaderProjections.Add(headerProjection);
                    }

                    response.HeaderProjection = HeaderProjections;

                }
            }
            catch (Exception exception)
            {
                var message = exception;
                response = null;
                throw;
            }

            return response;
        }

        private IEnumerable<ProjectionDescriptionDto> DeserializeXml(string xmlString)
        {
            var result = new List<ProjectionDescriptionDto>();
            
            var xmlfinal = xmlString.Replace("rs:", "").Replace("z:", "");

            var xDoc = XDocument.Parse(xmlfinal);
            var data = xDoc.Descendants("data").ToList();

            var rows = xDoc.Descendants("row").ToList();
            var count = 0;
            foreach (var item in rows)
            {
                if (count > 0 && item.Attribute("Deposit") != null)
                {
                    var projection = new ProjectionDescriptionDto();
                    projection.Description = item.Attribute("Description").Value;
                    projection.Deposit =  double.Parse(item.Attribute("Deposit").Value); 
                    projection.MonthYear = DateTime.Parse(item.Attribute("MonthYear").Value);
                    projection.Payment = double.Parse(item.Attribute("Payment").Value);
                    projection.Projected = double.Parse(item.Attribute("Projected").Value);

                    result.Add(projection);
                }

                count += 1;
                
            }

            return result;
        }

    }
}
