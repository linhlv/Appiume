using System;
using Appiume.Apm.Dependency;

namespace Appiume.Apm
{
    /// <summary>
    /// Implements <see cref="IGuidGenerator"/> by using <see cref="Guid.NewGuid"/>.
    /// </summary>
    public class RegularGuidGenerator : IGuidGenerator, ITransientDependency
    {
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}