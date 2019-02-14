using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Web
{
    public class SwaggerHelp : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
            var attrs = context.ApiDescription.ActionAttributes();
            foreach (var attr in attrs)
            {
                /*
                // 如果 Attribute 是我们自定义的验证过滤器
                if (attr.GetType() == typeof())
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "AuthToken",
                        In = "header",
                        Type = "string",
                        Required = false
                    });
                }
                */
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = false
                });
            }

        }
    }
}
