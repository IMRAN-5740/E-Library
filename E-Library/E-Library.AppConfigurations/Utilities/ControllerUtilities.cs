

using E_Library.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Library.AppConfigurations.Utilities
{
    public class ControllerUtilities
    {
        public static void ModelStateErrorBind(Result result, ModelStateDictionary modelState)
        {
            if (result.ErrorMessages.Any())
            {
                foreach (var error in result.ErrorMessages)
                {
                    modelState.AddModelError("", error);

                }
            }

        }
    }
}
