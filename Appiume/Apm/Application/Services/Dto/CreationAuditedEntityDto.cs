using System;

namespace Appiume.Apm.Application.Services.Dto
{
    /// <summary>
    ///  A shortcut of <see cref="CreationAuditedEntityDto"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntityDto : CreationAuditedEntityDto<int>
    {
        
    }
}