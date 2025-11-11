using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ValidationException = TalentMatch.Infrastructure.Exceptions.ValidationException;

namespace TalentMatch.Infrastructure.Middlewares
{
    public class ValidateFilterAttribute : IAsyncResultFilter, IFilterMetadata
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            List<string> list = new List<string>();
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                (string, IEnumerable<string>)[] array = (from x in context.ModelState.Where<KeyValuePair<string, ModelStateEntry>>(delegate (KeyValuePair<string, ModelStateEntry> x)
                {
                    ModelStateEntry value = x.Value;
                    return value != null && value.Errors.Count > 0;
                })
                                                         select (x.Key, x.Value.Errors.Select((ModelError x) => x.ErrorMessage))).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    foreach (string item in array[i].Item2)
                    {
                        list.Add(item);
                    }
                }

                if (list.Count != 0)
                {
                    throw new ValidationException(list);
                }
            }

            await next();
        }
    }
}