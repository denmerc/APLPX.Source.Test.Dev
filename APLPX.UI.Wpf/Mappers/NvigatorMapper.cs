using System;
using System.Collections.Generic;

using Client = APLPX.Client.Entity;
using Display = APLX.UI.WPF.DisplayEntities;

namespace APLX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Navigator display and client entities.
    /// </summary>
    public static class NavigatorMapper
    {
        public static Display.Navigator ToDisplayEntity(this Client.Navigator dto)
        {
            var result = new Display.Navigator();

            result.EntityId = dto.EntityId;
            result.NodeTitle = dto.NodeTitle;
            result.NodeHeader = dto.NodeHeader;
            result.NodeCaption = dto.NodeCaption;
            result.Workflow = dto.Workflow;
            result.WorkflowStep = dto.WorkflowStep;
            result.WorkflowGroup = dto.WorkflowGroup;
            result.WorkflowReadonly = dto.WorkflowReadonly;
            
            foreach (var node in dto.Nodes)
            {
                result.Nodes.Add(node.ToDisplayEntity());
            }

            return result;
        }

        public static Client.Navigator ToDTO(this Display.Navigator displayEntity)
        {
            //var result = new Client.Navigator(displayEntity.EntityId, displayEntity.NodeHeader, displayEntity.NodeTitle, displayEntity.NodeCaption, displayEntity.Workflow, displayEntity.WorkflowStep, displayEntity.WorkflowGroup, displayEntity.WorkflowReadonly, displayEntity.Nodes);
            var result = new Client.Navigator();

            result.EntityId = displayEntity.EntityId;
            result.NodeTitle = displayEntity.NodeTitle;
            result.NodeHeader = displayEntity.NodeHeader;
            //result.NodeCaption = displayEntity.NodeCaption;
            result.Workflow = displayEntity.Workflow;
            result.WorkflowStep = displayEntity.WorkflowStep;
            result.WorkflowGroup = displayEntity.WorkflowGroup;
            //result.WorkflowReadonly = displayEntity.WorkflowReadonly;

            foreach (var node in displayEntity.Nodes)
            {
                result.Nodes.Add(node.ToDTO());
            }


            return result;
        }
    }
}
