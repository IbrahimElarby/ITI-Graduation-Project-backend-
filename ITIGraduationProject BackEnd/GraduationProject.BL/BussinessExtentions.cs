
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public static class BussinessExtentions
    {
        public static void AddBusinessServices(
       this IServiceCollection services)
        {
           
            services.AddValidatorsFromAssembly(
           typeof(BussinessExtentions).Assembly);


        }
    }
}
