﻿using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using RIL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIL.ActionFilters
{
    public class SortAndFilterParamsValidationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.ActionArguments["sortingParameter"] != null)
            {
                if ((int)context.ActionArguments["sortingParameter"] > Enum.GetValues(typeof(SortingParameterViewModel)).Cast<int>().Last()
                    || (int)context.ActionArguments["sortingParameter"] < Enum.GetValues(typeof(SortingParameterViewModel)).Cast<int>().First())
                {
                    context.ActionArguments["sortingParameter"] = (int)SortingParameterViewModel.Default;
                }
            }
            else
            {
                context.ActionArguments["sortingParameter"] = (int)SortingParameterViewModel.Default;
            }


            var genreFilter = context.ActionArguments["genreFilter"] as int[];
            if (genreFilter.Length != 0)
            {
                if(genreFilter.Any(z => z > Enum.GetValues(typeof(Genre)).Cast<int>().Last() || z < Enum.GetValues(typeof(Genre)).Cast<int>().First()))
                {
                    context.ActionArguments["genreFilter"] = genreFilter
                        .Where(f => (f <= Enum.GetValues(typeof(Genre)).Cast<int>().Last()) 
                        && (f >= Enum.GetValues(typeof(Genre)).Cast<int>().First()))
                        .Select(f => f)
                        .ToArray();
                }
            }

            var ageFilter = context.ActionArguments["ageFilter"] as int[];
            if (ageFilter.Length != 0)
            {
                if (ageFilter.Any(z => z > Enum.GetValues(typeof(Rating)).Cast<int>().Last() || z < Enum.GetValues(typeof(Rating)).Cast<int>().First()))
                {
                    context.ActionArguments["ageFilter"] = ageFilter
                        .Where(f => (f <= Enum.GetValues(typeof(Rating)).Cast<int>().Last()) 
                        && (f >= Enum.GetValues(typeof(Rating)).Cast<int>().First()))
                        .Select(f => f)
                        .ToArray();
                }
            }

            var result = await next();
        }
    }
}
