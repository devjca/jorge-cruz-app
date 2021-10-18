using Entities;
using System.Collections.Generic;


namespace BsusinessLogic.Contracts
{
    public interface IProjectionDataBL
    {
        List<ProjectionResponseDto> GetAllProjections(RequestDto requestDto);
        ProjectionResponseDto GetProjectionById(int projectionId);
    }
}
