using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Appiume.Apm.Tenancy.IdentityFramework
{
    public class ApmIdentityResult : IdentityResult
    {
        public ApmIdentityResult()
        {
            
        }

        public ApmIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {
            
        }

        public ApmIdentityResult(params string[] errors)
            :base(errors)
        {
            
        }

        public static ApmIdentityResult Failed(params string[] errors)
        {
            return new ApmIdentityResult(errors);
        }
    }
}