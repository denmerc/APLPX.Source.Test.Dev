using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping Workflow display and client entities.
    /// </summary>
    public static class WorkflowMapper
    {
        public static Display.Workflow ToDisplayEntity(this Client.Workflow dto)
        {
            var result = new Display.Workflow();

            result.Title = dto.Title;

            //foreach (var step in dto.Steps)
            //{
            //    result.Steps.Add(step.ToDisplayEntity());
            //}
            result.TypeCode = dto.ThisWorkflowType;

            return result;
        }

        public static APLPX.Client.Entity.Workflow ToDTO(this Display.Workflow displayEntity)
        {
            var result = new Client.Workflow();

            result.Title = displayEntity.Title;
            result.ThisWorkflowType = displayEntity.TypeCode;

            foreach (var step in displayEntity.Steps)
            {
                //result.Steps.Add(step.ToDTO());
            }

            return result;
        }
    }
}
